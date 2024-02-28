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

//#define USE_OFFLINE_BASE_PATH
//#define USE_ATOD_CDN

using System;
using System.Collections.Generic;

namespace Atod;

internal struct KnownApplication
{
#if USE_OFFLINE_BASE_PATH
    static Uri OFFLINE_BASE_URI = new Uri("file://A:/sample/path/");
#endif
#if USE_ATOD_CDN
    static Uri ATOD_CDN_BASE_URI = new Uri("https://atod-cdn.raisingthefloor.org/");
#endif

    public enum IdValue
    {
        AudioNotetaker,
        AutoHotkey,
        BuildABoard,
        CameraMouse,
        ClaroRead,
        ClaroReadSe,
        ClickNType,
        ComfortOsk,
        Communicator5,
        CoWriter,
        DolphinScreenReader,
        Dragger,
        DuxburyBrailleTranslator,
        Equatio,
        FreeVirtualKeyboard,
        Fusion,
        Ghotit,
        Grid3,
        Inspiration,
        Jaws,
        Kurzweil1000,
        Kurzweil3000,
        Magic,
        NaturalReader,
        Nvda,
        OpenBook,
        PurpleP3,
        ReadAndWrite,
        SmyleMouse,
        SofType,
        SuperNovaMagnifier,
        SuperNovaMagnifierAndSpeech,
        WordQ,
        Wynn,
        ZoomText,
    }

    public readonly KnownApplication.IdValue Id { get; init; }

    public static readonly KnownApplication AUDIO_NOTETAKER = new() { Id = IdValue.AudioNotetaker };
    public static readonly KnownApplication AUTOHOTKEY = new() {  Id = IdValue.AutoHotkey };
    public static readonly KnownApplication BUILD_A_BOARD = new() { Id = IdValue.BuildABoard };
    public static readonly KnownApplication CAMERA_MOUSE = new() { Id = IdValue.CameraMouse };
    public static readonly KnownApplication CLAROREAD = new() { Id = IdValue.ClaroRead };
    public static readonly KnownApplication CLAROREAD_SE = new() { Id = IdValue.ClaroReadSe };
    public static readonly KnownApplication CLICK_N_TYPE = new() { Id = IdValue.ClickNType };
    public static readonly KnownApplication COMFORT_OSK = new() { Id = IdValue.ComfortOsk };
    public static readonly KnownApplication COMMUNICATOR_5 = new() { Id = IdValue.Communicator5 };
    public static readonly KnownApplication COWRITER = new() { Id = IdValue.CoWriter };
    public static readonly KnownApplication DOLPHIN_SCREEN_READER = new() { Id = IdValue.DolphinScreenReader };
    public static readonly KnownApplication DRAGGER = new() { Id = IdValue.Dragger };
    public static readonly KnownApplication DUXBURY_BRAILLE_TRANSLATOR = new() { Id = IdValue.DuxburyBrailleTranslator };
    public static readonly KnownApplication EQUATIO = new() { Id = IdValue.Equatio };
    public static readonly KnownApplication FREE_VIRTUAL_KEYBOARD = new() { Id = IdValue.FreeVirtualKeyboard };
    public static readonly KnownApplication FUSION = new() { Id = IdValue.Fusion };
    public static readonly KnownApplication GHOTIT = new() { Id = IdValue.Ghotit };
    public static readonly KnownApplication GRID3 = new() { Id = IdValue.Grid3 };
    public static readonly KnownApplication INSPIRATION = new() { Id = IdValue.Inspiration };
    public static readonly KnownApplication JAWS = new() { Id = IdValue.Jaws };
    public static readonly KnownApplication KURZWEIL_1000 = new() { Id = IdValue.Kurzweil1000 };
    public static readonly KnownApplication KURZWEIL_3000 = new() { Id = IdValue.Kurzweil3000 };
    public static readonly KnownApplication MAGIC = new() { Id = IdValue.Magic };
    public static readonly KnownApplication NATURAL_READER = new() { Id = IdValue.NaturalReader };
    public static readonly KnownApplication NVDA = new() { Id = IdValue.Nvda };
    public static readonly KnownApplication OPEN_BOOK = new() { Id = IdValue.OpenBook };
    public static readonly KnownApplication PURPLE_P3 = new() { Id = IdValue.PurpleP3 };
    public static readonly KnownApplication READ_AND_WRITE = new() { Id = IdValue.ReadAndWrite };
    public static readonly KnownApplication SMYLE_MOUSE = new() { Id = IdValue.SmyleMouse };
    public static readonly KnownApplication SOFTYPE = new() { Id = IdValue.SofType };
    public static readonly KnownApplication SUPERNOVA_MAGNIFIER = new() { Id = IdValue.SuperNovaMagnifier };
    public static readonly KnownApplication SUPERNOVA_MAGNIFIER_AND_SPEECH = new() { Id = IdValue.SuperNovaMagnifierAndSpeech };
    public static readonly KnownApplication WORDQ = new() { Id = IdValue.WordQ };
    public static readonly KnownApplication WYNN = new() { Id = IdValue.Wynn };
    public static readonly KnownApplication ZOOMTEXT = new() { Id = IdValue.ZoomText };

    public static KnownApplication? TryFromProductName(string applicationName)
    {
        switch (applicationName.ToLowerInvariant())
        {
            case "audionotetaker":
                return KnownApplication.AUDIO_NOTETAKER;
            case "autohotkey":
                return KnownApplication.AUTOHOTKEY;
            case "buildaboard":
                return KnownApplication.BUILD_A_BOARD;
            case "cameramouse":
                return KnownApplication.CAMERA_MOUSE;
            case "claroread":
                return KnownApplication.CLAROREAD;
            case "claroreadse":
                return KnownApplication.CLAROREAD_SE;
            case "clickntype":
                return KnownApplication.CLICK_N_TYPE;
            case "comfortosk":
                return KnownApplication.COMFORT_OSK;
            case "communicator5":
                return KnownApplication.COMMUNICATOR_5;
            case "cowriter":
                return KnownApplication.COWRITER;
            case "dolphinscreenreader":
                return KnownApplication.DOLPHIN_SCREEN_READER;
            case "dragger":
                return KnownApplication.DRAGGER;
            case "duxburybrailletranslator":
                return KnownApplication.DUXBURY_BRAILLE_TRANSLATOR;
            case "equatio":
                return KnownApplication.EQUATIO;
            case "freevirtualkeyboard":
                return KnownApplication.FREE_VIRTUAL_KEYBOARD;
            case "fusion":
                return KnownApplication.FUSION;
            case "ghotit":
                return KnownApplication.GHOTIT;
            case "grid3":
                return KnownApplication.GRID3;
            case "inspiration":
                return KnownApplication.INSPIRATION;
            case "jaws":
                return KnownApplication.JAWS;
            case "kurzweil1000":
                return KnownApplication.KURZWEIL_1000;
            case "kurzweil3000":
                return KnownApplication.KURZWEIL_3000;
            case "magic":
                return KnownApplication.MAGIC;
            case "naturalreader":
                return KnownApplication.NATURAL_READER;
            case "nvda":
                return KnownApplication.NVDA;
            case "openbook":
                return KnownApplication.OPEN_BOOK;
            case "purplep3":
                return KnownApplication.PURPLE_P3;
            case "readandwrite":
                return KnownApplication.READ_AND_WRITE;
            case "smylemouse":
                return KnownApplication.SMYLE_MOUSE;
            case "softype":
                return KnownApplication.SOFTYPE;
            case "supernovamagnifier":
                return KnownApplication.SUPERNOVA_MAGNIFIER;
            case "supernovamagnifierandspeech":
                return KnownApplication.SUPERNOVA_MAGNIFIER_AND_SPEECH;
            case "wordq":
                return KnownApplication.WORDQ;
            case "wynn":
                return KnownApplication.WYNN;
            case "zoomtext":
                return KnownApplication.ZOOMTEXT;
            default:
                return null;
        }
    }

