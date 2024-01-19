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
        Magic,
        Nvda,
        ReadAndWrite,
    }

    public readonly KnownApplication.IdValue Id { get; init; }

    public static readonly KnownApplication MAGIC = new() {  Id = IdValue.Magic };
    public static readonly KnownApplication NVDA = new() { Id = IdValue.Nvda };
    public static readonly KnownApplication READ_AND_WRITE = new() { Id = IdValue.ReadAndWrite };

    public static KnownApplication? TryFromProductName(string applicationName)
    {
        switch (applicationName.ToLowerInvariant())
        {
            case "magic":
                return KnownApplication.MAGIC;
            case "nvda":
                return KnownApplication.NVDA;
            case "readandwrite":
                return KnownApplication.READ_AND_WRITE;
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
            case IdValue.Magic:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/magic/M15.0.2014.400-enu-x64.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", new IAtodChecksum.Sha256(new byte[] { 117, 43, 40, 31, 213, 216, 116, 36, 163, 80, 21, 225, 236, 198, 3, 98, 216, 80, 190, 74, 152, 147, 124, 179, 180, 103, 218, 218, 33, 248, 175, 208 })),
                    new IAtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "M15.0.2014.400-enu-x64.exe", "/type silent", STANDARD_REBOOT_REQUIRED_EXIT_CODE, true),
                    //new IAtodOperation.Download(new Uri("https://atod-cdn.raisingthefloor.org/magic/M15.0.2014.400-enu-x86.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "M15.0.2014.400-enu-x86.exe", new IAtodChecksum.Sha256(new byte[] { 191, 242, 8, 62, 97, 92, 119, 166, 122, 208, 203, 88, 108, 200, 56, 65, 5, 197, 43, 69, 56, 82, 107, 0, 78, 111, 94, 99, 189, 235, 169, 128 })),
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
            case IdValue.ReadAndWrite:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/us/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // US download MSI
                    //new AtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/uk/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // UK download MSI
                    new IAtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "setup.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    new IAtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "setup.msi", RequiresElevation: true),
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

        const int STANDARD_REBOOT_REQUIRED_EXIT_CODE = unchecked((int)(uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS_REBOOT_REQUIRED);

        switch (this.Id)
        {
            case IdValue.Magic:
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.UninstallUsingRegistryUninstallString("MAGic15.0", new string[] { "/type silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingRegistryUninstallString("FSReader3.0", new string[] { "/type silent" }, null, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY, RequiresElevation: true),
                };
                break;
            case IdValue.Nvda:
                // NOTE: NVDA does not include the "/S" option (for Nullsoft "SILENT") in their UninstallString, so we force-add it as a parameter; this is fragile, and it should be removed if a QuietUninstallString is present, etc.
                result = new List<IAtodOperation>()
                {
                    new IAtodOperation.UninstallUsingRegistryUninstallString("NVDA", new string[] { "/S" }, null, RequiresElevation: true),
                };
                break;
            case IdValue.ReadAndWrite:
                result = new List<IAtodOperation>()
                {
                    //new IAtodOperation.UninstallUsingWindowsInstaller(this.GetWindowsInstallerProductCode()!.Value, RequiresElevation: true),
                    new IAtodOperation.UninstallUsingWindowsInstaller(KnownApplicationProductCode.READ_AND_WRITE, RequiresElevation: true),
                };
                break;
            default:
                throw new Exception("invalid code path");
        }

        return result;
    }

    //private Guid? GetWindowsInstallerProductCode()
    //{
    //    switch (this.Id)
    //    {
    //        case IdValue.Magic:
    //            // NOTE: MAGic is a multi-component installer
    //            return null;
    //        case IdValue.Nvda:
    //            return null;
    //        case IdValue.ReadAndWrite:
    //            return KnownApplicationProductCode.READ_AND_WRITE;
    //        default:
    //            throw new Exception("invalid code path");
    //    }
    //}
}
