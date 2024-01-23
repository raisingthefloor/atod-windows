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

using System;
using System.Collections.Generic;

namespace Atod;

internal struct KnownApplication
{
    public enum IdValue
    {
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
        Magic,
        Nvda,
        PurpleP3,
        ReadAndWrite,
        SofType,
        SuperNovaMagnifier,
        SuperNovaMagnifierAndSpeech,
    }

    public readonly KnownApplication.IdValue Id { get; init; }

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
    public static readonly KnownApplication MAGIC = new() {  Id = IdValue.Magic };
    public static readonly KnownApplication NVDA = new() { Id = IdValue.Nvda };
    public static readonly KnownApplication PURPLE_P3 = new() { Id = IdValue.PurpleP3 };
    public static readonly KnownApplication READ_AND_WRITE = new() { Id = IdValue.ReadAndWrite };
    public static readonly KnownApplication SOFTYPE = new() { Id = IdValue.SofType };
    public static readonly KnownApplication SUPERNOVA_MAGNIFIER = new() { Id = IdValue.SuperNovaMagnifier };
    public static readonly KnownApplication SUPERNOVA_MAGNIFIER_AND_SPEECH = new() { Id = IdValue.SuperNovaMagnifierAndSpeech };

    public static KnownApplication? TryFromProductName(string applicationName)
    {
        switch (applicationName.ToLowerInvariant())
        {
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
            case "magic":
                return KnownApplication.MAGIC;
            case "nvda":
                return KnownApplication.NVDA;
            case "purplep3":
                return KnownApplication.PURPLE_P3;
            case "readandwrite":
                return KnownApplication.READ_AND_WRITE;
            case "softype":
                return KnownApplication.SOFTYPE;
            case "supernovamagnifier":
                return KnownApplication.SUPERNOVA_MAGNIFIER;
            case "supernovamagnifierandspeech":
                return KnownApplication.SUPERNOVA_MAGNIFIER_AND_SPEECH;
            default:
                return null;
        }
    }

