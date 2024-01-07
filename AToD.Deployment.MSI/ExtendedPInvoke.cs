// Copyright 2022-2023 Raising the Floor - US, Inc.
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AToD.Deployment.MSI;

internal struct ExtendedPInvoke
{
    #region msi.h

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetinternalui
    internal enum INSTALLUILEVEL : uint
    {
        INSTALLUILEVEL_NOCHANGE = 0,
        INSTALLUILEVEL_DEFAULT = 1,
        INSTALLUILEVEL_NONE = 2,
        INSTALLUILEVEL_BASIC = 3,
        INSTALLUILEVEL_REDUCED = 4,
        INSTALLUILEVEL_FULL = 5,
        INSTALLUILEVEL_ENDDIALOG = 0x80,
        INSTALLUILEVEL_PROGRESSONLY = 0x40,
        INSTALLUILEVEL_HIDECANCEL = 0x20,
        INSTALLUILEVEL_SOURCERESONLY = 0x100,
        // Windows 7 or newer
        INSTALLUILEVEL_UACONLY = 0x200,
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetexternaluiw
    internal enum INSTALLLOGMODE : uint
    {
        INSTALLLOGMODE_FATALEXIT = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_FATALEXIT >> 24),
        INSTALLLOGMODE_ERROR = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_ERROR >> 24),
        INSTALLLOGMODE_WARNING = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_WARNING >> 24),
        INSTALLLOGMODE_USER = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_USER >> 24),
        INSTALLLOGMODE_INFO = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_INFO >> 24),
        INSTALLLOGMODE_RESOLVESOURCE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_RESOLVESOURCE >> 24),
        INSTALLLOGMODE_OUTOFDISKSPACE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_OUTOFDISKSPACE >> 24),
        INSTALLLOGMODE_ACTIONSTART = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_ACTIONSTART >> 24),
        INSTALLLOGMODE_ACTIONDATA = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_ACTIONDATA >> 24),
        INSTALLLOGMODE_COMMONDATA = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_COMMONDATA >> 24),
        //
        // log only
        INSTALLLOGMODE_PROPERTYDUMP = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_PROGRESS >> 24),
        INSTALLLOGMODE_VERBOSE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_INITIALIZE >> 24),
        INSTALLLOGMODE_EXTRADEBUG = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_TERMINATE >> 24),
        INSTALLLOGMODE_LOGONLYONERROR = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_SHOWDIALOG >> 24),
        INSTALLLOGMODE_LOGPERFORMANCE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_PERFORMANCE >> 24),
        //
        // external handlers only
        INSTALLLOGMODE_PROGRESS = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_PROGRESS >> 24),
        INSTALLLOGMODE_INITIALIZE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_INITIALIZE >> 24),
        INSTALLLOGMODE_TERMINATE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_TERMINATE >> 24),
        INSTALLLOGMODE_SHOWDIALOG = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_SHOWDIALOG >> 24),
        INSTALLLOGMODE_FILESINUSE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_FILESINUSE >> 24),
        INSTALLLOGMODE_RMFILESINUSE = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_RMFILESINUSE >> 24),
        //
        // external/embedded only
        INSTALLLOGMODE_INSTALLSTART = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_INSTALLSTART >> 24),
        INSTALLLOGMODE_INSTALLEND = (uint)1 << (int)((uint)INSTALLMESSAGE.INSTALLMESSAGE_INSTALLEND >> 24),
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nc-msi-installui_handler_record
    // NOTE: iMessageType is the result of ORing a message box style, a message box icon type, a default button, and an installation message type (INSTALLMESSAGE)
    internal delegate int INSTALLUI_HANDLER_RECORD(IntPtr pvContext, uint iMessageType, uint hRecord);

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nc-msi-installui_handlerw
    internal enum INSTALLMESSAGE : uint
    {
        INSTALLMESSAGE_FATALEXIT = 0x00000000,
        INSTALLMESSAGE_ERROR = 0x01000000,
        INSTALLMESSAGE_WARNING = 0x02000000,
        INSTALLMESSAGE_USER = 0x03000000,
        INSTALLMESSAGE_INFO = 0x04000000,
        INSTALLMESSAGE_FILESINUSE = 0x05000000,
        INSTALLMESSAGE_RESOLVESOURCE = 0x06000000,
        INSTALLMESSAGE_OUTOFDISKSPACE = 0x07000000,
        INSTALLMESSAGE_ACTIONSTART = 0x08000000,
        INSTALLMESSAGE_ACTIONDATA = 0x09000000,
        INSTALLMESSAGE_PROGRESS = 0x0A000000,
        INSTALLMESSAGE_COMMONDATA = 0x0B000000,
        INSTALLMESSAGE_INITIALIZE = 0x0C000000,
        INSTALLMESSAGE_TERMINATE = 0x0D000000,
        INSTALLMESSAGE_SHOWDIALOG = 0x0E000000,
        INSTALLMESSAGE_PERFORMANCE = 0x0F000000,
        INSTALLMESSAGE_RMFILESINUSE = 0x19000000,
        INSTALLMESSAGE_INSTALLSTART = 0x1A000000,
        INSTALLMESSAGE_INSTALLEND = 0x1B000000,
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetinternalui
    [DllImport("msi.dll")]
    internal static extern INSTALLUILEVEL MsiSetInternalUI(INSTALLUILEVEL dwUILevel, ref IntPtr phWnd);
    //
    // NOTE: for scenarios where we don't want to change the owner window of the installer UI, we pass IntPtr.Zero (i.e. a null pointer) instead; note that this is
    //       distinct from passing the value IntPtr.Zero by reference (which should indicate to use the desktop--hWnd 0--as the parent).
    internal static INSTALLUILEVEL MsiSetInternalUI(INSTALLUILEVEL dwUILevel)
    {
        return ExtendedPInvoke.MsiSetInternalUI(dwUILevel, IntPtr.Zero);
    }
    //
    [DllImport("msi.dll")]
    private static extern INSTALLUILEVEL MsiSetInternalUI(INSTALLUILEVEL dwUILevel, IntPtr phWnd);

    //

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msisetexternaluirecord
    [DllImport("msi.dll")]
    internal static extern uint MsiSetExternalUIRecord([MarshalAs(UnmanagedType.FunctionPtr)] INSTALLUI_HANDLER_RECORD? puiHandler, uint dwMessageFilter, IntPtr pvContext, out INSTALLUI_HANDLER_RECORD? ppuiPrevHandler);

    //

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msiinstallproductw
    [DllImport("msi.dll", CharSet = CharSet.Unicode)]
    internal static extern uint MsiInstallProduct([MarshalAs(UnmanagedType.LPWStr)] string szPackagePath, [MarshalAs(UnmanagedType.LPWStr)] string szCommandLine);

    //

    internal enum INSTALLLEVEL : int
    {
        INSTALLLEVEL_DEFAULT = 0,
        INSTALLLEVEL_MINIMUM = 1,
        INSTALLLEVEL_MAXIMUM = 0xFFFF,
    }

    internal enum INSTALLSTATE : int
    {
        INSTALLSTATE_NOTUSED = -7,
        INSTALLSTATE_BADCONFIG = -6,
        INSTALLSTATE_INCOMPLETE = -5,
        INSTALLSTATE_SOURCEABSENT = -4,
        INSTALLSTATE_MOREDATA = -3,
        INSTALLSTATE_INVALIDARG = -2,
        INSTALLSTATE_UNKNOWN = -1,
        INSTALLSTATE_BROKEN = 0,
        INSTALLSTATE_ADVERTISED = 1,
        INSTALLSTATE_REMOVED = 1,
        INSTALLSTATE_ABSENT = 2,
        INSTALLSTATE_LOCAL = 3,
        INSTALLSTATE_SOURCE = 4,
        INSTALLSTATE_DEFAULT = 5,
    }

    // https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msiconfigureproductexw
    [DllImport("msi.dll", CharSet = CharSet.Unicode)]
    internal static extern uint MsiConfigureProductEx([MarshalAs(UnmanagedType.LPWStr)] string szProduct, int iInstallLevel, INSTALLSTATE eInstallState, [MarshalAs(UnmanagedType.LPWStr)] string szCommandLine);

    #endregion msi.h


    #region msiquery.h

    internal const int MSI_NULL_INTEGER = unchecked((int)0x80000000);

    // https://learn.microsoft.com/en-us/windows/win32/api/msiquery/nf-msiquery-msirecordgetinteger
    [DllImport("msi.dll")]
    internal static extern int MsiRecordGetInteger(uint hRecord, uint iField);

    // https://learn.microsoft.com/en-us/windows/win32/api/msiquery/nf-msiquery-msirecordgetstringw
    [DllImport("msi.dll", CharSet = CharSet.Unicode)]
    internal static extern uint MsiRecordGetString(uint hRecord, uint iField, StringBuilder szValueBuf, ref uint pcchValueBuf);

    // https://learn.microsoft.com/en-us/windows/win32/api/msiquery/nf-msiquery-msiformatrecordw
    [DllImport("msi.dll", CharSet = CharSet.Unicode)]
    internal static extern uint MsiFormatRecord(uint hInstall, uint hRecord, StringBuilder szValueBuf, ref uint pcchValueBuf);

    #endregion msiquery.h


    #region winuser.h

    internal enum DialogBoxCommandID : int
    {
        IDOK        = 1,
        IDCANCEL    = 2,
        IDABORT     = 3,
        IDRETRY     = 4,
        IDIGNORE    = 5,
        IDYES       = 6,
        IDNO        = 7,
        IDCLOSE     = 8,
        IDHELP      = 9,
        IDTRYAGAIN  = 10,
        IDCONTINUE  = 11,
    }

    internal enum MessageBoxStyle : uint
    {
        MB_OK                   = 0x00000000,
        MB_OKCANCEL             = 0x00000001,
        MB_ABORTRETRYIGNORE     = 0x00000002,
        MB_YESNOCANCEL          = 0x00000003,
        MB_YESNO                = 0x00000004,
        MB_RETRYCANCEL          = 0x00000005,
        MB_CANCELTRYCONTINUE    = 0x00000006,
    }

    internal enum MessageBoxIconType : uint
    {
        MB_ICONHAND         = 0x00000010,
        MB_ICONQUESTION     = 0x00000020,
        MB_ICONEXCLAMATION  = 0x00000030,
        MB_ICONASTERISK     = 0x00000040,
        MB_USERICON         = 0x00000080,
        MB_ICONWARNING      = MB_ICONEXCLAMATION,
        MB_ICONERROR        = MB_ICONHAND,
        MB_ICONINFORMATION  = MB_ICONASTERISK,
        MB_ICONSTOP         = MB_ICONHAND,
    }

    internal enum MessageBoxDefaultButton : uint
    {
        MB_DEFBUTTON1   = 0x00000000,
        MB_DEFBUTTON2   = 0x00000100,
        MB_DEFBUTTON3   = 0x00000200,
        MB_DEFBUTTON4   = 0x00000300,
    }

    #endregion winuser.h

}
