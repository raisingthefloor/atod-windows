// Copyright 2022-2024 Raising the Floor - US, Inc.
//
// The R&D leading to these results received funding from the:
// * Rehabilitation Services Administration, US Dept. of Education under
//   grant H421A150006 (APCP)
// * National Institute on Disability, Independent Living, and
//   Rehabilitation Research (NIDILRR)
// * Administration for Independent Living & Dept. of Education under grants
//   H133E080022 (RERC-IT) and H133E130028/90RE5003-01-00 (UIITA-RERC)
// * European Union's Seventh Framework Programme (FP7/2007-2013) grant
//   agreement nos. 289016 (Cloud4all) and 610510 (Prosperity4All)
// * William and Flora Hewlett Foundation
// * Ontario Ministry of Research and Innovation
// * Canadian Foundation for Innovation
// * Adobe Foundation
// * Consumer Electronics Association Foundation

using Morphic.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Atod.Deployment.Msi;

public class WindowsInstaller
{
    public class ProgressEventArgs : EventArgs
    {
        public double Percent;
    }
    public EventHandler<ProgressEventArgs>? ProgressUpdate = null;

    // NOTE: we retain references to our MSI external record handlers; this is necessary to prevent the GC from GC'ing our PInvoke callback
	//       in the future, we need to un-reference these at an appropriate time
    private static List<ExtendedPInvoke.INSTALLUI_HANDLER_RECORD> RetainedUIHandlerRecords = new();

    public record InstallResult
    {
        public bool RebootRequired;
    }
    //
    public record InstallError : MorphicAssociatedValueEnum<InstallError.Values>
    {
        // enum members
        public enum Values
        {
            MonitoringHookFailedToInitialize,
            SwitchToSilentInstallerModeFailed,
            Win32Error/*(uint win32ErrorCode)*/,
        }

        // functions to create member instances
        public static InstallError MonitoringHookFailedToInitialize(uint win32ErrorCode) => new InstallError(Values.MonitoringHookFailedToInitialize) { Win32ErrorCode = win32ErrorCode };
        public static InstallError SwitchToSilentInstallerModeFailed => new InstallError(Values.SwitchToSilentInstallerModeFailed);
        public static InstallError Win32Error(uint win32ErrorCode) => new InstallError(Values.Win32Error) { Win32ErrorCode = win32ErrorCode };

        // associated values
        public uint? Win32ErrorCode { get; private set; }