    public List<IAtodOperation> GetInstallOperations()
    {
        List<IAtodOperation> result;

        const int STANDARD_REBOOT_REQUIRED_EXIT_CODE = unchecked((int)(uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS_REBOOT_REQUIRED);

        switch (this.Id)
        {
            case IdValue.AutoHotkey:
                result =
                [
                    new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/autohotkey/AutoHotkey_2.0.11_setup.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "AutoHotkey_2.0.11_setup.exe", new IAtodChecksum.Sha256([81, 10, 131, 59, 221, 15, 137, 108, 195, 152, 234, 174, 79, 244, 117, 245, 183, 207, 227, 118, 73, 239, 191, 100, 123, 80, 210, 30, 68, 35, 148, 185])),
                    // NOTE: we had problems with the AutoHotkey CDN returning HTTP 403 in testing; for now, we're using the AToD CDN instead
                    //new IAtodOperation.Download(new Uri("https://www.autohotkey.com/download/2.0/AutoHotkey_2.0.11_setup.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "AutoHotkey_2.0.11_setup.exe", new IAtodChecksum.Sha256([81, 10, 131, 59, 221, 15, 137, 108, 195, 152, 234, 174, 79, 244, 117, 245, 183, 207, 227, 118, 73, 239, 191, 100, 123, 80, 210, 30, 68, 35, 148, 185])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "AutoHotkey_2.0.11_setup.exe", "/silent", [], null, true),
                ];
                break;
            case IdValue.BuildABoard:
                result = 
                [
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/buildaboard/bab220r7_win_7-11.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "bab220r7_win_7-11.exe", new IAtodChecksum.Sha256([5, 119, 199, 0, 179, 224, 50, 132, 212, 73, 223, 118, 101, 145, 65, 143, 217, 30, 217, 149, 1, 160, 175, 154, 163, 91, 40, 217, 177, 212, 230, 61])),
                    new IAtodOperation.Download(new Uri("https://www.imgpresents.com/downloads/secure/bab220r7_win_7-11.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "bab220r7_win_7-11.exe", new IAtodChecksum.Sha256([5, 119, 199, 0, 179, 224, 50, 132, 212, 73, 223, 118, 101, 145, 65, 143, 217, 30, 217, 149, 1, 160, 175, 154, 163, 91, 40, 217, 177, 212, 230, 61])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "bab220r7_win_7-11.exe", "/Q", [], null, true),
                ];
                break;
            case IdValue.CameraMouse:
                result =
                [
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/cameramouse/CameraMouse2018Installer.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "CameraMouse2018Installer.exe", new IAtodChecksum.Sha256([46, 79, 235, 219, 64, 193, 9, 39, 54, 33, 199, 223, 31, 66, 226, 80, 196, 144, 214, 172, 6, 147, 65, 80, 239, 220, 234, 126, 200, 111, 68, 52])),
                    new IAtodOperation.Download(new Uri("http://www.cameramouse.org/downloads/CameraMouse2018Installer.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "CameraMouse2018Installer.exe", new IAtodChecksum.Sha256([46, 79, 235, 219, 64, 193, 9, 39, 54, 33, 199, 223, 31, 66, 226, 80, 196, 144, 214, 172, 6, 147, 65, 80, 239, 220, 234, 126, 200, 111, 68, 52])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "CameraMouse2018Installer.exe", "/VERYSILENT", [], null, true),
                ];
                break;
            case IdValue.ClaroRead:
                result =
                [
                    new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/claroread/ClaroRead-12.0.29-auth-bundle.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "ClaroRead-12.0.29-auth-bundle.zip", new IAtodChecksum.Sha256([71, 107, 184, 235, 96, 196, 0, 70, 56, 229, 227, 209, 126, 188, 46, 17, 211, 118, 45, 14, 31, 184, 124, 58, 145, 132, 207, 46, 211, 194, 176, 120])),
                    new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "ClaroRead-12.0.29-auth-bundle.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroRead-int-12.0.29-auth.msi", null, RequiresElevation: true),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Capture-int-8.2.5-auth.msi", null, RequiresElevation: true),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroIdeas-int-3.1.0-auth.msi", null, RequiresElevation: true),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroView-int-3.4.8-auth.msi", null, RequiresElevation: true),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "Scan2Text-int-7.4.19-auth.msi", null, RequiresElevation: true),
                ];
                break;
            case IdValue.ClaroReadSe:
                result =
                [
                    new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/claroreadse/ClaroReadSE-12.0.29-auth.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "ClaroReadSE-12.0.29-auth.zip", new IAtodChecksum.Sha256([10, 216, 206, 64, 233, 62, 147, 47, 173, 200, 128, 149, 102, 213, 0, 140, 116, 0, 210, 33, 111, 211, 74, 70, 140, 74, 135, 26, 106, 206, 3, 126])),
                    new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "ClaroReadSE-12.0.29-auth.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ClaroReadSE-int-12.0.29-auth.msi", null, RequiresElevation: true),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "ScanScreenPlus-int-2.2.4-net.msi", null, RequiresElevation: true),
                ];
                break;
            case IdValue.ClickNType:
                // NOTE: this MSI is not digitally signed and should not be included in the catalog until it can be authenticated (and even then, it should probably be forced to use UAC individually for consumers)
                result =
                [
                    new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/clickntype/CNTsetup.msi"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "CNTsetup.msi", new IAtodChecksum.Sha256([247, 39, 177, 189, 83, 5, 117, 0, 156, 194, 197, 39, 178, 75, 17, 158, 11, 182, 44, 216, 240, 103, 55, 228, 156, 122, 103, 243, 197, 141, 49, 102])),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "CNTsetup.msi", null, RequiresElevation: true),
                ];
                break;
            case IdValue.ComfortOsk:
                result =
                [
                    new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/comfortosk/ComfortOSKSetup.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "ComfortOSKSetup.exe", new IAtodChecksum.Sha256([47, 50, 152, 31, 84, 136, 66, 70, 235, 149, 2, 171, 74, 145, 63, 162, 219, 199, 185, 249, 179, 244, 0, 127, 24, 199, 74, 255, 51, 29, 161, 34])),
                    //new IAtodOperation.Download(new Uri("https://www.comfortsoftware.com/download/ComfortOSKSetup.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "ComfortOSKSetup.exe", new IAtodChecksum.Sha256([47, 50, 152, 31, 84, 136, 66, 70, 235, 149, 2, 171, 74, 145, 63, 162, 219, 199, 185, 249, 179, 244, 0, 127, 24, 199, 74, 255, 51, 29, 161, 34])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "ComfortOSKSetup.exe", "/NORESTART /VERYSILENT /RESTARTEXITCODE=" + STANDARD_REBOOT_REQUIRED_EXIT_CODE.ToString(), [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                ];
                break;
            case IdValue.Communicator5:
                result =
                [
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/communicator5/TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe", new IAtodChecksum.Sha256([166, 250, 23, 4, 104, 200, 73, 99, 181, 133, 233, 25, 48, 23, 179, 100, 68, 37, 145, 200, 35, 192, 159, 168, 164, 220, 248, 225, 14, 66, 57, 31])),
                    new IAtodOperation.Download(new Uri("https://download.mytobiidynavox.com/Communicator/software/5.6.1/TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe", new IAtodChecksum.Sha256([166, 250, 23, 4, 104, 200, 73, 99, 181, 133, 233, 25, 48, 23, 179, 100, 68, 37, 145, 200, 35, 192, 159, 168, 164, 220, 248, 225, 14, 66, 57, 31])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "TobiiDynavox_CommunicatorSuite_Installer_5.6.1.5584_en-US.exe", "/SILENT", [], null, true),
                ];
                break;
            case IdValue.CoWriter:
                result =
                [
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/cowriter/cowriter-universal-desktop.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "cowriter-universal-desktop.zip", new IAtodChecksum.Sha256([161, 17, 99, 194, 63, 214, 199, 187, 55, 65, 121, 192, 191, 184, 150, 177, 4, 92, 191, 174, 53, 80, 60, 209, 30, 46, 11, 137, 44, 250, 65, 160])),
                    new IAtodOperation.Download(new Uri("http://donjohnston.com/wp-content/downloads/products/cowriter-universal-desktop.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "cowriter-universal-desktop.zip", new IAtodChecksum.Sha256([161, 17, 99, 194, 63, 214, 199, 187, 55, 65, 121, 192, 191, 184, 150, 177, 4, 92, 191, 174, 53, 80, 60, 209, 30, 46, 11, 137, 44, 250, 65, 160])),
                    new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "cowriter-universal-desktop.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "CoWriter Universal Desktop.msi", null, RequiresElevation: true),
                ];
                break;
            case IdValue.DolphinScreenReader:
                result =
                [
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/dolphinscreenreader/ScreenReader_22.04_English_(United_States)_NETWORK.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "ScreenReader_22.04_English_(United_States)_NETWORK.zip", new IAtodChecksum.Sha256([10, 9, 68, 124, 151, 69, 51, 35, 52, 38, 250, 19, 105, 129, 188, 249, 101, 101, 1, 43, 207, 49, 10, 109, 1, 0, 123, 94, 216, 152, 46, 232])),
                    new IAtodOperation.Download(new Uri("https://yourdolphin.com/downloads/product?pvid=552&lid=2&network=true"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "ScreenReader_22.04_English_(United_States)_NETWORK.zip", new IAtodChecksum.Sha256([10, 9, 68, 124, 151, 69, 51, 35, 52, 38, 250, 19, 105, 129, 188, 249, 101, 101, 1, 43, 207, 49, 10, 109, 1, 0, 123, 94, 216, 152, 46, 232])),
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
                result =
                [
                    // Intel 32-bit (also works on 64-bit)
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/dragger/DraggerSetup2.0.1350.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "DraggerSetup2.0.1350.0.exe", new IAtodChecksum.Sha256([192, 106, 159, 37, 241, 148, 202, 37, 180, 183, 125, 200, 244, 122, 178, 4, 174, 80, 91, 54, 68, 226, 81, 205, 0, 73, 149, 110, 226, 93, 151, 232])),
                    new IAtodOperation.Download(new Uri("https://orin.com/binaries/DraggerSetup2.0.1350.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "DraggerSetup2.0.1350.0.exe", new IAtodChecksum.Sha256([192, 106, 159, 37, 241, 148, 202, 37, 180, 183, 125, 200, 244, 122, 178, 4, 174, 80, 91, 54, 68, 226, 81, 205, 0, 73, 149, 110, 226, 93, 151, 232])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "DraggerSetup2.0.1350.0.exe", "/qn", [], null, true),
                ];
                break;
            case IdValue.Magic:
                result =
                [
                    // Intel 64-bit
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/magic/M15.0.2014.400-enu-x64.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", new IAtodChecksum.Sha256([117, 43, 40, 31, 213, 216, 116, 36, 163, 80, 21, 225, 236, 198, 3, 98, 216, 80, 190, 74, 152, 147, 124, 179, 180, 103, 218, 218, 33, 248, 175, 208])),
                    new IAtodOperation.Download(new Uri("https://magic15.vfo.digital/1502013NT2244W/M15.0.2014.400-enu-x64.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", new IAtodChecksum.Sha256([117, 43, 40, 31, 213, 216, 116, 36, 163, 80, 21, 225, 236, 198, 3, 98, 216, 80, 190, 74, 152, 147, 124, 179, 180, 103, 218, 218, 33, 248, 175, 208])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", "/type silent", [], STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                    //
                    // Intel 32-bit
                    ////new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/magic/M15.0.2014.400-enu-x86.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", new IAtodChecksum.Sha256([191, 242, 8, 62, 97, 92, 119, 166, 122, 208, 203, 88, 108, 200, 56, 65, 5, 197, 43, 69, 56, 82, 107, 0, 78, 111, 94, 99, 189, 235, 169, 128])),
                    //new IAtodOperation.Download(new Uri("https://magic15.vfo.digital/1502013NT2244W/M15.0.2014.400-enu-x86.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", new IAtodChecksum.Sha256([191, 242, 8, 62, 97, 92, 119, 166, 122, 208, 203, 88, 108, 200, 56, 65, 5, 197, 43, 69, 56, 82, 107, 0, 78, 111, 94, 99, 189, 235, 169, 128])),
                    //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", "/type silent", STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                ];
                break;
            case IdValue.Nvda:
                result =
                [
                    new IAtodOperation.Download(new Uri("https://www.nvaccess.org/download/nvda/releases/2023.3.1/nvda_2023.3.1.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "nvda_2023.3.1.exe", new IAtodChecksum.Sha256([181, 55, 16, 36, 104, 67, 106, 185, 62, 15, 230, 60, 247, 140, 138, 220, 84, 66, 235, 190, 208, 88, 146, 119, 212, 92, 60, 185, 196, 239, 140, 114])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "nvda_2023.3.1.exe", "--minimal --install-silent", [], null, true),
                ];
                break;
            case IdValue.PurpleP3:
                result =
                [
                    // Intel 64-bit (presumably, based on the 64b filename)
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/purple/Purple_P3_9.6.1-3513-64b.msi"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "Purple_P3_9.6.1-3513-64b.msi", new IAtodChecksum.Sha256([150, 220, 62, 126, 42, 170, 35, 35, 144, 175, 63, 143, 147, 215, 67, 168, 203, 118, 93, 241, 10, 254, 67, 224, 147, 168, 255, 220, 225, 167, 117, 2])),
                    new IAtodOperation.Download(new Uri("https://s3.amazonaws.com/PurpleDownloads/P3/Purple_P3_9.6.1-3513-64b.msi"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "Purple_P3_9.6.1-3513-64b.msi", new IAtodChecksum.Sha256([150, 220, 62, 126, 42, 170, 35, 35, 144, 175, 63, 143, 147, 215, 67, 168, 203, 118, 93, 241, 10, 254, 67, 224, 147, 168, 255, 220, 225, 167, 117, 2])),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "Purple_P3_9.6.1-3513-64b.msi", new() { { "WRAPPED_ARGUMENTS", "\"--mode unattended\"" } }, RequiresElevation: true),
                ];
                break;
            case IdValue.ReadAndWrite:
                result =
                [
                    new IAtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/us/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // US download MSI
                    //new AtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/uk/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // UK download MSI
                    new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "setup.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "setup.msi", null, RequiresElevation: true),
                ];
                break;
            case IdValue.SofType:
                result =
                [
                    // Intel 32-bit (also works on 64-bit)
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/softype/SofTypeSetup5.0.1074.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SofTypeSetup5.0.1074.0.exe", new IAtodChecksum.Sha256([7, 194, 43, 36, 173, 85, 39, 1, 90, 164, 199, 126, 119, 42, 88, 220, 237, 100, 180, 138, 132, 56, 211, 126, 203, 95, 151, 150, 184, 245, 123, 31])),
                    new IAtodOperation.Download(new Uri("https://orin.com/binaries/SofTypeSetup5.0.1074.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SofTypeSetup5.0.1074.0.exe", new IAtodChecksum.Sha256([7, 194, 43, 36, 173, 85, 39, 1, 90, 164, 199, 126, 119, 42, 88, 220, 237, 100, 180, 138, 132, 56, 211, 126, 203, 95, 151, 150, 184, 245, 123, 31])),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "SofTypeSetup5.0.1074.0.exe", "/qn", [], null, true),
                ];
                break;
            case IdValue.SuperNovaMagnifier:
                result = 
                [
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/supernovamagnifier/SuperNova_Magnifier_22.04_English_(United_States)_NETWORK.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SuperNova_Magnifier_22.04_English_(United_States)_NETWORK.zip", new IAtodChecksum.Sha256([75, 230, 63, 108, 233, 45, 206, 131, 184, 250, 237, 149, 69, 5, 36, 56, 41, 71, 213, 154, 133, 68, 32, 149, 167, 53, 179, 162, 124, 162, 227, 1])),
                    new IAtodOperation.Download(new Uri("https://yourdolphin.com/downloads/product?pvid=554&lid=2&network=true"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SuperNova_Magnifier_22.04_English_(United_States)_NETWORK.zip", new IAtodChecksum.Sha256([75, 230, 63, 108, 233, 45, 206, 131, 184, 250, 237, 149, 69, 5, 36, 56, 41, 71, 213, 154, 133, 68, 32, 149, 167, 53, 179, 162, 124, 162, 227, 17])),
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
                result =
                [
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/supernovamagnifierandspeech/SuperNova_Magnifier_&_Speech_22.04_English_(United_States)_NETWORK.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SuperNova_Magnifier_&_Speech_22.04_English_(United_States)_NETWORK", new IAtodChecksum.Sha256([177, 168, 203, 125, 224, 250, 32, 161, 89, 193, 190, 62, 99, 104, 24, 41, 159, 73, 240, 102, 41, 211, 31, 89, 190, 117, 81, 139, 179, 211, 102, 148])),
                    new IAtodOperation.Download(new Uri("https://yourdolphin.com/downloads/product?pvid=556&lid=2&network=true"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SuperNova_Magnifier_&_Speech_22.04_English_(United_States)_NETWORK.zip", new IAtodChecksum.Sha256([177, 168, 203, 125, 224, 250, 32, 161, 89, 193, 190, 62, 99, 104, 24, 41, 159, 73, 240, 102, 41, 211, 31, 89, 190, 117, 81, 139, 179, 211, 102, 148])),
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
            default:
                throw new Exception("invalid code path");
        }

        return result;
    }

    public List<IAtodOperation>? GetUninstallOperations()
    {
        List<IAtodOperation>? result;

//        const int STANDARD_REBOOT_REQUIRED_EXIT_CODE = unchecked((int)(uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS_REBOOT_REQUIRED);

        switch (this.Id)
        {
            case IdValue.AutoHotkey:
                // NOTE: AutoHotkey has a "QuietUninstallString" registry entry, so we don't need to pass in "/silent" as a flag
                result =
                [
//                    new IAtodOperation.UninstallUsingRegistryUninstallString("AutoHotkey", new string[] { "/silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingRegistryUninstallString("AutoHotkey", null, null, RequiresElevation: true),
                ];
                break;
            case IdValue.BuildABoard:
                // as of 2024-01-19, there is no silent uninstall available for Build-A-Board
                result = null;
                //result = new List<IAtodOperation>()
                //{
                //    // NOTE: the corresponding UninstallString (as of 2024-01-19) was "C:\Program Files (x86)\Build-A-Board\BIN\IMGUTIL.exe Uninstall12401", with no silent uninstallation option
                //    new IAtodOperation.UninstallUsingRegistryUninstallString("Build-A-Board", null, null, RequiresElevation: true),
                //};
                break;
            case IdValue.CameraMouse:
                // NOTE: CameraMouse has a "QuietUninstallString" registry entry, but it uses "/SILENT" instead of "/VERYSILENT" so we supplement it with "/VERYSILENT"
                result =
                [
                    new IAtodOperation.UninstallUsingRegistryUninstallString("{F5E6727D-0969-4C4A-A669-71F1A3913A03}}_is1", new string[] { "/VERYSILENT" }, null, RequiresElevation: true),
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
                    new IAtodOperation.UninstallUsingRegistryUninstallString("{6EB17721-6249-417B-99B9-DAF3FD532955}_is1", new string[] { "/NORESTART /VERYSILENT" }, null, RequiresElevation: true),
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
            case IdValue.Magic:
                result =
                [
                    new IAtodOperation.UninstallUsingRegistryUninstallString("MAGic15.0", new string[] { "/type silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingRegistryUninstallString("FSReader3.0", new string[] { "/type silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY, null, RequiresElevation: true),
                ];
                break;
            case IdValue.Nvda:
                // NOTE: NVDA does not include the "/S" option (for Nullsoft "SILENT") in their UninstallString, so we force-add it as a parameter; this is fragile, and it should be removed if a QuietUninstallString is present, etc.
                result =
                [
                    new IAtodOperation.UninstallUsingRegistryUninstallString("NVDA", new string[] { "/S" }, null, RequiresElevation: true),
                ];
                break;
            case IdValue.PurpleP3:
                result =
                [
                    //new IAtodOperation.UninstallUsingRegistryUninstallString("Purple P3 9.6.1-3513", new string[] { "--mode unattended" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.PURPLE_P3, new() { { "WRAPPED_ARGUMENTS", "\"--mode unattended\"" } }, RequiresElevation: true),
                ];
                break;
            case IdValue.ReadAndWrite:
                result =
                [
                    //new IAtodOperation.UninstallUsingWindowsInstaller(this.GetWindowsInstallerProductCode()!.Value, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.READ_AND_WRITE, null, RequiresElevation: true),
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
            default:
                throw new Exception("invalid code path");
        }

        return result;
    }
}
