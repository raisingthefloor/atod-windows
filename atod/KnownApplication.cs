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
        Dragger,
        Magic,
        Nvda,
        PurpleP3,
        ReadAndWrite,
        SofType,
    }

    public readonly KnownApplication.IdValue Id { get; init; }

    public static readonly KnownApplication AUTOHOTKEY = new() {  Id = IdValue.AutoHotkey };
    public static readonly KnownApplication BUILD_A_BOARD = new() { Id = IdValue.BuildABoard };
    public static readonly KnownApplication CAMERA_MOUSE = new() { Id = IdValue.CameraMouse };
    public static readonly KnownApplication DRAGGER = new() { Id = IdValue.Dragger };
    public static readonly KnownApplication MAGIC = new() {  Id = IdValue.Magic };
    public static readonly KnownApplication NVDA = new() { Id = IdValue.Nvda };
    public static readonly KnownApplication PURPLE_P3 = new() { Id = IdValue.PurpleP3 };
    public static readonly KnownApplication READ_AND_WRITE = new() { Id = IdValue.ReadAndWrite };
    public static readonly KnownApplication SOFTYPE = new() { Id = IdValue.SofType };

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
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/autohotkey/AutoHotkey_2.0.11_setup.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "AutoHotkey_2.0.11_setup.exe", new IAtodChecksum.Sha256(new byte[] { 81, 10, 131, 59, 221, 15, 137, 108, 195, 152, 234, 174, 79, 244, 117, 245, 183, 207, 227, 118, 73, 239, 191, 100, 123, 80, 210, 30, 68, 35, 148, 185 })),
                    // NOTE: we had problems with the AutoHotkey CDN returning HTTP 403 in testing; for now, we're using the AToD CDN instead
                    //new IAtodOperation.Download(new Uri("https://www.autohotkey.com/download/2.0/AutoHotkey_2.0.11_setup.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "AutoHotkey_2.0.11_setup.exe", new IAtodChecksum.Sha256(new byte[] { 81, 10, 131, 59, 221, 15, 137, 108, 195, 152, 234, 174, 79, 244, 117, 245, 183, 207, 227, 118, 73, 239, 191, 100, 123, 80, 210, 30, 68, 35, 148, 185 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "AutoHotkey_2.0.11_setup.exe", "/silent", null, true),
                };
                break;
            case IdValue.BuildABoard:
                result = new List<IAtodOperation>()
                {
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/buildaboard/bab220r7_win_7-11.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "bab220r7_win_7-11.exe", new IAtodChecksum.Sha256(new byte[] { 5, 119, 199, 0, 179, 224, 50, 132, 212, 73, 223, 118, 101, 145, 65, 143, 217, 30, 217, 149, 1, 160, 175, 154, 163, 91, 40, 217, 177, 212, 230, 61 })),
                    new IAtodOperation.Download(new Uri("https://www.imgpresents.com/downloads/secure/bab220r7_win_7-11.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "bab220r7_win_7-11.exe", new IAtodChecksum.Sha256(new byte[] { 5, 119, 199, 0, 179, 224, 50, 132, 212, 73, 223, 118, 101, 145, 65, 143, 217, 30, 217, 149, 1, 160, 175, 154, 163, 91, 40, 217, 177, 212, 230, 61 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "bab220r7_win_7-11.exe", "/Q", null, true),
                };
                break;
            case IdValue.CameraMouse:
                result = new List<IAtodOperation>()
                {
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/cameramouse/CameraMouse2018Installer.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "CameraMouse2018Installer.exe", new IAtodChecksum.Sha256(new byte[] { 46, 79, 235, 219, 64, 193, 9, 39, 54, 33, 199, 223, 31, 66, 226, 80, 196, 144, 214, 172, 6, 147, 65, 80, 239, 220, 234, 126, 200, 111, 68, 52 })),
                    new IAtodOperation.Download(new Uri("http://www.cameramouse.org/downloads/CameraMouse2018Installer.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "CameraMouse2018Installer.exe", new IAtodChecksum.Sha256(new byte[] { 46, 79, 235, 219, 64, 193, 9, 39, 54, 33, 199, 223, 31, 66, 226, 80, 196, 144, 214, 172, 6, 147, 65, 80, 239, 220, 234, 126, 200, 111, 68, 52 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "CameraMouse2018Installer.exe", "/VERYSILENT", null, true),
                };
                break;
            case IdValue.Dragger:
                result = new List<IAtodOperation>()
                {
                    // Intel 32-bit (also works on 64-bit)
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/dragger/DraggerSetup2.0.1350.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "DraggerSetup2.0.1350.0.exe", new IAtodChecksum.Sha256(new byte[] { 192, 106, 159, 37, 241, 148, 202, 37, 180, 183, 125, 200, 244, 122, 178, 4, 174, 80, 91, 54, 68, 226, 81, 205, 0, 73, 149, 110, 226, 93, 151, 232 })),
                    new IAtodOperation.Download(new Uri("https://orin.com/binaries/DraggerSetup2.0.1350.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "DraggerSetup2.0.1350.0.exe", new IAtodChecksum.Sha256(new byte[] { 192, 106, 159, 37, 241, 148, 202, 37, 180, 183, 125, 200, 244, 122, 178, 4, 174, 80, 91, 54, 68, 226, 81, 205, 0, 73, 149, 110, 226, 93, 151, 232 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "DraggerSetup2.0.1350.0.exe", "/qn", null, true),
                };
                break;
            case IdValue.Magic:
                result = new List<IAtodOperation>()
                {
                    // Intel 64-bit
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/magic/M15.0.2014.400-enu-x64.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", new IAtodChecksum.Sha256(new byte[] { 117, 43, 40, 31, 213, 216, 116, 36, 163, 80, 21, 225, 236, 198, 3, 98, 216, 80, 190, 74, 152, 147, 124, 179, 180, 103, 218, 218, 33, 248, 175, 208 })),
                    new IAtodOperation.Download(new Uri("https://magic15.vfo.digital/1502013NT2244W/M15.0.2014.400-enu-x64.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", new IAtodChecksum.Sha256(new byte[] { 117, 43, 40, 31, 213, 216, 116, 36, 163, 80, 21, 225, 236, 198, 3, 98, 216, 80, 190, 74, 152, 147, 124, 179, 180, 103, 218, 218, 33, 248, 175, 208 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", "/type silent", STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                    //
                    // Intel 32-bit
                    ////new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/magic/M15.0.2014.400-enu-x86.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", new IAtodChecksum.Sha256(new byte[] { 191, 242, 8, 62, 97, 92, 119, 166, 122, 208, 203, 88, 108, 200, 56, 65, 5, 197, 43, 69, 56, 82, 107, 0, 78, 111, 94, 99, 189, 235, 169, 128 })),
                    //new IAtodOperation.Download(new Uri("https://magic15.vfo.digital/1502013NT2244W/M15.0.2014.400-enu-x86.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", new IAtodChecksum.Sha256(new byte[] { 191, 242, 8, 62, 97, 92, 119, 166, 122, 208, 203, 88, 108, 200, 56, 65, 5, 197, 43, 69, 56, 82, 107, 0, 78, 111, 94, 99, 189, 235, 169, 128 })),
                    //new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", "/type silent", STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                };
                break;
            case IdValue.Nvda:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.Download(new Uri("https://www.nvaccess.org/download/nvda/releases/2023.3.1/nvda_2023.3.1.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "nvda_2023.3.1.exe", new IAtodChecksum.Sha256(new byte[] { 181, 55, 16, 36, 104, 67, 106, 185, 62, 15, 230, 60, 247, 140, 138, 220, 84, 66, 235, 190, 208, 88, 146, 119, 212, 92, 60, 185, 196, 239, 140, 114 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "nvda_2023.3.1.exe", "--minimal --install-silent", null, true),
                };
                break;
            case IdValue.PurpleP3:
                result = new List<IAtodOperation>()
                {
                    // Intel 64-bit (presumably, based on the 64b filename)
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/purple/Purple_P3_9.6.1-3513-64b.msi"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "Purple_P3_9.6.1-3513-64b.msi", new IAtodChecksum.Sha256(new byte[] { 150, 220, 62, 126, 42, 170, 35, 35, 144, 175, 63, 143, 147, 215, 67, 168, 203, 118, 93, 241, 10, 254, 67, 224, 147, 168, 255, 220, 225, 167, 117, 2 })),
                    new IAtodOperation.Download(new Uri("https://s3.amazonaws.com/PurpleDownloads/P3/Purple_P3_9.6.1-3513-64b.msi"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "Purple_P3_9.6.1-3513-64b.msi", new IAtodChecksum.Sha256(new byte[] { 150, 220, 62, 126, 42, 170, 35, 35, 144, 175, 63, 143, 147, 215, 67, 168, 203, 118, 93, 241, 10, 254, 67, 224, 147, 168, 255, 220, 225, 167, 117, 2 })),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("downloadfolder"), "Purple_P3_9.6.1-3513-64b.msi", new() { { "WRAPPED_ARGUMENTS", "\"--mode unattended\"" } }, RequiresElevation: true),
                };
                break;
            case IdValue.ReadAndWrite:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/us/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // US download MSI
                    //new AtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/uk/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // UK download MSI
                    new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "setup.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "setup.msi", null, RequiresElevation: true),
                };
                break;
            case IdValue.SofType:
                result = new List<IAtodOperation>()
                {
                    // Intel 32-bit (also works on 64-bit)
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/softype/SofTypeSetup5.0.1074.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SofTypeSetup5.0.1074.0.exe", new IAtodChecksum.Sha256(new byte[] { 7, 194, 43, 36, 173, 85, 39, 1, 90, 164, 199, 126, 119, 42, 88, 220, 237, 100, 180, 138, 132, 56, 211, 126, 203, 95, 151, 150, 184, 245, 123, 31 })),
                    new IAtodOperation.Download(new Uri("https://orin.com/binaries/SofTypeSetup5.0.1074.0.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "SofTypeSetup5.0.1074.0.exe", new IAtodChecksum.Sha256(new byte[] { 7, 194, 43, 36, 173, 85, 39, 1, 90, 164, 199, 126, 119, 42, 88, 220, 237, 100, 180, 138, 132, 56, 211, 126, 203, 95, 151, 150, 184, 245, 123, 31 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "SofTypeSetup5.0.1074.0.exe", "/qn", null, true),
                };
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
                result = new List<IAtodOperation>()
                {
//                    new IAtodOperation.UninstallUsingRegistryUninstallString("AutoHotkey", new string[] { "/silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingRegistryUninstallString("AutoHotkey", null, null, RequiresElevation: true),
                };
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
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.UninstallUsingRegistryUninstallString("{F5E6727D-0969-4C4A-A669-71F1A3913A03}}_is1", new string[] { "/VERYSILENT" }, null, RequiresElevation: true),
                };
                break;
            case IdValue.Dragger:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.DRAGGER, null, RequiresElevation: true),
                };
                break;
            case IdValue.Magic:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.UninstallUsingRegistryUninstallString("MAGic15.0", new string[] { "/type silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingRegistryUninstallString("FSReader3.0", new string[] { "/type silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY, null, RequiresElevation: true),
                };
                break;
            case IdValue.Nvda:
                // NOTE: NVDA does not include the "/S" option (for Nullsoft "SILENT") in their UninstallString, so we force-add it as a parameter; this is fragile, and it should be removed if a QuietUninstallString is present, etc.
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.UninstallUsingRegistryUninstallString("NVDA", new string[] { "/S" }, null, RequiresElevation: true),
                };
                break;
            case IdValue.PurpleP3:
                result = new List<IAtodOperation>()
                {
                    //new IAtodOperation.UninstallUsingRegistryUninstallString("Purple P3 9.6.1-3513", new string[] { "--mode unattended" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.PURPLE_P3, new() { { "WRAPPED_ARGUMENTS", "\"--mode unattended\"" } }, RequiresElevation: true),
                };
                break;
            case IdValue.ReadAndWrite:
                result = new List<IAtodOperation>()
                {
                    //new IAtodOperation.UninstallUsingWindowsInstaller(this.GetWindowsInstallerProductCode()!.Value, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.READ_AND_WRITE, null, RequiresElevation: true),
                };
                break;
            case IdValue.SofType:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.SOFTYPE, null, RequiresElevation: true),
                };
                break;
            default:
                throw new Exception("invalid code path");
        }

        return result;
    }
}