        // verbatim required constructor implementation for MorphicAssociatedValueEnums
        private InstallError(Values value) : base(value) { }
    }
    //
    public async Task<MorphicResult<InstallResult, InstallError>> InstallAsync(string packagePath, Dictionary<string, string> propertySettings, bool suppressUacPrompt = false)
    {
        /* Configure the Windows Installer APIs to run in silent mode (and allowing/disallowing UAC prompts as requested by the caller) */

        // as we are using the Windows Installer APIs in silent mode, we have no hWnd to serve as parent of the installer UI elements; a value of zero means "desktop hWnd"
        var newInternalUIhWnd = IntPtr.Zero;
        //
        ExtendedPInvoke.INSTALLUILEVEL newInternalUILevel;
        if (suppressUacPrompt == true)
        {
            // run installers silently and suppress any UAC prompts
            newInternalUILevel = ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NONE;
        }
        else
        {
            // allow the UAC prompt (if and where required), but otherwise run installers silently
            newInternalUILevel = ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NONE | ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_UACONLY;
        }
        //
        // NOTE: we intentionally set the parent window to hWnd zero (desktop) instead of passing null; passing null might keep another parent hWnd which we set previously after msi.dll was loaded
        // NOTE: as the uiHWnd parameter of MsiSetInternalUI changes the value we pass in, create a copy of our new internal UI hWnd to preserve its value (for future comparisons, etc.)
        var internalUIhWnd = newInternalUIhWnd;
        var previousUILevel = ExtendedPInvoke.MsiSetInternalUI(newInternalUILevel, ref internalUIhWnd);
        // if the result is INSTALLUILEVEL_NOCHANGE, this means that we supplied an invalid installation level; this is not specific to a particular MSI file; treat this as a code error
        if (previousUILevel == ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NOCHANGE)
        {
            Debug.Assert(false, "Installation level was not changed; this indicates that the supplied installation level was invalid.");
            return MorphicResult.ErrorResult(InstallError.SwitchToSilentInstallerModeFailed);
        }
        // NOTE: the internalUIhWnd parameter is set to the previous internal UI hWnd as part of the MsiSetInternalUI function call's work
        var previousInternalUIhWnd = newInternalUIhWnd;

        try
        {
            /* Configure the messages we want to capture using our external user interface handler; we'll also effectively suppress the MSI's UI by handling all the messages */

            // see: https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetexternaluirecord
            var messageFilter =
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_ACTIONDATA |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_ACTIONSTART |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_COMMONDATA |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_ERROR |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_FATALEXIT |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_FILESINUSE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INFO |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INITIALIZE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INSTALLSTART |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INSTALLEND |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_OUTOFDISKSPACE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_PROGRESS |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_RESOLVESOURCE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_RMFILESINUSE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_SHOWDIALOG |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_TERMINATE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_USER |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_WARNING;
            //
            ExtendedPInvoke.INSTALLUI_HANDLER_RECORD? previousHandler;
            // NOTE: we must create an instance of INSTALLUI_HANDLER_RECORD which points to our function (to prevent the PInvoke callback from getting garbage collected)
            var installUIRecordHandler = new ExtendedPInvoke.INSTALLUI_HANDLER_RECORD(this.InstallUiRecordHandler);
            WindowsInstaller.RetainedUIHandlerRecords.Add(installUIRecordHandler);
            var msiSetExternalUIRecordResult = ExtendedPInvoke.MsiSetExternalUIRecord(installUIRecordHandler, messageFilter, IntPtr.Zero, out previousHandler);
            switch (msiSetExternalUIRecordResult)
            {
                case (int)PInvoke.Win32ErrorCode.ERROR_SUCCESS:
                    // success; proceed
                    break;
                case (int)PInvoke.Win32ErrorCode.ERROR_CALL_NOT_IMPLEMENTED:
                    // we should not get this failure (as it would indicate that we were called from a custom action)
                    return MorphicResult.ErrorResult(InstallError.MonitoringHookFailedToInitialize(msiSetExternalUIRecordResult));
                default:
                    // undocumented error result code
                    Debug.Assert(false, "Unknown result: " + msiSetExternalUIRecordResult.ToString());
                    return MorphicResult.ErrorResult(InstallError.MonitoringHookFailedToInitialize(msiSetExternalUIRecordResult));
            }

            // set up event handler loop
            this.StartEventLoop();
            //
            try
            {
                // install the product (i.e. install the MSI)
                var commandLineArgsBuilder = new StringBuilder();
                foreach (var propertySetting in propertySettings)
                {
                    if (commandLineArgsBuilder.Length > 0)
                    {
                        commandLineArgsBuilder.Append(" ");
                    }
                    commandLineArgsBuilder.Append(propertySetting.Key + "=" + propertySetting.Value);
                }
                var commandLineArgs = commandLineArgsBuilder.ToString();
                //
                var msiInstallProductResult = await Task.Run(() =>
                {
                    return ExtendedPInvoke.MsiInstallProduct(packagePath, commandLineArgs);
                });

                switch (msiInstallProductResult)
                {
                    case (uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS:
                        return MorphicResult.OkResult(new InstallResult { RebootRequired = false });
                    case (uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS_REBOOT_REQUIRED:
                        return MorphicResult.OkResult(new InstallResult { RebootRequired = true });
                    default:
                        return MorphicResult.ErrorResult(InstallError.Win32Error((uint)msiInstallProductResult));
                }
            }
            finally
            {
                // tear down event handler loop
                this.StopEventLoop();

                // restore the previous handler, passing in 0 as the message filter parameter (and disable our external UI record handler in the process)
                // see: https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetexternaluirecord
                ExtendedPInvoke.INSTALLUI_HANDLER_RECORD? previousHandlerToRestore = previousHandler;
                msiSetExternalUIRecordResult = ExtendedPInvoke.MsiSetExternalUIRecord(previousHandlerToRestore, 0, IntPtr.Zero, out previousHandler);
                //
                // verify that the external UI record handler was successfully disabled
                switch (msiSetExternalUIRecordResult)
                {
                    case (int)PInvoke.Win32ErrorCode.ERROR_SUCCESS:
                        // success; proceed
                        break;
                    case (int)PInvoke.Win32ErrorCode.ERROR_CALL_NOT_IMPLEMENTED:
                        // we should not get this failure (as it would indicate that we were called from a custom action)
                        Debug.Assert(false, "WARNING: MsiSetExternalUIRecord tried to disable the external message handler but received the error ERROR_CALL_NOT_IMPLEMENTED (which should only happen if we were calling from a custom action--which is not a supported scenario).");
                        break;
                    default:
                        // undocumented error result code
                        Debug.Assert(false, "WARNING: MsiSetExternalUIRecord tried to disable the external message handler but received the unknown error: " + msiSetExternalUIRecordResult.ToString());
                        break;
                }
                //
                // verify that the previous handler matches our current handler (NOTE: this isn't really critical; it's just a sanity check)
                Debug.Assert(previousHandler == installUIRecordHandler, "WARNING: MsiSetExternalUIRecord disabled the external message handler, but the previous handler returned did not match our handler.");

                // free our unmanaged callback (so that it can be garbage collected)
                installUIRecordHandler = null;
            }
        }
        finally
        {
            // restore the previous internal UI level
            if (previousUILevel != ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NOCHANGE)
            {
                // NOTE: as the uiHWnd parameter of MsiSetInternalUI changes the value we pass in, create a copy of our previous internal UI hWnd to preserve its value
                internalUIhWnd = previousInternalUIhWnd;
                var setInternalUIResult = ExtendedPInvoke.MsiSetInternalUI(previousUILevel, ref internalUIhWnd);
                //
                // verify that the install UI level was restored (and that we changed from the setting we had previously selected)
                if (setInternalUIResult == ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NOCHANGE)
                {
                    Debug.Assert(false, "WARNING: MSiSetInternalUI tried to restore the previous internal UI level but failed (error: INSTALLUILEVEL_NOCHANGE).");
                }
                else if (setInternalUIResult != newInternalUILevel)
                {
                    Debug.Assert(false, "WARNING: MSiSetInternalUI got back a different 'new' internal UI level than what we set.");
                }
                //
                // verify that the install UI hWnd was restored
                Debug.Assert(internalUIhWnd == newInternalUIhWnd, "WARNING: MsiSetInternalUI got back a different 'new' internal UI hWnd than what we set.");
            }
        }
    }

    //

    public async Task<MorphicResult<InstallResult, InstallError>> UninstallAsync(Guid productCode, Dictionary<string, string> propertySettings, bool suppressUacPrompt = false)
    {
        /* Configure the Windows Installer APIs to run in silent mode (and allowing/disallowing UAC prompts as requested by the caller) */

        // as we are using the Windows Installer APIs in silent mode, we have no hWnd to serve as parent of the installer UI elements; a value of zero means "desktop hWnd"
        var newInternalUIhWnd = IntPtr.Zero;
        //
        ExtendedPInvoke.INSTALLUILEVEL newInternalUILevel;
        if (suppressUacPrompt == true)
        {
            // run installers silently and suppress any UAC prompts
            newInternalUILevel = ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NONE;
        }
        else
        {
            // allow the UAC prompt (if and where required), but otherwise run installers silently
            newInternalUILevel = ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NONE | ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_UACONLY;
        }
        //
        // NOTE: we intentionally set the parent window to hWnd zero (desktop) instead of passing null; passing null might keep another parent hWnd which we set previously after msi.dll was loaded
        // NOTE: as the uiHWnd parameter of MsiSetInternalUI changes the value we pass in, create a copy of our new internal UI hWnd to preserve its value (for future comparisons, etc.)
        var internalUIhWnd = newInternalUIhWnd;
        var previousUILevel = ExtendedPInvoke.MsiSetInternalUI(newInternalUILevel, ref internalUIhWnd);
        // if the result is INSTALLUILEVEL_NOCHANGE, this means that we supplied an invalid installation level; this is not specific to a particular MSI file; treat this as a code error
        if (previousUILevel == ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NOCHANGE)
        {
            Debug.Assert(false, "Installation level was not changed; this indicates that the supplied installation level was invalid.");
            return MorphicResult.ErrorResult(InstallError.SwitchToSilentInstallerModeFailed);
        }
        // NOTE: the internalUIhWnd parameter is set to the previous internal UI hWnd as part of the MsiSetInternalUI function call's work
        var previousInternalUIhWnd = newInternalUIhWnd;

        try
        {
            /* Configure the messages we want to capture using our external user interface handler; we'll also effectively suppress the MSI's UI by handling all the messages */

            // see: https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetexternaluirecord
            var messageFilter =
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_ACTIONDATA |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_ACTIONSTART |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_COMMONDATA |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_ERROR |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_FATALEXIT |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_FILESINUSE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INFO |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INITIALIZE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INSTALLSTART |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_INSTALLEND |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_OUTOFDISKSPACE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_PROGRESS |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_RESOLVESOURCE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_RMFILESINUSE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_SHOWDIALOG |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_TERMINATE |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_USER |
                        (uint)ExtendedPInvoke.INSTALLLOGMODE.INSTALLLOGMODE_WARNING;
            //
            ExtendedPInvoke.INSTALLUI_HANDLER_RECORD? previousHandler;
            // NOTE: we must create an instance of INSTALLUI_HANDLER_RECORD which points to our function (to prevent the PInvoke callback from getting garbage collected)
            var installUIRecordHandler = new ExtendedPInvoke.INSTALLUI_HANDLER_RECORD(this.InstallUiRecordHandler);
            WindowsInstaller.RetainedUIHandlerRecords.Add(installUIRecordHandler);
            var msiSetExternalUIRecordResult = ExtendedPInvoke.MsiSetExternalUIRecord(installUIRecordHandler, messageFilter, IntPtr.Zero, out previousHandler);
            switch (msiSetExternalUIRecordResult)
            {
                case (int)PInvoke.Win32ErrorCode.ERROR_SUCCESS:
                    // success; proceed
                    break;
                case (int)PInvoke.Win32ErrorCode.ERROR_CALL_NOT_IMPLEMENTED:
                    // we should not get this failure (as it would indicate that we were called from a custom action)
                    return MorphicResult.ErrorResult(InstallError.MonitoringHookFailedToInitialize(msiSetExternalUIRecordResult));
                default:
                    // undocumented error result code
                    Debug.Assert(false, "Unknown result: " + msiSetExternalUIRecordResult.ToString());
                    return MorphicResult.ErrorResult(InstallError.MonitoringHookFailedToInitialize(msiSetExternalUIRecordResult));
            }

            // set up event handler loop
            this.StartEventLoop();
            //
            try
            {
                // uninstall the product
                var commandLineArgsBuilder = new StringBuilder();
                foreach (var propertySetting in propertySettings)
                {
                    if (commandLineArgsBuilder.Length > 0)
                    {
                        commandLineArgsBuilder.Append(" ");
                    }
                    commandLineArgsBuilder.Append(propertySetting.Key + "=" + propertySetting.Value);
                }
                var commandLineArgs = commandLineArgsBuilder.ToString();
                //
                var msiConfigureProductResult = await Task.Run(() =>
                {
                    string productCodeAsString = productCode.ToString("B"); // format: {00000000-0000-0000-0000-000000000000}
                    return ExtendedPInvoke.MsiConfigureProductEx(productCodeAsString, (int)ExtendedPInvoke.INSTALLLEVEL.INSTALLLEVEL_DEFAULT, ExtendedPInvoke.INSTALLSTATE.INSTALLSTATE_ABSENT, commandLineArgs);
                });

                switch (msiConfigureProductResult)
                {
                    case (uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS:
                        return MorphicResult.OkResult(new InstallResult { RebootRequired = false });
                    case (uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS_REBOOT_REQUIRED:
                        return MorphicResult.OkResult(new InstallResult { RebootRequired = true });
                    //case (uint)PInvoke.Win32ErrorCode.ERROR_INVALID_PARAMETER:
                    //    // invalid parameter
                    //    // NOTE: we handle this as a special case, just in case we want to handle it in the future
                    //    return MorphicResult.ErrorResult(InstallError.Win32Error((uint)msiConfigureProductResult));
                    default:
                        return MorphicResult.ErrorResult(InstallError.Win32Error((uint)msiConfigureProductResult));
                }
            }
            finally
            {
                // tear down event handler loop
                this.StopEventLoop();

                // restore the previous handler, passing in 0 as the message filter parameter (and disable our external UI record handler in the process)
                // see: https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetexternaluirecord
                ExtendedPInvoke.INSTALLUI_HANDLER_RECORD? previousHandlerToRestore = previousHandler;
                msiSetExternalUIRecordResult = ExtendedPInvoke.MsiSetExternalUIRecord(previousHandlerToRestore, 0, IntPtr.Zero, out previousHandler);
                //
                // verify that the external UI record handler was successfully disabled
                switch (msiSetExternalUIRecordResult)
                {
                    case (int)PInvoke.Win32ErrorCode.ERROR_SUCCESS:
                        // success; proceed
                        break;
                    case (int)PInvoke.Win32ErrorCode.ERROR_CALL_NOT_IMPLEMENTED:
                        // we should not get this failure (as it would indicate that we were called from a custom action)
                        Debug.Assert(false, "WARNING: MsiSetExternalUIRecord tried to disable the external message handler but received the error ERROR_CALL_NOT_IMPLEMENTED (which should only happen if we were calling from a custom action--which is not a supported scenario).");
                        break;
                    default:
                        // undocumented error result code
                        Debug.Assert(false, "WARNING: MsiSetExternalUIRecord tried to disable the external message handler but received the unknown error: " + msiSetExternalUIRecordResult.ToString());
                        break;
                }
                //
                // verify that the previous handler matches our current handler (NOTE: this isn't really critical; it's just a sanity check)
                Debug.Assert(previousHandler == installUIRecordHandler, "WARNING: MsiSetExternalUIRecord disabled the external message handler, but the previous handler returned did not match our handler.");

                // free our unmanaged callback (so that it can be garbage collected)
                installUIRecordHandler = null;
            }
        }
        finally
        {
            // restore the previous internal UI level
            if (previousUILevel != ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NOCHANGE)
            {
                // NOTE: as the uiHWnd parameter of MsiSetInternalUI changes the value we pass in, create a copy of our previous internal UI hWnd to preserve its value
                internalUIhWnd = previousInternalUIhWnd;
                var setInternalUIResult = ExtendedPInvoke.MsiSetInternalUI(previousUILevel, ref internalUIhWnd);
                //
                // verify that the install UI level was restored (and that we changed from the setting we had previously selected)
                if (setInternalUIResult == ExtendedPInvoke.INSTALLUILEVEL.INSTALLUILEVEL_NOCHANGE)
                {
                    Debug.Assert(false, "WARNING: MSiSetInternalUI tried to restore the previous internal UI level but failed (error: INSTALLUILEVEL_NOCHANGE).");
                }
                else if (setInternalUIResult != newInternalUILevel)
                {
                    Debug.Assert(false, "WARNING: MSiSetInternalUI got back a different 'new' internal UI level than what we set.");
                }
                //
                // verify that the install UI hWnd was restored
                Debug.Assert(internalUIhWnd == newInternalUIhWnd, "WARNING: MsiSetInternalUI got back a different 'new' internal UI hWnd than what we set.");
            }
        }
    }

    //

    internal int InstallUiRecordHandler(IntPtr pvContext, uint iMessageType, uint hRecord)
    {
        var installationMessageType = PInvokeUtils.InstallUiHandlerMessageType.FromMessageType(iMessageType);

        // NOTE: see message parsing docs at: https://learn.microsoft.com/en-us/windows/win32/msi/parsing-windows-installer-messages
        // NOTE: see notes on processing progress messages at: https://learn.microsoft.com/en-us/windows/win32/msi/handling-progress-messages-using-msisetexternalui

        switch (installationMessageType.InstallationMessageType)
        {
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_ACTIONDATA:
                {
                    var record = new MsiRecord(hRecord);
                    // NOTE: INSTALLMESSAGE_ACTIONDATA is represented by a string whose format is specified by the Template value provided in the ActionText table or
                    //       by the MsiProcessMessage function
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved), we assert in debug mode but gracefully degrade by ignoring the info in production
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_ACTIONDATA should have a record which can be converted to a string.");
                    }

                    // NOTE: we still call ProcessInstallationMessage_ActionData will a null recordAsString if we asserted above; this is so that we continue to move the progress bar forward properly
                    this.ProcessInstallationMessage_ActionData(installationMessageType, recordAsString);
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_ACTIONSTART:
                {
                    var record = new MsiRecord(hRecord);

                    // NOTE: action start messages are formatted as:
                    // "Action [1]: [2]. [3]"

                    // field 1
                    var field1 = record.GetString(1);
                    if (field1 is null)
                    {
                        Debug.Assert(false, "field 1 for INSTALLMESSAGE_ACTIONSTART is null.");
                        // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                        return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                    }

                    // field 2
                    var field2 = record.GetString(2);
                    if (field2 is null)
                    {
                        Debug.Assert(false, "field 2 for INSTALLMESSAGE_ACTIONSTART is null.");
                        // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                        return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                    }

                    // field 1
                    var field3 = record.GetString(3);
                    if (field3 is null)
                    {
                        Debug.Assert(false, "field 3 for INSTALLMESSAGE_ACTIONSTART is null.");
                        // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                        return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                    }

                    this.ProcessInstallationMessage_ActionStart(installationMessageType, field1, field2, field3);
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_COMMONDATA:
                {
                    var record = new MsiRecord(hRecord);

                    // field 1: subtype
                    var field1 = record.GetInteger(1);
                    switch (field1)
                    {
                        case 0:
                            // subtype: Language
                            {
                                // get the numeric language identifier (LANGID)
                                var langId = record.GetString(2);
                                if (langId is null)
                                {
                                    Debug.Assert(false, "LANGID for INSTALLMESSAGE_COMMANDATA (subtype 'Language') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                // get the ANSI code page
                                var ansiCodePage = record.GetString(3);
                                if (ansiCodePage is null)
                                {
                                    Debug.Assert(false, "'ANSI code page' for INSTALLMESSAGE_COMMANDATA (subtype 'Language') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                this.ProcessInstallationMessage_CommonData_Language(installationMessageType, langId!, ansiCodePage!);
                            }
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        case 1:
                            // subtype: Caption
                            {
                                // get the caption/title for a dialog box
                                var captionText = record.GetString(2);
                                if (captionText is null)
                                {
                                    Debug.Assert(false, "Field 2 (caption text) for INSTALLMESSAGE_COMMANDATA (subtype 'Caption') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                // get field 3 to make sure it's null or an empty string
                                var field3 = record.GetString(3);
                                if (field3 is not null && field3 != String.Empty)
                                {
                                    Debug.Assert(false, "Invalid field3 string for INSTALLMESSAGE_COMMONDATA (subtype 'Caption'): " + field3);
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                this.ProcessInstallationMessage_CommonData_Caption(installationMessageType, captionText!);
                            }
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        case 2:
                            // subtype: CancelShow
                            {
                                var showCancelButtonAsInt = record.GetInteger(2);
                                if (showCancelButtonAsInt is null)
                                {
                                    Debug.Assert(false, "Field 2 (visible state) for INSTALLMESSAGE_COMMANDATA (subtype 'CancelType') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }
                                //
                                bool showCancelButton;
                                switch (showCancelButtonAsInt)
                                {
                                    case 0:
                                        showCancelButton = false;
                                        break;
                                    case 1:
                                        showCancelButton = true;
                                        break;
                                    default:
                                        Debug.Assert(false, "INSTALLMESSAGE_COMMONDATA (subtype 'CancelShow') has an invalid value in field 2: " + showCancelButtonAsInt.ToString());
                                        // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                        return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                this.ProcessInstallationMessage_CommonData_CancelShow(installationMessageType, showCancelButton);
                            }
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        case null:
                            // could not convert field into an integer (or field is missing)
                            Debug.Assert(false, "INSTALLMESSAGE_COMMONDATA is missing field 1 (subtype)");
                            // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                        default:
                            Debug.Assert(false, "INSTALLMESSAGE_COMMONDATA has an unknown/undocumented field 1 (subtype): " + field1.ToString());
                            // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                    }
                }
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_ERROR:
                {
                    var record = new MsiRecord(hRecord);
                    // NOTE: INSTALLMESSAGE_ERROR contains an error message
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved), we assert in debug mode but gracefully degrade by ignoring the info in production
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_ERROR should have a record which can be converted to a string.");
                    }

                    // NOTE: we still call ProcessInstallationMessage_Error will a null recordAsString if we asserted above; this is so that we continue to move the progress bar forward properly
                    this.ProcessInstallationMessage_Error(installationMessageType, recordAsString);
                }
                // NOTE: in our currentimplement, we return IDCANCEL if an out of disk space condition is encountered
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_FATALEXIT:
                {
                    var record = new MsiRecord(hRecord);
                    // NOTE: INSTALLMESSAGE_FATALEXIT indicates premature termination of the installation
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved), we assert in debug mode but gracefully degrade by ignoring the info in production
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_FATALEXIT should have a record which can be converted to a string.");
                    }

                    // NOTE: we still call ProcessInstallationMessage_FatalExit will a null recordAsString if we asserted above; this is so that we continue to move the progress bar forward properly
                    this.ProcessInstallationMessage_FatalExit(installationMessageType, recordAsString);
                }
                // NOTE: in our currentimplement, we return IDCANCEL if an out of disk space condition is encountered
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_FILESINUSE:
                // Files are in use; this message would typically result in a dialog being displayed to the user showing the files in use
                {
                    Debug.Assert(false, "INSTALLMESSAGE_FILESINUSE message received; analyze the record's values");
                    //var record = new MsiRecord(hRecord);
                    //var recordAsString = record.TryFormatAsString();
                }
                // NOTE: in our current implementation, we cancel the installation
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_INFO:
                {
                    var record = new MsiRecord(hRecord);

                    // NOTE: the record is a log message; format the record as a string to retrieve it
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved), we assert in debug mode but gracefully degrade by ignoring the info in production
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_INFO should have a record which can be converted to a string.");
                    }
                    else
                    {
                        this.ProcessInstallationMessage_Info(installationMessageType, recordAsString);
                    }
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_INITIALIZE:
                // the UI sequence has started
                {
                    // the record should be missing (i.e. zero)
                    Debug.Assert(hRecord == 0, "Windows Installer callback for INSTALLMESSAGE_INITIALIZE should not have a record.");

                    this.ProcessInstallationMessage_Initialize(installationMessageType);
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_INSTALLEND:
                // NOTE: INSTALLMESSAGE_INSTALLEND is not documented at https://learn.microsoft.com/en-us/windows/win32/msi/parsing-windows-installer-messages
                {
                    //var record = new MsiRecord(hRecord);
                    ////
                    //// NOTE: field1 appears to be the name of the installer/product (e.g. "Read&Write")
                    //var field1 = record.GetString(1);
                    ////
                    //// NOTE: field2 appears to be a GUID (presumably the GUID of the installer package or the major/minor GUID tracker)
                    //var field2 = record.GetString(2);
                    ////
                    //// NOTE: field3 is unknown; it appears to be an integer (which was the value 1, which happens to be the same as the INSTALL action's return value, in a successful installation test)
                    //var field3 = record.GetString(3);
                    //////
                    ////var recordAsString = record.TryFormatAsString();

                    this.ProcessInstallationMessage_InstallEnd(installationMessageType);
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_INSTALLSTART:
                // NOTE: INSTALLMESSAGE_INSTALLSTART is not documented at https://learn.microsoft.com/en-us/windows/win32/msi/parsing-windows-installer-messages
                {
                    //var record = new MsiRecord(hRecord);
                    ////
                    //// NOTE: field1 appears to be the name of the installer/product (e.g. "Read&Write")
                    //var field1 = record.GetString(1);
                    ////
                    //// NOTE: field2 appears to be a GUID (presumably the GUID of the installer package or the major/minor GUID tracker)
                    //var field2 = record.GetString(2);
                    //////
                    ////var recordAsString = record.TryFormatAsString();

                    this.ProcessInstallationMessage_InstallStart(installationMessageType);
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_OUTOFDISKSPACE:
                {
                    var record = new MsiRecord(hRecord);
                    // NOTE: INSTALLMESSAGE_OUTOFDISKSPACE is represented by a string indicating an out of disk condition
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved), we assert in debug mode but gracefully degrade by ignoring the info in production
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_OUTOFDISKSPACE should have a record which can be converted to a string.");
                    }

                    // NOTE: we still call ProcessInstallationMessage_OutOfDiskSpace will a null recordAsString if we asserted above; this is so that we continue to move the progress bar forward properly
                    this.ProcessInstallationMessage_OutOfDiskSpace(installationMessageType, recordAsString);
                }
                // NOTE: in our currentimplement, we return IDCANCEL if an out of disk space condition is encountered
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_PERFORMANCE:
                // NOTE: INSTALLMESSAGE_PERFORMANCE is not documented at https://learn.microsoft.com/en-us/windows/win32/msi/parsing-windows-installer-messages
                {
                    Debug.Assert(false, "INSTALLMESSAGE_PERFORMANCE message received; analyze the record's values");
                    //var record = new MsiRecord(hRecord);
                    //var recordAsString = record.TryFormatAsString();
                }
                // in our current implementation, we return 0 (i.e. we simply monitor the message, forwarding it along to the next handler)
                return 0;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_PROGRESS:
                {
                    var record = new MsiRecord(hRecord);

                    // field 1: subtype
                    var field1 = record.GetInteger(1);
                    switch (field1)
                    {
                        case 0:
                            // subtype: Reset (reset of the progress bar)
                            {
                                var estimatedNumberOfTicksInProgressBar = record.GetInteger(2);
                                if (estimatedNumberOfTicksInProgressBar is null)
                                {
                                    Debug.Assert(false, "Field 2 ([estimated] number of ticks in progress bar) for INSTALLMESSAGE_PROGRESS (subtype 'Reset') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                var motionDirectionAsInt = record.GetInteger(3);
                                if (motionDirectionAsInt is null)
                                {
                                    Debug.Assert(false, "Field 3 (direction of progress bar motion) for INSTALLMESSAGE_PROGRESS (subtype 'Reset') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }
                                //
                                bool motionDirectionIsForward;
                                switch (motionDirectionAsInt)
                                {
                                    case 0:
                                        motionDirectionIsForward = true;
                                        break;
                                    case 1:
                                        motionDirectionIsForward = false;
                                        break;
                                    default:
                                        Debug.Assert(false, "INSTALLMESSAGE_COMMONDATA (subtype 'Reset') has an invalid value in field 2: " + motionDirectionAsInt.ToString());
                                        // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                        return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                var field4Value = record.GetInteger(4);
                                if (field4Value is null)
                                {
                                    Debug.Assert(false, "Field 4 (running vs. script flag) for INSTALLMESSAGE_PROGRESS (subtype 'Reset') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                this.ProcessInstallationMessage_Progress_Reset(installationMessageType, estimatedNumberOfTicksInProgressBar.Value, motionDirectionIsForward, field4Value.Value);
                            }
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        case 1:
                            // subtype: ActionInfo (reset of the progress bar)
                            {
                                var numberOfTicksToMovePerActionData = record.GetInteger(2);
                                if (numberOfTicksToMovePerActionData is null)
                                {
                                    Debug.Assert(false, "Field 2 (number of ticks of movement per ACTIONDATA message) for INSTALLMESSAGE_PROGRESS (subtype 'ActionInfo') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                var field3 = record.GetInteger(3);
                                if (field3 is null)
                                {
                                    Debug.Assert(false, "Field 3 for INSTALLMESSAGE_PROGRESS (subtype 'ActionInfo') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }
                                if (field3 != 0 && field3 != 1)
                                {
                                    Debug.Assert(false, "Field 3 for INSTALLMESSAGE_PROGRESS (subtype 'ActionInfo') has an unknown value: " + field3.ToString());
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }
                                
                                // NOTE: field4 is unused

                                if (field3 == 1)
                                {
                                    // if field3 == 1 then we should increment the progress bar by the number of ticks in field 2 (numberOfTicksToMovePerActionData) for each
                                    // ActionData message sent by the _current_ action
                                    this.ProcessInstallationMessage_Progress_ActionInfo(installationMessageType, numberOfTicksToMovePerActionData.Value);
                                }
                            }
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        case 2:
                            // subtype: ProgressReport (i.e. the progress bar has moved a number of ticks)
                            {
                                var numberOfTicksMoved = record.GetInteger(2);
                                if (numberOfTicksMoved is null)
                                {
                                    Debug.Assert(false, "Field 3 (number of ticks moved) for INSTALLMESSAGE_PROGRESS (subtype 'ProgressReport') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                // NOTE: field3 and field4 are unused

                                this.ProcessInstallationMessage_Progress_ProgressReport(installationMessageType, numberOfTicksMoved.Value);
                            }
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        case 3:
                            // subtype: ProgressAddition (i.e. the progress bar's total # of ticks should be increased)
                            {
                                var numberOfTicksToAddToTotalExpectedCount = record.GetInteger(2);
                                if (numberOfTicksToAddToTotalExpectedCount is null)
                                {
                                    Debug.Assert(false, "Field 3 (number of ticks to add to the expected count) for INSTALLMESSAGE_PROGRESS (subtype 'ProgressAddition') is null.");
                                    // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                                    return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                                }

                                // NOTE: field3 and field4 are unused

                                this.ProcessInstallationMessage_Progress_ProgressAddition(installationMessageType, numberOfTicksToAddToTotalExpectedCount.Value);
                            }
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        case null:
                            // could not convert field into an integer (or field is missing)
                            //
                            // NOTE: ideally, we would assert in this condition; however we actually see this condition in frequent installers, and it's progress info (so it's
                            //       technically non-critical as it does not affect the success or failure of the installer's process); therefore we ignore the message.
                            //
                            //Debug.Assert(false, "INSTALLMESSAGE_PROGRESS is missing field 1 (subtype)");
                            //// NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                            //return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                            //
                            // ignore the message
                            Debug.WriteLine("INSTALLMESSAGE_PROGRESS is missing field 1 (subtype)");
                            //
                            // NOTE: ideally, we'd return -1 (indicating an error in the external ui handler), but out of an abundance of caution we'll just return IDOK, following
                            //       the pattern used at https://learn.microsoft.com/en-us/windows/win32/msi/handling-progress-messages-using-msisetexternalui
                            //return -1;
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
                        default:
                            Debug.Assert(false, "INSTALLMESSAGE_PROGRESS has an unknown/undocumented field 1 (subtype): " + field1.ToString());
                            // NOTE: returning IDCANCEL from messages which do not include a button type will cancel the installation
                            return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL;
                    }
                }
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_RESOLVESOURCE:
                // [monitor-only message]
                {
                    Debug.Assert(false, "INSTALLMESSAGE_RESOLVESOURCE message received; analyze the record's values");
                    //var record = new MsiRecord(hRecord);
                    //var recordAsString = record.TryFormatAsString();
                }
                //
                // NOTE: we must return 0 to let Windows Installer handle this message
                // see: https://learn.microsoft.com/en-us/windows/win32/msi/parsing-windows-installer-messages
                return 0;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_RMFILESINUSE:
                // Files are in use (but we may work with the restart manager to prevent the system from needing to restart) 
                {
                    // see: https://learn.microsoft.com/en-us/windows/win32/msi/using-windows-installer-with-restart-manager
                    // see: https://learn.microsoft.com/en-us/windows/win32/msi/using-restart-manager-with-an-external-ui-

                    Debug.Assert(false, "INSTALLMESSAGE_RMFILESINUSE message received; analyze the record's values");
                    //var record = new MsiRecord(hRecord);
                    //var recordAsString = record.TryFormatAsString();
                }
                // NOTE: in our current implementation, we cancel the installation
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDCANCEL; // NOTE: returning -1 will also end the installation for this message
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_SHOWDIALOG:
                {
                    Debug.Assert(false, "INSTALLMESSAGE_SHOWDIALOG message received; in theory, this should not be received in silent install scenarios");

                    var record = new MsiRecord(hRecord);
                    // NOTE: INSTALLMESSAGE_SHOWDIALOG contains the name of the current dialog box in the record string
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved)
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_SHOWDIALOG should have a record which can be converted to a string.");
                    }
                    else
                    {
                        this.ProcessInstallationMessage_ShowDialog(installationMessageType, recordAsString);
                    }
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_TERMINATE:
                // the UI sequence has ended
                {
                    // the record should be missing (i.e. zero)
                    Debug.Assert(hRecord == 0, "Windows Installer callback for INSTALLMESSAGE_TERMINATE should not have a record.");

                    this.ProcessInstallationMessage_Terminate(installationMessageType);
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_USER:
                {
                    var record = new MsiRecord(hRecord);
                    // NOTE: INSTALLMESSAGE_USER contains a formatted user message
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved)
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_USER should have a record which can be converted to a string.");
                    }
                    else
                    {
                        this.ProcessInstallationMessage_User(installationMessageType, recordAsString);
                    }
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            case ExtendedPInvoke.INSTALLMESSAGE.INSTALLMESSAGE_WARNING:
                {
                    var record = new MsiRecord(hRecord);
                    // NOTE: INSTALLMESSAGE_WARNING contains a formatted warning message
                    var recordAsString = record.TryFormatAsString();
                    if (recordAsString is null)
                    {
                        // NOTE: if the string value is null (missing or could not be retrieved)
                        Debug.Assert(false, "Windows Installer callback for INSTALLMESSAGE_WARNING should have a record which can be converted to a string.");
                    }
                    else
                    {
                        this.ProcessInstallationMessage_Warning(installationMessageType, recordAsString);
                    }
                }
                return (int)ExtendedPInvoke.DialogBoxCommandID.IDOK;
            default:
                Debug.Assert(false, "Windows installer callback installation message type is an unknown value: " + installationMessageType.InstallationMessageType.ToString());
                // NOTE: returning -1 indicates that the external message processor encountered an error
                return -1;
        }
    }

    //

    // NOTE: in our current implementation, we track progress similarly to Microsoft's examples; moving forward, we will
	//       probably expand this functionality and pass more data along to the caller (via the event handlers), allow for
	//       more precise progress tracking, etc.
    bool _progressStarted = false; // set to true when Progress Reset message is received
    bool _actionDataProcessingEnabled = false; // set to true when ActionInfo message is received
    int _estimatedTotalNumberOfProgressTicks = 0;
    int _currentNumberOfProgressTicks = 0;
    bool _progressMotionDirectionIsForward = true;
    bool _scriptIsCurrentlyRunning = false;
    //
    int _numberOfTicksToMovePerActionDataMessage = 0;

    private void ProcessInstallationMessage_ActionData(PInvokeUtils.InstallUiHandlerMessageType messageType, string? message)
    {
        Debug.WriteLine("MSI: ActionData >>> " + (message is not null ? message : "[null]"));

        // if the progress has not been initialized or the action data message has not been enabled
        if (_progressStarted == false || _actionDataProcessingEnabled == false)
        {
            return;
        }

        int newNumberOfTicks;
        try
        {
            newNumberOfTicks = checked(_currentNumberOfProgressTicks + _numberOfTicksToMovePerActionDataMessage);
        }
        catch (OverflowException)
        {
            newNumberOfTicks = int.MaxValue;
            Debug.Assert(false, "Number of ticks exceeds upper bounds of variable type");
        }
        //
        if (newNumberOfTicks > _estimatedTotalNumberOfProgressTicks)
        {
            newNumberOfTicks = _estimatedTotalNumberOfProgressTicks;
            Debug.Assert(false, "Current number of ticks exceeds total number of ticks (i.e. >100%)");
        }
        _currentNumberOfProgressTicks = newNumberOfTicks;

        this.OutputUpdatedProgress();
    }

    private void ProcessInstallationMessage_ActionStart(PInvokeUtils.InstallUiHandlerMessageType messageType, string field1, string field2, string field3)
    {
        // field1: time that the action was started using the Time property format
        // field2: action's name (from the sequence table)
        // field3: action's description (from the ActionText table or from the MsiProcessMessage function)

        // format: Action [1]: [2]. [3]
        Debug.WriteLine("MSI: ActionStart >>> Action " + field1 + ": " + field2 + ". " + field3);

        // a new action has started, so disable ActionData messages until we have received information on the per-message tick increment
        _actionDataProcessingEnabled = false;
    }

    private void ProcessInstallationMessage_CommonData_Language(PInvokeUtils.InstallUiHandlerMessageType messageType, string langId, string ansiCodePage)
    {
        Debug.WriteLine("MSI: CommandData > Language (langId == \"" + langId + "\"; ansiCodePage == \"" + ansiCodePage + "\"))");
    }

    private void ProcessInstallationMessage_CommonData_Caption(PInvokeUtils.InstallUiHandlerMessageType messageType, string captionText)
    {
        Debug.WriteLine("MSI: CommandData > Caption (captionText == " + captionText + ")");
    }

    private void ProcessInstallationMessage_CommonData_CancelShow(PInvokeUtils.InstallUiHandlerMessageType messageType, bool visible)
    {
        Debug.WriteLine("MSI: CommandData > CancelShow (visible == " + visible.ToString() + ")");
    }

    private void ProcessInstallationMessage_Error(PInvokeUtils.InstallUiHandlerMessageType messageType, string? message)
    {
        Debug.WriteLine("MSI: Error >>> " + (message is not null ? message : "[null]"));
    }

    private void ProcessInstallationMessage_FatalExit(PInvokeUtils.InstallUiHandlerMessageType messageType, string? message)
    {
        Debug.WriteLine("MSI: FatalExit >>> " + (message is not null ? message : "[null]"));
    }

    private void ProcessInstallationMessage_Info(PInvokeUtils.InstallUiHandlerMessageType messageType, string message)
    {
        Debug.WriteLine("MSI: Info >>> " + message);
    }

    private void ProcessInstallationMessage_Initialize(PInvokeUtils.InstallUiHandlerMessageType messageType)
    {
        Debug.WriteLine("MSI: Initialize");
    }

    private void ProcessInstallationMessage_InstallEnd(PInvokeUtils.InstallUiHandlerMessageType messageType)
    {
        Debug.WriteLine("MSI: InstallEnd");
    }

    private void ProcessInstallationMessage_InstallStart(PInvokeUtils.InstallUiHandlerMessageType messageType)
    {
        Debug.WriteLine("MSI: InstallStart");
    }

    private void ProcessInstallationMessage_OutOfDiskSpace(PInvokeUtils.InstallUiHandlerMessageType messageType, string? message)
    {
        Debug.WriteLine("MSI: OutOfDiskSpace >>> " + (message is not null ? message : "[null]"));
    }

    private void ProcessInstallationMessage_Progress_Reset(PInvokeUtils.InstallUiHandlerMessageType messageType, int estimatedNumberOfTicksInProgressBar, bool motionDirectionIsForward, int field4Value)
    {
        // NOTE: field 4 values should be interpreted as follows:
        // field 4 value of 0 means that the installation is in progress and the time remaining may be calculated.
        // field 4 value of 1 means that a script is running (i.e. the installer should display "please wait...")

        Debug.WriteLine("MSI: Progress > Reset (estimatedNumberOfTicksInProgressBar == " + estimatedNumberOfTicksInProgressBar.ToString() + "; motionDirection: " + ((motionDirectionIsForward == true) ? "forward" : "backwards") + "; field4Value: " + field4Value.ToString() + ")");

        _progressStarted = true;
        _actionDataProcessingEnabled = false;
        //
        // intialize all our progress-related variables
        _currentNumberOfProgressTicks = 0;
        _estimatedTotalNumberOfProgressTicks = estimatedNumberOfTicksInProgressBar;
        //
        _progressMotionDirectionIsForward = motionDirectionIsForward;
        _scriptIsCurrentlyRunning = (field4Value != 0);

        this.OutputUpdatedProgress();
    }

    private void ProcessInstallationMessage_Progress_ActionInfo(PInvokeUtils.InstallUiHandlerMessageType messageType, int numberOfTicksToMovePerActionDataMessage)
    {
        Debug.WriteLine("MSI: Progress > ActionInfo (numberOfTicksToMovePerActionDataMessage == " + numberOfTicksToMovePerActionDataMessage.ToString() + ")");

        // if the progress bar has not been initialized, ignore this message
        if (_progressStarted == false)
        {
            Debug.Assert(false, "Progress has not been initialized, but Progress message (subtype ActionInfo) was received.");
            return;
        }

        Debug.Assert(numberOfTicksToMovePerActionDataMessage >= 0, "Progress message (subtype ActionInfo) reported a negative number of ticks to move per ActionData message.");

        _numberOfTicksToMovePerActionDataMessage = numberOfTicksToMovePerActionDataMessage;

        // now that we know how many ticks to move per action data message, enable the action data message
        _actionDataProcessingEnabled = true;

        this.OutputUpdatedProgress();
    }

    private void ProcessInstallationMessage_Progress_ProgressReport(PInvokeUtils.InstallUiHandlerMessageType messageType, int numberOfTicksMoved)
    {
        Debug.WriteLine("MSI: Progress > ProgressReport (numberOfTicksMoved == " + numberOfTicksMoved.ToString() + ")");

        // if the progress has not been initialized or the maximum number of ticks has not been set, ignore this data
        if (_progressStarted == false || _estimatedTotalNumberOfProgressTicks == 0)
        {
            return;
        }

        Debug.Assert(numberOfTicksMoved >= 0, "Progress message (subtype ProgressReport) reported a negative number of ticks moved.");

        int newNumberOfTicks;
        try
        {
            newNumberOfTicks = checked(_currentNumberOfProgressTicks + numberOfTicksMoved);
        }
        catch (OverflowException)
        {
            newNumberOfTicks = int.MaxValue;
            Debug.Assert(false, "Number of ticks exceeds upper bounds of variable type");
        }
        //
        if (newNumberOfTicks > _estimatedTotalNumberOfProgressTicks)
        {
            newNumberOfTicks = _estimatedTotalNumberOfProgressTicks;
            Debug.Assert(false, "Current number of ticks exceeds total number of ticks (i.e. >100%)");
        }
        _currentNumberOfProgressTicks = newNumberOfTicks;

        this.OutputUpdatedProgress();
    }

    private void ProcessInstallationMessage_Progress_ProgressAddition(PInvokeUtils.InstallUiHandlerMessageType messageType, int numberOfTicksToAddToTotalExpectedCount)
    {
        Debug.WriteLine("MSI: Progress > ProgressAddition (numberOfTicksToAddToTotalExpectedCount == " + numberOfTicksToAddToTotalExpectedCount.ToString() + ")");

        // if the progress bar has not been initialized, ignore this message
        if (_progressStarted == false)
        {
            Debug.Assert(false, "Progress has not been initialized, but Progress message (subtype ProgressAddition) was received.");
            return;
        }

        Debug.Assert(numberOfTicksToAddToTotalExpectedCount >= 0, "Progress message (subtype ProgressAddition) reported a negative number of ticks to add to the estimated total tick count.");

        int newEstimatedTotalNumberOfTicks;
        try
        {
            newEstimatedTotalNumberOfTicks = checked(_estimatedTotalNumberOfProgressTicks + numberOfTicksToAddToTotalExpectedCount);
        }
        catch (OverflowException)
        {
            newEstimatedTotalNumberOfTicks = int.MaxValue;
            Debug.Assert(false, "Number of total ticks (after adding 'added ticks') exceeds upper bounds of variable type");
        }
        _estimatedTotalNumberOfProgressTicks = newEstimatedTotalNumberOfTicks;

        this.OutputUpdatedProgress();
    }

    private void ProcessInstallationMessage_ShowDialog(PInvokeUtils.InstallUiHandlerMessageType messageType, string dialogBoxName)
    {
        Debug.WriteLine("MSI: ShowDialog >>> " + dialogBoxName);
    }

    private void ProcessInstallationMessage_Terminate(PInvokeUtils.InstallUiHandlerMessageType messageType)
    {
        Debug.WriteLine("MSI: Terminate");
    }

    private void ProcessInstallationMessage_User(PInvokeUtils.InstallUiHandlerMessageType messageType, string message)
    {
        Debug.WriteLine("MSI: User >>> " + message);
    }

    private void ProcessInstallationMessage_Warning(PInvokeUtils.InstallUiHandlerMessageType messageType, string message)
    {
        Debug.WriteLine("MSI: Warning >>> " + message);
    }

    private void OutputUpdatedProgress()
    {
        if (_progressStarted == true)
        {
            var estimatedTotalNumberOfTicks = _estimatedTotalNumberOfProgressTicks;

            double percentage;
            if (estimatedTotalNumberOfTicks != 0)
            {
                percentage = (double)_currentNumberOfProgressTicks / (double)estimatedTotalNumberOfTicks;
            }
            else
            {
                // if the estimated total number of ticks is 0, then by definition our percentage is 100% (or completely uknown).
                percentage = 1.0;
            }

            // if the motion direction is backwards, then invert the percentage
            if (_progressMotionDirectionIsForward == false)
            {
                percentage = 1.0 - percentage;
            }

            ProgressEventArgs eventArgs = new ProgressEventArgs() { Percent = percentage };
            EnqueueEvent("PROGRESS", eventArgs);

            //Debug.WriteLine("PROGRESS: " + ((int)(percentage * 100)).ToString() + "% - " + _currentNumberOfProgressTicks.ToString() + " of " + _estimatedTotalNumberOfProgressTicks.ToString() + " - " + (_scriptIsCurrentlyRunning ? "Please wait..." : "Estimated time remaining is not calculated"));
        }
        else
        {
            //Debug.WriteLine("PROGRESS: NOT YET STARTED");
        }
    }

    //

    #region Event Loop

    private Thread? _eventLoopThread = null;
    private AutoResetEvent? _eventLoopWaitHandle = null;
    private ConcurrentQueue<(string, EventArgs)>? _eventQueue = null;
    private bool _eventLoopShouldShutdown = false;

    private void StartEventLoop()
    {
        if (_eventLoopThread is not null)
        {
            return;
        }

        _eventLoopWaitHandle = new AutoResetEvent(false);
        _eventQueue = new();

        _eventLoopThread = new Thread(this.EventLoop);
        _eventLoopThread.Start();
    }

    private void StopEventLoop()
    {
        _eventLoopShouldShutdown = true;
        _eventLoopWaitHandle!.Set();
    }

    private void EventLoop()
    {
        try
        {
            _eventLoopWaitHandle!.WaitOne();

            (string, EventArgs) item;
            var tryDequeueResult = _eventQueue!.TryDequeue(out item);
            if (tryDequeueResult == true)
            {
                // event was dequeued
                switch (item.Item1.ToUpper())
                {
                    case "PROGRESS":
                        // progress event
                        this.ProgressUpdate?.Invoke(this, (ProgressEventArgs)item.Item2);
                        break;
                    default:
                        break;
                }
            }
        }
        finally
        {
            // if we landed here because of an exception in a delegate, relaunch our event loop
            if (_eventLoopShouldShutdown == false)
            {
                _eventLoopThread = new Thread(this.EventLoop);
                _eventLoopThread.Start();
            }
        }
    }

    private void EnqueueEvent(string eventName, EventArgs eventArgs)
    {
        _eventQueue!.Enqueue((eventName, eventArgs));
        _eventLoopWaitHandle!.Set();
    }

    #endregion Event Loop

}