    public List<IAtodOperation> GetInstallOperations()
    {
        const int STANDARD_REBOOT_REQUIRED_EXIT_CODE = unchecked((int)(uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS_REBOOT_REQUIRED);

        // capture initial download operations
        var downloadOperations = this.GetInstallDownloadOperations();

        // capture post-download installation operations
        List<IAtodOperation> installOperations;
        switch (this.Id)
        {
            case IdValue.AudioNotetaker:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "SonocentAudioNotetaker_Win_5.3.29.0.exe", "/install /quiet /norestart", [], null, true),
                    ];
                break;
            case IdValue.AutoHotkey:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "AutoHotkey_2.0.11_setup.exe", "/silent", [], null, true),
                    ];
                break;
            case IdValue.BuildABoard:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "bab220r7_win_7-11.exe", "/Q", [], null, true),
                    ];
                break;
            case IdValue.CameraMouse:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "CameraMouse2018Installer.exe", "/VERYSILENT", [], null, true),
                    ];
                break;
            case IdValue.ClaroRead:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "ClaroRead-12.0.29-auth-bundle.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroRead-int-12.0.29-auth.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Capture-int-8.2.5-auth.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroIdeas-int-3.1.0-auth.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroView-int-3.4.8-auth.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Scan2Text-int-7.4.19-auth.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ClaroReadSe:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "ClaroReadSE-12.0.29-auth.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroReadSE-int-12.0.29-auth.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ScanScreenPlus-int-2.2.4-net.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ClickNType:
                // NOTE: this MSI is not digitally signed and should not be included in the catalog until it can be authenticated (and even then, it should probably be forced to use UAC individually for consumers)
                installOperations =
                    [
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "CNTsetup.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ComfortOsk:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "ComfortOSKSetup.exe", "/NORESTART /VERYSILENT /RESTARTEXITCODE=" + STANDARD_REBOOT_REQUIRED_EXIT_CODE.ToString(), [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                    ];
                break;
            case IdValue.Communicator5:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe", "/SILENT", [], null, true),
                    ];
                break;
            case IdValue.CoWriter:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "cowriter-universal-desktop.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "CoWriter Universal Desktop.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.DolphinScreenReader:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "ScreenReader_22.04_English_(United_States)_NETWORK.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        //
                        // NOTE: for MicrosoftEdgeWebView2RuntimeInstaller deployment details, see: https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\MicrosoftEdgeWebView2RuntimeInstallerX86.exe", "/silent /install",
                        //    [
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //    ],
                        //    null, true), // Intel 32-bit
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\MicrosoftEdgeWebView2RuntimeInstallerX64.exe", "/silent /install",
                            [
                                new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                                new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                            ],
                            null, true),   // Intel 64-bit
                        //
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\vc_redist.x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Sam\\Sam.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Sam\\Sam64Addon.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices\\VocExprCommonFiles.msi", null, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices/English_US_Allison.msi", null, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices/English_US_Ava.msi", null, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices/English_US_Samantha.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices\\English_US_Tom.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices\\Orpheus.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "SuperNova\\SnovaRead.msi", new() { { "SETUPLANGLIST", "\"00001\"" } }, RequiresElevation: true), // default language: US-EN
                        // NOTE: the network installation instructions say to ensure that the system is restarted following installation
                    ];
                break;
            case IdValue.Dragger:
                installOperations =
                    [
                        // Intel 32-bit (also works on 64-bit)
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "DraggerSetup2.0.1350.0.exe", "/qn", [], null, true),
                    ];
                break;
            case IdValue.DuxburyBrailleTranslator:
                installOperations =
                    [
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "dbt-1207sr2.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Equatio:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "Equatio.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Setup.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.FreeVirtualKeyboard:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "FreeVKSetup.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "FreeVKSetup.exe", "/NORESTART /VERYSILENT /RESTARTEXITCODE=" + STANDARD_REBOOT_REQUIRED_EXIT_CODE.ToString(), [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                    ];
                break;
            case IdValue.Fusion:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "F2024.2312.8.400.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        ////
                        //// NOTE: for MicrosoftEdgeWebView2RuntimeInstaller deployment details, see: https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution
                        ////new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "MicrosoftEdgeWebView2RuntimeInstallerX86.exe", "/silent /install",
                        ////    [
                        ////        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        ////        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        ////    ],
                        ////    null, true), // Intel 32-bit
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "MicrosoftEdgeWebView2RuntimeInstallerX64.exe", "/silent /install",
                        //    [
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //    ],
                        //    null, true),   // Intel 64-bit
                        ////
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "vcredist2022_x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "vcredist2022_x64.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "windowsdesktop-runtime-6.0.25-win-x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "windowsdesktop-runtime-6.0.25-win-x64.exe", "/install /quiet /norestart", [], null, true),
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Sentinel System Driver Installer 7.5.0.exe", "/S /v/qn", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Sentinel System Driver Installer 7.6.1.exe", "/S /v/qn", [], null, true),
                        new IAtodOperation.CreateRegistryKey(Microsoft.Win32.Registry.CurrentUser, "Software\\Freedom Scientific", RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Eloquence.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\Utilities.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\fsElevation.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\fsSynth.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\UIAHooks.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        // NOTE: FSReader should not be installed if it is not in the layout for a language
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\FSReader.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        // NOTE: TableOfContents should not be installed if it is not in the layout for a language
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\TableOfContents.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "FSOmnipage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "FSOmnipageAsian.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x86\\FSOcr.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\FSOcr.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x86\\FSOcrTombstone.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\FSOcrTombstone.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FSSupportTool.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\ErrorReporting.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\TouchServer.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\FusionInterface.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\Authorization.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\RdpSupport.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\FSCam.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\HookManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\AccEventCache.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\GlobalHooksDispatcher.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\GateManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\Telemetry.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\VoiceAssistant.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\JAWSBase.msi", new() { { "ARPSYSTEMCOMPONENT", "1" }, { "SETUP_LANGUAGES", "enu" }, { "TANDEM", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\JAWSLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" }, { "PRIMARY_LANGUAGE", "1" }, { "TANDEM", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\JAWSStart.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "arb\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "cht\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "csy\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "dan\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "deu\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "eng\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "esn\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "eti\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "fin\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "fra\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "heb\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "hun\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "isl\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ita\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "jpn\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "kkz\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "kor\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "lvi\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "mki\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "nld\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "nor\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "plk\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ptb\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "rus\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "sky\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "sqi\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "sve\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "trk\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ukr\\x64\\ZoomTextLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x86\\KeyboardManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "VocalizerExpressive-2.2.206-enu-Tom-Compact-enu.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "VocalizerExpressive-2.2.206-enu-Zoe-Compact-enu.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\ZoomText.msi", new() { { "ARPSYSTEMCOMPONENT", "1" }, { "PRODUCT_TYPE", "2" }, { "PRIMARY_LANGUAGE", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\Fusion.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        // NOTE: based on early experience, we need to run FusionBundle.exe after completing the install to apply default settings, etc.  In our experience, this was required for JAWS so we do it for ZoomText and Fusion as well.
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "FusionBundle.exe", "/Type silent", [], null, true),
                    ];
                break;
            case IdValue.Ghotit:
                installOperations =
                    [
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "Ghotit-Real-Writer-and-Reader-10-64bit.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Grid3:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "windowsdesktop-runtime-7.0.9-win-x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "Grid 3.0.86.3.msi", null, RequiresElevation: true),
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "US Voices.zip", AtodPath.CreateTemporaryFolderForNewPathKey("usvoicesfolder")),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("usvoicesfolder"), "Grid 3 English (United States).Ryan - American English Male Adult.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("usvoicesfolder"), "Grid 3 English (United States).Heather - American English Female Adult.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Inspiration:
                installOperations =
                    [
                        //// enterprise-style install (preliminary and probably incomplete)
                        //new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "Inspiration+10+msi.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Inspiration 10 msi\\Inspiration 10.msi", null, RequiresElevation: true),
                        //
                        // TEMPORARY UNOFFICIAL TEST -- NOT FOR WIDE RELEASE
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "insp10_ESD.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "setup.exe", "-p network.ini", [], null, true),
                    ];
                break;
            case IdValue.Jaws:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "J2024.2312.53.400-any.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        ////
                        //// NOTE: for MicrosoftEdgeWebView2RuntimeInstaller deployment details, see: https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution
                        ////new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "MicrosoftEdgeWebView2RuntimeInstallerX86.exe", "/silent /install",
                        ////    [
                        ////        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        ////        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        ////    ],
                        ////    null, true), // Intel 32-bit
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "MicrosoftEdgeWebView2RuntimeInstallerX64.exe", "/silent /install",
                        //    [
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //    ],
                        //    null, true),   // Intel 64-bit
                        ////
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "vcredist2022_x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "vcredist2022_x64.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "windowsdesktop-runtime-6.0.25-win-x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "windowsdesktop-runtime-6.0.25-win-x64.exe", "/install /quiet /norestart", [], null, true),
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Sentinel System Driver Installer 7.5.0.exe", "/S /v/qn", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Sentinel System Driver Installer 7.6.1.exe", "/S /v/qn", [], null, true),
                        new IAtodOperation.CreateRegistryKey(Microsoft.Win32.Registry.CurrentUser, "Software\\Freedom Scientific", RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x86\\Eloquence.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\Utilities.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\fsElevation.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\fsSynth.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\UIAHooks.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\HookManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\AccEventCache.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\GlobalHooksDispatcher.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\GateManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        // NOTE: FSReader should not be installed if it is not in the layout for a language
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FSReader.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        // NOTE: TableOfContents should not be installed if it is not in the layout for a language
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\TableOfContents.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x86\\FSOmnipage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x86\\FSOmnipageAsian.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x86\\FSOcr.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FSOcr.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x86\\FSOcrTombstone.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FSOcrTombstone.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FSSupportTool.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\ErrorReporting.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\TouchServer.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FusionInterface.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\Authorization.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x86\\KeyboardManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FSCam.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\RdpSupport.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\Telemetry.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\VoiceAssistant.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\JAWSBase.msi", new() { { "ARPSYSTEMCOMPONENT", "1" }, { "ENABLE_UNTRUSTED_FONTS_EXCEPTION", "1" }, { "SETUP_LANGUAGES", "enu" }, { "TANDEM", "1" }, { "REMOTE_ONLY", "0" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\JAWSLanguage.msi", new() { { "ARPSYSTEMCOMPONENT", "1" }, { "PRIMARY_LANGUAGE", "1" }, { "TANDEM", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\JAWSStart.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        // NOTE: based on early experience, we need to run "JAWS setup package.exe" after completing the install to apply default settings, etc.
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "JAWS setup package.exe", "/Type silent", [], null, true),
                    ];
                break;
            case IdValue.Kurzweil1000:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "K1000_k1_1417_W_D.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        // NOTE: we should auto-detect if .NET 4.0 Full (plus KB2468871) is already installed (and skip it otherwise); this should not technically be necessary on the versions of Windows 10/11 (i.e. beyond 1903+) that AToD supports
                        // see: https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{32D7E3D1-C9DF-4FA6-9F9B-4D5117AB2917}\\dotNetFx40_Full_x86_x64.exe", "/q /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{1c29ecbe-6b15-441c-afbe-0e1469fc74b6}\\NDP40-KB2468871-v2-x86.exe", "/q /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        // NOTE: we may want to detect if this package is already installed (and skip it otherwise)
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{957E6AC2-3CE2-4802-B460-2CAFFC93DB6A}\\vcredist_x86.exe", "/install /quiet /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        // NOTE: we may want to detect if this package is already installed (and skip it otherwise)
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{2C3CC737-20F3-4A85-BB50-C7CCC74D8B50}\\vcredist_x64.exe", "/install /quiet /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Software\\Kurzweil 1000 v.14 Demo.msi", new() { { "AUTO", "1" } }, RequiresElevation: true),
                    ];
                break;
            case IdValue.Kurzweil3000:
                installOperations =
                    [
                        // NOTE: in v22, the "weblicense" ZIP is identical to the "standard" license ZIP except for the setup.exe file (which we don't use); the MSI file takes the type of license as a parameter
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "K3000_k3_2210_W_WebLicense.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        // NOTE: we should auto-detect if .NET 8 is already installed (and skip it otherwise); this should not technically be necessary on the versions of Windows 10/11 (i.e. beyond 1903+) that AToD supports
                        // see: https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{2040DEBA-E3A0-4A5A-824B-9A36E43EB33B}\\ndp48-x86-x64-allos-enu.exe", "/quiet /AcceptEULA /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{2040DEBA-E3A0-4A5A-824B-9A36E43EB33B}\\ndp48-x86-x64-allos-enu.exe", "/quiet /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        // NOTE: we may want to detect if this package is already installed (and skip it otherwise)
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{957E6AC2-3CE2-4802-B460-2CAFFC93DB6A}\\vcredist_x86.exe", "/install /quiet /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        // NOTE: we may want to detect if this package is already installed (and skip it otherwise)
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{73BD27F1-D526-43DB-8108-95B74D9299B9}\\VC_redist.x86.exe", "/install /quiet /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        // NOTE: we may want to detect if this package is already installed (and skip it otherwise)
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Software\\ISSetupPrerequisites\\{B88FD2D7-C8D4-4019-95CF-A011FE2096E6}\\VC_redist.x64.exe", "/install /quiet /norestart", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Software\\Kurzweil 3000 v.22.msi", new() { { "AUTO", "1" }, { "EDITION", "\"web\"" }, { "AUTOUPDATE", "0" }  }, RequiresElevation: true),
                        // NOTE: if the user needed to install the standalone-license verison, we would pass "standalone" as the EDITION parameter instead of "web"
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Software\\Kurzweil 3000 v.22.msi", new() { { "AUTO", "1" }, { "EDITION", "\"standalone\"" }, { "AUTOUPDATE", "0" } }, RequiresElevation: true),
                    ];
                break;
            case IdValue.Magic:
                installOperations =
                    [
                        // Intel 64-bit
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", "/type silent", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                        //
                        // Intel 32-bit
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", "/type silent", STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                    ];
                break;
            case IdValue.NaturalReader:
                installOperations =
                    [
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "naturalreader16.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Nvda:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "nvda_2023.3.1.exe", "--minimal --install-silent", [], null, true),
                    ];
                break;
            case IdValue.OpenBook:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "OPEN-BOOK  OB9.0.1768.403-enu.exe", "/type silent", [], null, true),
                    ];
                break;
            case IdValue.PurpleP3:
                installOperations =
                    [
                        // Intel 64-bit (presumably, based on the 64b filename)
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "Purple_P3_9.6.1-3513-64b.msi", new() { { "WRAPPED_ARGUMENTS", "\"--mode unattended\"" } }, RequiresElevation: true),
                    ];
                break;
            case IdValue.ReadAndWrite:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "setup.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "setup.msi", null, RequiresElevation: true),
                    ];
                break;
            case IdValue.SmyleMouse:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "SmyleMouse_setup.exe", "/NORESTART /VERYSILENT /RESTARTEXITCODE=" + STANDARD_REBOOT_REQUIRED_EXIT_CODE.ToString(), [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                    ];
                break;
            case IdValue.SofType:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "SofTypeSetup5.0.1074.0.exe", "/qn", [], null, true),
                    ];
                break;
            case IdValue.SuperNovaMagnifier:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "SuperNova_Magnifier_22.04_English_(United_States)_NETWORK.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        //
                        // NOTE: for MicrosoftEdgeWebView2RuntimeInstaller deployment details, see: https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\MicrosoftEdgeWebView2RuntimeInstallerX86.exe", "/silent /install",
                        //    [
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //    ],
                        //    null, true), // Intel 32-bit
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\MicrosoftEdgeWebView2RuntimeInstallerX64.exe", "/silent /install",
                            [
                                new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                                new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                            ],
                            null, true),   // Intel 64-bit
                        //
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\vc_redist.x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "SuperNova\\SnovaMag.msi", new() { { "SETUPLANGLIST", "\"00001\"" } }, RequiresElevation: true), // default language: US-EN
                        // NOTE: the network installation instructions say to ensure that the system is restarted following installation
                    ];
                break;
            case IdValue.SuperNovaMagnifierAndSpeech:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "SuperNova_Magnifier_&_Speech_22.04_English_(United_States)_NETWORK.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        //
                        // NOTE: for MicrosoftEdgeWebView2RuntimeInstaller deployment details, see: https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\MicrosoftEdgeWebView2RuntimeInstallerX86.exe", "/silent /install",
                        //    [
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //        new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                        //    ],
                        //    null, true), // Intel 32-bit
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\MicrosoftEdgeWebView2RuntimeInstallerX64.exe", "/silent /install",
                            [
                                new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.LocalMachine, SubKeyName: "SOFTWARE\\WOW6432Node\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                                new IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion(RootKey: Microsoft.Win32.Registry.CurrentUser, SubKeyName: "Software\\Microsoft\\EdgeUpdate\\Clients\\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}", "pv"),
                            ],
                            null, true),   // Intel 64-bit
                        //
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "3rdParty\\vc_redist.x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Sam\\Sam.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Sam\\Sam64Addon.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices\\VocExprCommonFiles.msi", null, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices/English_US_Allison.msi", null, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices/English_US_Ava.msi", null, RequiresElevation: true),
                        //new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices/English_US_Samantha.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices\\English_US_Tom.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Voices\\Orpheus.msi", null, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "SuperNova\\SnovaReadMag.msi", new() { { "SETUPLANGLIST", "\"00001\"" } }, RequiresElevation: true), // default language: US-EN
                        // NOTE: the network installation instructions say to ensure that the system is restarted following installation
                    ];
                break;
            case IdValue.WordQ:
                installOperations =
                [
                    new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "WordQ5S_NA_EN.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "AcapelaCore\\Acapela Text to Speech for WordQ 5 (Core).msi", null, RequiresElevation: true),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "AcapelaNA\\Acapela Text to Speech for WordQ 5(North America).msi", null, RequiresElevation: true),
//                    // NOTE: installing we could also run NA_Std_EN_wdqsetup.exe instead, but using the MSI directly is preferred
//                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "WordQ\\NA_Std_EN_wdqsetup.exe", "/S /v/qn", [], null, RequiresElevation: true),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "WordQ\\WordQ 5.msi", null, RequiresElevation: true),
                ];
                break;
            case IdValue.Wynn:
                installOperations =
                    [
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "WYNN7.0.084-32bit-64bit", "/type Silent", [], null, true),
                    ];
                break;
            case IdValue.ZoomText:
                installOperations =
                    [
                        new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "ZT2024.2312.26.400.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "vcredist2022_x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "vcredist2022_x64.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "windowsdesktop-runtime-6.0.25-win-x86.exe", "/install /quiet /norestart", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "windowsdesktop-runtime-6.0.25-win-x64.exe", "/install /quiet /norestart", [], null, true),
                        //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Sentinel System Driver Installer 7.5.0.exe", "/S /v/qn", [], null, true),
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "Sentinel System Driver Installer 7.6.1.exe", "/S /v/qn", [], null, true),
                        new IAtodOperation.CreateRegistryKey(Microsoft.Win32.Registry.CurrentUser, "Software\\Freedom Scientific", RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\fsElevation.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\FSSupportTool.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\ErrorReporting.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\Authorization.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "KeyboardManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\fsSynth.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "x64\\RdpSupport.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "VocalizerExpressive-2.2.206-enu-Tom-Compact-enu.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "VocalizerExpressive-2.2.206-enu-Zoe-Compact-enu.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\HookManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\Telemetry.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\AccEventCache.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\GlobalHooksDispatcher.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\GateManager.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\UIAHooks.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\VoiceAssistant.msi", new() { { "ARPSYSTEMCOMPONENT", "1" } }, RequiresElevation: true),
                        new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "enu\\x64\\ZoomText.msi", new() { { "ARPSYSTEMCOMPONENT", "1" }, { "PRODUCT_TYPE", "2" }, { "PRIMARY_LANGUAGE", "1" }, { "SETUP_LANGUAGES", "enu" } }, RequiresElevation: true),
                        // NOTE: based on early experience, we need to run ZoomTextSetupPackage.exe after completing the install to apply default settings, etc.  In our experience, this was required for JAWS so we do it for ZoomText and Fusion as well.
                        new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("setupfolder"), "ZoomTextSetupPackage.exe", "/Type silent", [], null, true),
                    ];
                break;
            default:
                throw new Exception("invalid code path");
        }

        List<IAtodOperation> result = new();
        result.AddRange(downloadOperations);
        result.AddRange(installOperations);

        return result;
    }

    private List<IAtodOperation> GetInstallDownloadOperations()
    {
        List<IAtodOperation> result = new();

        var downloadFilesInfo = this.GetDownloadFilesInfo();

#if USE_OFFLINE_BASE_PATH
        for (var index = 0; index < downloadFilesInfo.Count; index += 1)
        {
            var downloadFileInfo = downloadFilesInfo[index];
            var destinationPath = index switch
            {
                0 => AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"),
                _ => AtodPath.ExistingPathKey("downloadfolder"),
            };
            Uri uriToOfflineFile = new Uri(OFFLINE_BASE_URI, downloadFileInfo.CdnRelativePath);
            var operation = new IAtodOperation.Download(downloadFileInfo.UriToOfflineFile, destinationPath, downloadFileInfo.Filename, downloadFileInfo.OptionalChecksum);
            result.Add(operation);
        }
#elif USE_ATOD_CDN
        for (var index = 0; index < downloadFilesInfo.Count; index += 1)
        {
            var downloadFileInfo = downloadFilesInfo[index];
            var destinationPath = index switch
            {
                0 => AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"),
                _ => AtodPath.ExistingPathKey("downloadfolder"),
            };
            Uri uriToCdnFile = new Uri(ATOD_CDN_BASE_URI, downloadFileInfo.CdnRelativePath);
            var operation = new IAtodOperation.Download(uriToCdnFile, destinationPath, downloadFileInfo.Filename, downloadFileInfo.OptionalChecksum);
            result.Add(operation);
        }
#else
        for (var index = 0; index < downloadFilesInfo.Count; index += 1)
        {
            var downloadFileInfo = downloadFilesInfo[index];
            var destinationPath = index switch
            {
                0 => AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"),
                _ => AtodPath.ExistingPathKey("downloadfolder"),
            };
            var operation = new IAtodOperation.Download(downloadFileInfo.DirectDownloadUri, destinationPath, downloadFileInfo.Filename, downloadFileInfo.OptionalChecksum);
            result.Add(operation);
        }
#endif

        return result;
    }

    private List<(Uri DirectDownloadUri, string CdnRelativePath, string Filename, IAtodChecksum? OptionalChecksum)> GetDownloadFilesInfo()
    {
        return this.Id switch
        {
            IdValue.AudioNotetaker =>
                [(
                    DirectDownloadUri: new Uri("https://downloads.sonocent.com/AudioNote/Windows/SonocentAudioNotetaker_Win_5.3.29.0.exe"),
                    CdnRelativePath: "audionotetaker/SonocentAudioNotetaker_Win_5.3.29.0.exe",
                    Filename: "SonocentAudioNotetaker_Win_5.3.29.0.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([119, 160, 114, 146, 142, 159, 151, 119, 150, 161, 148, 137, 41, 174, 198, 168, 160, 21, 212, 107, 10, 113, 166, 62, 206, 185, 227, 3, 3, 94, 134, 60])
                )],
            IdValue.AutoHotkey => 
                [(
                    // NOTE: we had problems with the AutoHotkey CDN returning HTTP 403 in testing; for now, we're using the AToD CDN instead
                    //DirectDownloadUri: new Uri("https://www.autohotkey.com/download/2.0/AutoHotkey_2.0.11_setup.exe"),
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/autohotkey/AutoHotkey_2.0.11_setup.exe"), 
                    CdnRelativePath: "autohotkey/AutoHotkey_2.0.11_setup.exe",
                    Filename: "AutoHotkey_2.0.11_setup.exe", 
                    OptionalChecksum: new IAtodChecksum.Sha256([81, 10, 131, 59, 221, 15, 137, 108, 195, 152, 234, 174, 79, 244, 117, 245, 183, 207, 227, 118, 73, 239, 191, 100, 123, 80, 210, 30, 68, 35, 148, 185])
                )],
            IdValue.BuildABoard =>
                [(
                    DirectDownloadUri: new Uri("https://www.imgpresents.com/downloads/secure/bab220r7_win_7-11.exe"),
                    CdnRelativePath: "buildaboard/bab220r7_win_7-11.exe",
                    Filename: "bab220r7_win_7-11.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([5, 119, 199, 0, 179, 224, 50, 132, 212, 73, 223, 118, 101, 145, 65, 143, 217, 30, 217, 149, 1, 160, 175, 154, 163, 91, 40, 217, 177, 212, 230, 61])
                )],
            IdValue.CameraMouse =>
                [(
                    DirectDownloadUri: new Uri("http://www.cameramouse.org/downloads/CameraMouse2018Installer.exe"),
                    CdnRelativePath: "cameramouse/CameraMouse2018Installer.exe",
                    Filename: "CameraMouse2018Installer.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([46, 79, 235, 219, 64, 193, 9, 39, 54, 33, 199, 223, 31, 66, 226, 80, 196, 144, 214, 172, 6, 147, 65, 80, 239, 220, 234, 126, 200, 111, 68, 52])
                )],
            IdValue.ClaroRead =>
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/claroread/ClaroRead-12.0.29-auth-bundle.zip"),
                    CdnRelativePath: "claroread/ClaroRead-12.0.29-auth-bundle.zip",
                    Filename: "ClaroRead-12.0.29-auth-bundle.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([71, 107, 184, 235, 96, 196, 0, 70, 56, 229, 227, 209, 126, 188, 46, 17, 211, 118, 45, 14, 31, 184, 124, 58, 145, 132, 207, 46, 211, 194, 176, 120])
                )],
            IdValue.ClaroReadSe =>
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/claroreadse/ClaroReadSE-12.0.29-auth.zip"),
                    CdnRelativePath: "claroreadse/ClaroReadSE-12.0.29-auth.zip",
                    Filename: "ClaroReadSE-12.0.29-auth.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([10, 216, 206, 64, 233, 62, 147, 47, 173, 200, 128, 149, 102, 213, 0, 140, 116, 0, 210, 33, 111, 211, 74, 70, 140, 74, 135, 26, 106, 206, 3, 126])
                )],
            IdValue.ClickNType =>
                [(
                    // NOTE: this MSI is not digitally signed and should not be included in the catalog until it can be authenticated (and even then, it should probably be forced to use UAC individually for consumers)
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/clickntype/CNTsetup.msi"),
                    CdnRelativePath: "clickntype/CNTsetup.msi",
                    Filename: "CNTsetup.msi",
                    OptionalChecksum: new IAtodChecksum.Sha256([247, 39, 177, 189, 83, 5, 117, 0, 156, 194, 197, 39, 178, 75, 17, 158, 11, 182, 44, 216, 240, 103, 55, 228, 156, 122, 103, 243, 197, 141, 49, 102])
                )],
            IdValue.ComfortOsk =>
                [(
                    //DirectDownloadUri: new IAtodOperation.Download(new Uri("https://www.comfortsoftware.com/download/ComfortOSKSetup.exe"),
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/comfortosk/ComfortOSKSetup.exe"),
                    CdnRelativePath: "comfortosk/ComfortOSKSetup.exe",
                    Filename: "ComfortOSKSetup.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([47, 50, 152, 31, 84, 136, 66, 70, 235, 149, 2, 171, 74, 145, 63, 162, 219, 199, 185, 249, 179, 244, 0, 127, 24, 199, 74, 255, 51, 29, 161, 34])
                )],
            IdValue.Communicator5 =>
                [(
                    DirectDownloadUri: new Uri("https://download.mytobiidynavox.com/Communicator/software/5.6.1/TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe"),
                    CdnRelativePath: "communicator5/TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe",
                    Filename: "TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([166, 250, 23, 4, 104, 200, 73, 99, 181, 133, 233, 25, 48, 23, 179, 100, 68, 37, 145, 200, 35, 192, 159, 168, 164, 220, 248, 225, 14, 66, 57, 31])
                )],
            IdValue.CoWriter =>
                [(
                    DirectDownloadUri: new Uri("http://donjohnston.com/wp-content/downloads/products/cowriter-universal-desktop.zip"),
                    CdnRelativePath: "cowriter/cowriter-universal-desktop.zip",
                    Filename: "cowriter-universal-desktop.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([161, 17, 99, 194, 63, 214, 199, 187, 55, 65, 121, 192, 191, 184, 150, 177, 4, 92, 191, 174, 53, 80, 60, 209, 30, 46, 11, 137, 44, 250, 65, 160])
                )],
            IdValue.DolphinScreenReader =>
                [(
                    DirectDownloadUri: new Uri("https://yourdolphin.com/downloads/product?pvid=552&lid=2&network=true"),
                    CdnRelativePath: "dolphinscreenreader/ScreenReader_22.04_English_(United_States)_NETWORK.zip",
                    Filename: "ScreenReader_22.04_English_(United_States)_NETWORK.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([10, 9, 68, 124, 151, 69, 51, 35, 52, 38, 250, 19, 105, 129, 188, 249, 101, 101, 1, 43, 207, 49, 10, 109, 1, 0, 123, 94, 216, 152, 46, 232])
                )],
            IdValue.Dragger =>
                [(
                    DirectDownloadUri: new Uri("https://orin.com/binaries/DraggerSetup2.0.1350.0.exe"),
                    CdnRelativePath: "dragger/DraggerSetup2.0.1350.0.exe",
                    Filename: "DraggerSetup2.0.1350.0.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([192, 106, 159, 37, 241, 148, 202, 37, 180, 183, 125, 200, 244, 122, 178, 4, 174, 80, 91, 54, 68, 226, 81, 205, 0, 73, 149, 110, 226, 93, 151, 232])
                )],
            IdValue.DuxburyBrailleTranslator =>
                [(
                    DirectDownloadUri: new Uri("https://www.duxburysystems.org/downloads/dbt_12.7/dbt-1207sr2.msi"),
                    CdnRelativePath: "duxburybrailletranslator/dbt-1207sr2",
                    Filename: "dbt-1207sr2",
                    OptionalChecksum: new IAtodChecksum.Sha256([151, 40, 230, 165, 225, 205, 191, 136, 11, 12, 138, 111, 239, 234, 114, 123, 123, 237, 147, 63, 157, 212, 117, 13, 240, 178, 130, 153, 39, 151, 101, 241])
                )],
            IdValue.Equatio =>
                [(
                    DirectDownloadUri: new Uri("https://fastdownloads2.texthelp.com/equatio_desktop/installers/latest/windows/Equatio.zip"),
                    CdnRelativePath: "equatio/dbt-1207sr2/Equatio.zip",
                    Filename: "Equatio.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([116, 229, 253, 241, 141, 136, 187, 225, 133, 138, 98, 97, 112, 73, 138, 53, 7, 236, 229, 105, 212, 104, 4, 186, 4, 227, 224, 168, 95, 184, 52, 18])
                )],
            IdValue.FreeVirtualKeyboard =>
                [(
                    DirectDownloadUri: new Uri("https://freevirtualkeyboard.com/FreeVKSetup.zip"),
                    CdnRelativePath: "freevirtualkeyboard/FreeVKSetup.zip",
                    Filename: "FreeVKSetup.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([238, 198, 6, 70, 240, 98, 164, 244, 37, 131, 145, 185, 68, 115, 166, 20, 49, 54, 88, 24, 9, 138, 112, 185, 213, 96, 166, 194, 130, 116, 28, 127])
                )],
            IdValue.Fusion =>
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/fusion/F2024.2312.8.400.zip"),
                    CdnRelativePath: "fusion/F2024.2312.8.400.zip",
                    Filename: "F2024.2312.8.400.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([209, 96, 112, 214, 229, 63, 194, 206, 217, 63, 139, 224, 91, 194, 33, 197, 137, 179, 81, 243, 64, 67, 2, 240, 225, 71, 59, 90, 249, 250, 50, 53])
                )],
            IdValue.Ghotit =>
                [(
                    // Intel 64-bit
                    DirectDownloadUri: new Uri("https://spelling.ghotit.com/install/win-10/msi/Ghotit-Real-Writer-and-Reader-10-64bit.msi"),
                    CdnRelativePath: "ghotit/Ghotit-Real-Writer-and-Reader-10-64bit.msi",
                    Filename: "Ghotit-Real-Writer-and-Reader-10-64bit.msi",
                    OptionalChecksum: new IAtodChecksum.Sha256([165, 66, 81, 47, 163, 30, 231, 134, 25, 221, 122, 94, 103, 130, 246, 156, 201, 152, 13, 137, 126, 21, 228, 137, 164, 133, 76, 45, 157, 77, 54, 78])
                )],
            IdValue.Grid3 =>
                [(
                    // Grid 3 (main MSI)
                    DirectDownloadUri: new Uri("https://downloads.sensorysoftware.com/public/msi/Grid%203.0.86.3.msi"),
                    CdnRelativePath: "grid3/Grid%203.0.86.3.msi",
                    Filename: "Grid 3.0.86.3.msi",
                    OptionalChecksum: new IAtodChecksum.Sha256([91, 254, 58, 169, 36, 18, 100, 76, 184, 37, 148, 63, 111, 225, 203, 191, 25, 62, 197, 45, 237, 118, 125, 153, 60, 66, 100, 172, 1, 136, 211, 138])
                ),
                (
                    // US Voices (ZIP)
                    DirectDownloadUri: new Uri("https://downloads.sensorysoftware.com/public/msi/US%20Voices.zip"),
                    CdnRelativePath: "grid3/US%20Voices.zip",
                    Filename: "US Voices.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([65, 156, 193, 205, 221, 90, 13, 60, 204, 59, 87, 223, 246, 103, 215, 38, 150, 253, 2, 83, 210, 127, 63, 217, 117, 130, 230, 239, 112, 126, 64, 36])
                ),
                (
                    // .NET Framework 7.0.9 x86 (Windows desktop) runtime
                    DirectDownloadUri: new Uri("https://dotnetcli.azureedge.net/dotnet/WindowsDesktop/7.0.9/windowsdesktop-runtime-7.0.9-win-x86.exe"),
                    CdnRelativePath: "dotnet/windowsdesktop-runtime-7.0.9-win-x86.exe",
                    Filename: "windowsdesktop-runtime-7.0.9-win-x86.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([220, 87, 138, 3, 92, 45, 68, 110, 163, 41, 255, 130, 11, 120, 112, 190, 20, 28, 10, 98, 1, 243, 204, 115, 208, 99, 33, 106, 186, 47, 67, 24])
                )],
            IdValue.Inspiration =>
                //// enterprise-style install (preliminary and probably incomplete)
                //[(
                //    DirectDownloadUri: new Uri("https://inspiration10installers.s3-eu-west-1.amazonaws.com/Inspiration+10+msi.zip"),
                //    CdnRelativePath: "inspiration/Inspiration+10+msi.zip",
                //    Filename: "Inspiration+10+msi.zip",
                //    OptionalChecksum: new IAtodChecksum.Sha256([105, 238, 162, 228, 2, 22, 104, 184, 217, 57, 182, 10, 202, 236, 99, 12, 223, 211, 161, 95, 31, 241, 73, 215, 84, 25, 188, 32, 251, 136, 211, 190])
                //)],
                //
                // TEMPORARY UNOFFICIAL TEST -- NOT FOR WIDE RELEASE
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/inspiration/insp10_ESD.zip"),
                    CdnRelativePath: "inspiration/insp10_ESD.zip",
                    Filename: "insp10_ESD.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([221, 64, 18, 174, 128, 119, 237, 152, 101, 202, 151, 161, 27, 185, 77, 237, 220, 141, 120, 3, 211, 145, 210, 113, 43, 213, 207, 186, 51, 117, 31, 164])
                )],
            IdValue.Jaws =>
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/jaws/J2024.2312.53.400-any.zip"),
                    CdnRelativePath: "jaws/J2024.2312.53.400-any.zip",
                    Filename: "J2024.2312.53.400-any.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([115, 195, 31, 0, 11, 117, 21, 119, 17, 70, 116, 82, 47, 92, 62, 240, 26, 212, 231, 161, 105, 164, 176, 53, 98, 114, 204, 18, 152, 189, 126, 199])
                )],
            IdValue.Kurzweil1000 =>
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/kurzweil1000/K1000_k1_1417_W_D.zip"),
                    CdnRelativePath: "kurzweil1000/K1000_k1_1417_W_D.zip",
                    Filename: "K1000_k1_1417_W_D.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([186, 115, 126, 175, 156, 118, 24, 162, 240, 191, 202, 184, 126, 89, 208, 39, 92, 213, 110, 206, 9, 123, 128, 171, 221, 205, 115, 216, 129, 255, 69, 22])
                )],
            IdValue.Kurzweil3000 =>
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/kurzweil3000/K3000_k3_2210_W_WebLicense.zip"),
                    CdnRelativePath: "kurzweil3000/K3000_k3_2210_W_WebLicense.zip",
                    Filename: "K3000_k3_2210_W_WebLicense.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([233, 155, 244, 12, 19, 98, 146, 91, 16, 156, 0, 1, 3, 80, 241, 250, 147, 59, 234, 88, 203, 73, 1, 146, 116, 213, 126, 85, 121, 16, 18, 39])
                )],
            IdValue.Magic =>
                [(
                    // Intel 64-bit
                    DirectDownloadUri: new Uri("https://magic15.vfo.digital/1502013NT2244W/M15.0.2014.400-enu-x64.exe"),
                    CdnRelativePath: "magic/M15.0.2014.400-enu-x64.exe",
                    //// Intel 32-bit
                    //DirectDownloadUri: new Uri("https://magic15.vfo.digital/1502013NT2244W/M15.0.2014.400-enu-x86.exe"),
                    //CdnRelativePath: "magic/M15.0.2014.400-enu-x86.exe",
                    Filename: "M15.0.2014.400-enu-x64.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([117, 43, 40, 31, 213, 216, 116, 36, 163, 80, 21, 225, 236, 198, 3, 98, 216, 80, 190, 74, 152, 147, 124, 179, 180, 103, 218, 218, 33, 248, 175, 208])
                )],
            IdValue.NaturalReader =>
                [(
                    DirectDownloadUri: new Uri("https://wwwnaturalreaderscom.s3.amazonaws.com/software/naturalreader16.msi"),
                    CdnRelativePath: "naturalreader/naturalreader16.msi",
                    Filename: "naturalreader16.msi",
                    OptionalChecksum: new IAtodChecksum.Sha256([29, 211, 243, 223, 32, 180, 17, 179, 184, 63, 171, 137, 56, 20, 49, 222, 91, 135, 29, 228, 52, 242, 201, 176, 218, 75, 198, 83, 106, 142, 149, 210])
                )],
            IdValue.Nvda =>
                [(
                    DirectDownloadUri: new Uri("https://www.nvaccess.org/download/nvda/releases/2023.3.1/nvda_2023.3.1.exe"),
                    CdnRelativePath: "nvda/nvda_2023.3.1.exe",
                    Filename: "nvda_2023.3.1.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([181, 55, 16, 36, 104, 67, 106, 185, 62, 15, 230, 60, 247, 140, 138, 220, 84, 66, 235, 190, 208, 88, 146, 119, 212, 92, 60, 185, 196, 239, 140, 114])
                )],
            IdValue.OpenBook =>
                [(
                    DirectDownloadUri: new Uri("https://openbook9.0.vfo.digital/9017689VQUKK/OB9.0.1768.403-enu.exe"),
                    CdnRelativePath: "openbook/OPEN-BOOK  OB9.0.1768.403-enu.exe",
                    Filename: "OPEN-BOOK  OB9.0.1768.403-enu.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([171, 211, 166, 180, 232, 126, 163, 78, 209, 162, 175, 102, 3, 49, 94, 33, 132, 75, 115, 155, 183, 147, 73, 13, 147, 231, 206, 235, 206, 118, 160, 220])
                )],
            IdValue.PurpleP3 =>
                [(
                    DirectDownloadUri: new Uri("https://s3.amazonaws.com/PurpleDownloads/P3/Purple_P3_9.6.1-3513-64b.msi"),
                    CdnRelativePath: "purplep3/Purple_P3_9.6.1-3513-64b.msi",
                    Filename: "Purple_P3_9.6.1-3513-64b.msi",
                    OptionalChecksum: new IAtodChecksum.Sha256([150, 220, 62, 126, 42, 170, 35, 35, 144, 175, 63, 143, 147, 215, 67, 168, 203, 118, 93, 241, 10, 254, 67, 224, 147, 168, 255, 220, 225, 167, 117, 2])
                )],
            IdValue.ReadAndWrite =>
                [(
                    // US download
                    DirectDownloadUri: new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/us/setup.zip"),
                    CdnRelativePath: "readandwrite/us/setup.zip",
                    // UK download
                    //DirectDownloadUri: new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/uk/setup.zip"),
                    //CdnRelativePath: "readandwrite/uk/setup.zip",
                    Filename: "setup.zip",
                    OptionalChecksum: null
                )],
            IdValue.SmyleMouse =>
                [(
                    //DirectDownloadUri: new Uri("http://www.smylemouse.com/example_downloads_folder/SmyleMouse_setup.exe"),
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/smylemouse/SmyleMouse_setup.exe"),
                    CdnRelativePath: "smylemouse/SmyleMouse_setup.exe",
                    Filename: "SmyleMouse_setup.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([11, 230, 164, 22, 125, 9, 32, 251, 19, 236, 201, 60, 198, 123, 32, 134, 156, 40, 222, 214, 5, 7, 223, 7, 128, 222, 144, 7, 16, 225, 248, 94])
                )],
            IdValue.SofType =>
                [(
                    DirectDownloadUri: new Uri("https://orin.com/binaries/SofTypeSetup5.0.1074.0.exe"),
                    CdnRelativePath: "softype/SofTypeSetup5.0.1074.0.exe",
                    Filename: "SofTypeSetup5.0.1074.0.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([7, 194, 43, 36, 173, 85, 39, 1, 90, 164, 199, 126, 119, 42, 88, 220, 237, 100, 180, 138, 132, 56, 211, 126, 203, 95, 151, 150, 184, 245, 123, 31])
                )],
            IdValue.SuperNovaMagnifier =>
                [(
                    DirectDownloadUri: new Uri("https://yourdolphin.com/downloads/product?pvid=554&lid=2&network=true"),
                    CdnRelativePath: "supernovamagnifier/SuperNova_Magnifier_22.04_English_(United_States)_NETWORK.zip",
                    Filename: "SuperNova_Magnifier_22.04_English_(United_States)_NETWORK.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([75, 230, 63, 108, 233, 45, 206, 131, 184, 250, 237, 149, 69, 5, 36, 56, 41, 71, 213, 154, 133, 68, 32, 149, 167, 53, 179, 162, 124, 162, 227, 17])
                )],
            IdValue.SuperNovaMagnifierAndSpeech =>
                [(
                    DirectDownloadUri: new Uri("https://yourdolphin.com/downloads/product?pvid=556&lid=2&network=true"),
                    CdnRelativePath: "supernovamagnifierandspeech/SuperNova_Magnifier_&_Speech_22.04_English_(United_States)_NETWORK.zip",
                    Filename: "SuperNova_Magnifier_&_Speech_22.04_English_(United_States)_NETWORK.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([177, 168, 203, 125, 224, 250, 32, 161, 89, 193, 190, 62, 99, 104, 24, 41, 159, 73, 240, 102, 41, 211, 31, 89, 190, 117, 81, 139, 179, 211, 102, 148])
                )],
            IdValue.WordQ =>
                [(
                    //DirectDownloadUri: new Uri("https://downloads.quillsoft.ca/30days/WordQ5S_NA_EN.zip"), // NOTE: no zip file exists at this location today; only a file with the same name (but an .exe extension) exists, and it is a WinZip self-extracting ZIP file
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/wordq/WordQ5S_NA_EN.zip"),
                    CdnRelativePath: "wordq/WordQ5S_NA_EN.zip",
                    Filename: "WordQ5S_NA_EN.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([128, 129, 60, 178, 7, 32, 213, 225, 91, 158, 14, 85, 97, 185, 161, 14, 47, 70, 140, 48, 101, 45, 115, 189, 170, 142, 160, 31, 171, 229, 185, 132])
                )],
            IdValue.Wynn =>
                [(
                    DirectDownloadUri: new Uri("http://wynn.vfo.digital/jUq9U8C45c/WYNN7.0.084-32bit-64bit.exe"),
                    CdnRelativePath: "wynn/WYNN7.0.084-32bit-64bit.exe",
                    Filename: "WYNN7.0.084-32bit-64bit.exe",
                    OptionalChecksum: new IAtodChecksum.Sha256([12, 255, 64, 229, 245, 66, 23, 9, 199, 238, 187, 221, 32, 192, 55, 77, 22, 139, 41, 58, 116, 226, 38, 143, 162, 131, 247, 205, 72, 9, 160, 107])
                )],
            IdValue.ZoomText =>
                [(
                    DirectDownloadUri: new Uri("https://atod-cdn.raisingthefloor.org/zoomtext/ZT2024.2312.26.400.zip"),
                    CdnRelativePath: "zoomtext/ZT2024.2312.26.400.zip",
                    Filename: "ZT2024.2312.26.400.zip",
                    OptionalChecksum: new IAtodChecksum.Sha256([157, 16, 198, 230, 243, 66, 199, 30, 185, 0, 27, 62, 32, 52, 6, 59, 175, 254, 1, 43, 177, 125, 237, 198, 153, 74, 41, 210, 253, 71, 162, 72])
                )],
            _ => throw new Exception("invalid code path"),
        };
    }

    public List<IAtodOperation>? GetUninstallOperations()
    {
        List<IAtodOperation>? result;

//        const int STANDARD_REBOOT_REQUIRED_EXIT_CODE = unchecked((int)(uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS_REBOOT_REQUIRED);

        switch (this.Id)
        {
            case IdValue.AudioNotetaker:
                // NOTE: Sonocent Audio Notetaker has a "QuietUninstallString" registry entry, so we don't need to pass in "/quiet" as a flag
                result =
                    [
//                        new IAtodOperation.UninstallUsingRegistryUninstallString("{0e463382-de6d-4db1-bce4-b96d7c5f12f1}", [ new ISupplementalArgument.PostfixArgument("/quiet") ], null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{0e463382-de6d-4db1-bce4-b96d7c5f12f1}", null, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.AutoHotkey:
                // NOTE: AutoHotkey has a "QuietUninstallString" registry entry, so we don't need to pass in "/silent" as a flag
                result =
                    [
//                        new IAtodOperation.UninstallUsingRegistryUninstallString("AutoHotkey", [ new ISupplementalArgument.PostfixArgument("/silent") ], null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingRegistryUninstallString("AutoHotkey", null, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.BuildABoard:
                // as of 2024-01-19, there is no silent uninstall available for Build-A-Board
                result = null;
                //result =
                //    [
                //        // NOTE: the corresponding UninstallString (as of 2024-01-19) was "C:\Program Files (x86)\Build-A-Board\BIN\IMGUTIL.exe Uninstall12401", with no silent uninstallation option
                //        new IAtodOperation.UninstallUsingRegistryUninstallString("Build-A-Board", null, null, RequiresElevation: true),
                //    ];
                break;
            case IdValue.CameraMouse:
                // NOTE: CameraMouse has a "QuietUninstallString" registry entry, but it uses "/SILENT" instead of "/VERYSILENT" so we supplement it with "/VERYSILENT"
                result =
                    [
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{F5E6727D-0969-4C4A-A669-71F1A3913A03}}_is1", [ new ISupplementalArgument.PostfixArgument("/VERYSILENT") ], null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ClaroRead:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLARO_SOFTWARE_SCAN2TEXT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLARO_SOFTWARE_CLAROVIEW_SCREEN_READER_SCREEN_MARKER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLARO_SOFTWARE_CLAROIDEAS, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLARO_SOFTWARE_CAPTURE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLARO_SOFTWARE_CLAROREAD, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ClaroReadSe:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLARO_SOFTWARE_SCAN_SCREEN_PLUS, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLARO_SOFTWARE_CLAROREAD_SE, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ClickNType:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.CLICK_N_TYPE, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ComfortOsk:
                // NOTE: ComfortOsk has a "QuietUninstallString" registry entry, but it uses "/SILENT" instead of "/VERYSILENT" so we supplement it with "/VERYSILENT"
                result =
                    [
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{6EB17721-6249-417B-99B9-DAF3FD532955}_is1", [ new ISupplementalArgument.PostfixArgument("/VERYSILENT /NORESTART") ], null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Communicator5:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_COMMUNICATOR_5, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_PCS_FOR_COMMUNICATOR_5, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_US_ENGLISH_VOICES_FOR_TOBII_COMMUNICATOR, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_SONO_FLEX, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_SONO_KEY, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_SONO_LEXIS, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_SONO_PROMO_FOR_COMMUNICATOR, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_LITERAACY_US_EN, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TOBII_DYNAVOX_SYMBOLSTIX_2, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.CoWriter:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DON_JOHNSTON_CO_WRITER, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.DolphinScreenReader:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SCREEN_READER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_ORPHEUS, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM_VOCALIZER_EXPRESSIVE_ENGLISH_TOM, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM_VOCALIZER_EXPRESSIVE_VOCALIZER_EXPRESSIVE_COMMON_FILES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM_64_BIT_ADDON, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Dragger:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DRAGGER, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.DuxburyBrailleTranslator:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DUXBURY_BRAILLE_TRANSLATOR, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Equatio:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.EQUATIO, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.FreeVirtualKeyboard:
                // NOTE: FreeVirtualKeyboard has a "QuietUninstallString" registry entry, but it uses "/SILENT" instead of "/VERYSILENT" so we supplement it with "/VERYSILENT"
                // NOTE: the uninstaller is not indicating that the system should be restarted (using an exit code); the exit code is zero.
                result =
                    [
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{CA4F9519-1A83-4907-8651-F17073A0E1CE}_is1", [ new ISupplementalArgument.PostfixArgument("/VERYSILENT /NORESTART") ], null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Fusion:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELOQUENCE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_UTILITIES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELEVATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_SYNTH, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_UIA_HOOKS_1_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_FS_READER_3_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OMNIPAGE_20, null, RequiresElevation: true),
                        //new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OMNIPAGE_ASIAN_##, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_X86, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_X64, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X86, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X64, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_SUPPORT_TOOL, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ERROR_REPORTING, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TOUCH_SERVER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_FUSION_INTERFACE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_AUTHORIZATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_RDP_SUPPORT, null, RequiresElevation: true),
                        // NOTE: FREEDOM_SCIENTIFIC_USB_CAMERA_DRIVER is assumed to be the same package as "FSCam.msi"
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_USB_CAMERA_DRIVER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_HOOK_MANAGER_2_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ACC_EVENT_CACHE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_GLOBAL_HOOKS_DISPATCHER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_GATE_MANAGER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TELEMETRY, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VOICE_ASSISTANT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_2024_BASE, new() { { "FORCE_DEL_USERSETTINGS", "0" } }, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_2024_LANGUAGE_ENU, new() { { "FORCE_DEL_USERSETTINGS", "0" } }, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_START, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ARA, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_CHT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_CSY, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_DAN, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_DEU, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ENG, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ESN, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ETI, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_FIN, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_FRA, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_HEB, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_HUN, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ISL, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ITA, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_JPN, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_KKZ, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_KOR, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_LVI, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_MKI, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_NLD, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_NOR, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_PLK, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_PTB, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_RUS, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_SKY, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_SQI, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_SVE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_TRK, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_UKR, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_KEYBOARD_MANAGER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_TOM_COMPACT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_ZOE_COMPACT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_2004, new() { { "FORCE_DEL_USERSETTINGS", "0" }, { "PRODUCT_TYPE", "2" }, { "PRIMARY_LANGUAGE", "1" } }, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_FUSION_2024, null, RequiresElevation: true),
                        // NOTE: although this is not documented in the sample uninstall scripts from Freedom Scientific, we must run FusionBundle with "/uninstall /quiet" as arguments (i.e. the QuietUninstallString of "Freedom Scientific Fusion 2024")
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{ca9065af-0c32-414e-97c4-3c669b133d17}", null, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Ghotit:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.GHOTIT, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Grid3:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.SENSORY_SOFTWARE_GRID_3, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.SENSORY_SOFTWARE_RYAN_AMERICAN_ENGLISH_MALE_ADULT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.SENSORY_SOFTWARE_HEATHER_AMERICAN_ENGLISH_FEMALE_ADULT, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Inspiration:
                result =
                    [
                        //// enterprise-style uninstall (preliminary and probably incomplete)
                        //new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.TECHEDOLOGY_INSTALLER_FOR_INSPIRATION_10_DEPLOYMENT, null, RequiresElevation: true),
                        //
                        // TEMPORARY UNOFFICIAL TEST -- NOT FOR WIDE RELEASE
                        // NOTE: we would need to add the "/S" argument in the uninstallstring between the uninstall application name and the args (i.e. as the first argument), with a space after it if the pre-existing arguments were not an empty string
                        new IAtodOperation.UninstallUsingRegistryUninstallString("Inspiration 9", [ new ISupplementalArgument.PrefixArgument("/S") ], null, RequiresElevation: true),
                    ];
                //result = null;
                break;
            case IdValue.Jaws:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELOQUENCE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_UTILITIES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELEVATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_SYNTH, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_UIA_HOOKS_1_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_HOOK_MANAGER_2_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ACC_EVENT_CACHE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_GLOBAL_HOOKS_DISPATCHER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_GATE_MANAGER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_FS_READER_3_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OMNIPAGE_20, null, RequiresElevation: true),
                        //new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OMNIPAGE_ASIAN_##, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_X86, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_X64, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X86, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X64, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_SUPPORT_TOOL, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ERROR_REPORTING, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TOUCH_SERVER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_FUSION_INTERFACE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_AUTHORIZATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_KEYBOARD_MANAGER, null, RequiresElevation: true),
                        // NOTE: FREEDOM_SCIENTIFIC_USB_CAMERA_DRIVER is assumed to be the same package as "FSCam.msi"
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_USB_CAMERA_DRIVER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_RDP_SUPPORT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TELEMETRY, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VOICE_ASSISTANT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_2024_BASE, new() { { "FORCE_DEL_USERSETTINGS", "0" }, { "ENABLE_UNTRUSTED_FONTS_EXCEPTION", "1" }, { "TANDEM", "1" }, { "REMOTE_ONLY", "0" } }, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_2024_LANGUAGE_ENU, new() { { "FORCE_DEL_USERSETTINGS", "0" } }, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_JAWS_START, null, RequiresElevation: true),
                        // NOTE: although this is not documented in the sample uninstall scripts from Freedom Scientific, we must run "JAWS setup package.exe" with "/uninstall /quiet" as arguments (i.e. the QuietUninstallString of "Freedom Scientific JAWS 2024")
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{7a98cb34-9d9b-47e0-918b-d3d528ef75b7}", null, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Kurzweil1000:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.KURZWEIL_1000, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Kurzweil3000:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.KURZWEIL_3000, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Magic:
                result =
                    [
                        new IAtodOperation.UninstallUsingRegistryUninstallString("MAGic15.0", [ new ISupplementalArgument.PostfixArgument("/type silent") ], null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingRegistryUninstallString("FSReader3.0", [ new ISupplementalArgument.PostfixArgument("/type silent") ], null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.NaturalReader:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.NATURAL_READER, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Nvda:
                // NOTE: NVDA does not include the "/S" option (for Nullsoft "SILENT") in their UninstallString, so we force-add it as a parameter; this is fragile, and it should be removed if a QuietUninstallString is present, etc.
                result =
                    [
                        new IAtodOperation.UninstallUsingRegistryUninstallString("NVDA", [ new ISupplementalArgument.PostfixArgument("/S") ], null, RequiresElevation: true),
                    ];
                break;
            case IdValue.OpenBook:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELOQUENCE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_UTILITIES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELEVATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_FINE_READER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OMNIPAGE_19, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_X86, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_X64_OPENBOOK_9_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X86, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X64, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_USB_CAMERA_DRIVER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_BOOK_SEARCH, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TEXT_TO_AUDIO, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_BRAILLE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_IMPORT_PRINTER_1, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_IMPORT_PRINTER_2, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_IMPORT_PRINTER_3, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_WOW64_PROXY_OPENBOOK_9_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OPEN_BOOK_9_0, null, RequiresElevation: true),
                        // NOTE: we also run the final product uninstaller (postfixing "/type silent" to the command-line of the UninstallString)
                        new IAtodOperation.UninstallUsingRegistryUninstallString("OB9.0", [ new ISupplementalArgument.PostfixArgument("/type silent") ], null, RequiresElevation: true),
                    ];
                break;
            case IdValue.PurpleP3:
                result =
                    [
                        //new IAtodOperation.UninstallUsingRegistryUninstallString("Purple P3 9.6.1-3513", [ new ISupplementalArgument.PostfixArgument("--mode unattended") ], null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.PURPLE_P3, new() { { "WRAPPED_ARGUMENTS", "\"--mode unattended\"" } }, RequiresElevation: true),
                    ];
                break;
            case IdValue.ReadAndWrite:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.READ_AND_WRITE, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.SmyleMouse:
                // NOTE: SmyleMouse has a "QuietUninstallString" registry entry, but it uses "/SILENT" instead of "/VERYSILENT" so we supplement it with "/VERYSILENT"
                result =
                    [
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{09786633-20A6-48F4-932B-3AF58F730AD0}_is1", [ new ISupplementalArgument.PostfixArgument("/VERYSILENT /NORESTART") ], null, RequiresElevation: true),
                    ];
                break;
            case IdValue.SofType:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.SOFTYPE, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.SuperNovaMagnifier:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SUPERNOVA_MAGNIFIER, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.SuperNovaMagnifierAndSpeech:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SUPERNOVA_MAGNIFIER_AND_SPEECH, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_ORPHEUS, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM_VOCALIZER_EXPRESSIVE_ENGLISH_TOM, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM_VOCALIZER_EXPRESSIVE_VOCALIZER_EXPRESSIVE_COMMON_FILES, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM_64_BIT_ADDON, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DOLPHIN_SAM, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.WordQ:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.QUILLSOFT_WORDQ_5, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.QUILLSOFT_ACAPELA_TTS_FOR_WORDQ_5_CORE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.QUILLSOFT_ACAPELA_TTS_FOR_WORDQ_5_NORTH_AMERICA, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.Wynn:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELEVATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_IMPORT_PRINTER_1, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_IMPORT_PRINTER_2, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_IMPORT_PRINTER_3, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_FINE_READER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OMNIPAGE_16, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X86, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_USB_CAMERA_DRIVER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_BOOK_SEARCH, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TEXT_TO_AUDIO, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_DOCUMENT_SERVER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_WYNN_7_0, null, RequiresElevation: true),
                    ];
                break;
            case IdValue.ZoomText:
                result =
                    [
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ELEVATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_SUPPORT_TOOL, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ERROR_REPORTING, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_AUTHORIZATION, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_KEYBOARD_MANAGER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_SYNTH, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_RDP_SUPPORT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_TOM_COMPACT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_ZOE_COMPACT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_HOOK_MANAGER_2_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TELEMETRY, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ACC_EVENT_CACHE, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_GLOBAL_HOOKS_DISPATCHER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_GATE_MANAGER, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_UIA_HOOKS_1_0, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VOICE_ASSISTANT, null, RequiresElevation: true),
                        new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_ZOOMTEXT_2004, new() { { "FORCE_DEL_USERSETTINGS", "0" }, { "PRODUCT_TYPE", "2" }, { "PRIMARY_LANGUAGE", "1" } }, RequiresElevation: true),
                        // NOTE: although this is not documented in the sample uninstall scripts from Freedom Scientific, we must run ZoomTextSetupPackage with "/uninstall /quiet" as arguments (i.e. the QuietUninstallString of "Freedom Scientific ZoomText 2024")
                        new IAtodOperation.UninstallUsingRegistryUninstallString("{6abadd92-8371-4f4c-becf-6fc94c9dfcc5}", null, null, RequiresElevation: true),
                    ];
                break;
            default:
                throw new Exception("invalid code path");
        }

        return result;
    }
}
