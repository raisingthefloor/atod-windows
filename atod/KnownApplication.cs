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
        Nvda,
        ReadAndWrite,
    }

    public readonly KnownApplication.IdValue Id { get; init; }

    public static readonly KnownApplication NVDA = new() { Id = IdValue.Nvda };
    public static readonly KnownApplication READ_AND_WRITE = new() { Id = IdValue.ReadAndWrite };

    public static KnownApplication? TryFromProductName(string applicationName)
    {
        switch (applicationName.ToLowerInvariant())
        {
            case "nvda":
                return KnownApplication.NVDA;
            case "readandwrite":
                return KnownApplication.READ_AND_WRITE;
            default:
                return null;
        }
    }

    public List<AtodOperation> GetInstallOperations()
    {
        List<AtodOperation> result;

        switch (this.Id)
        {
            case IdValue.Nvda:
                result = new List<AtodOperation>()
                {
                    AtodOperation.Download(new Uri("https://www.nvaccess.org/download/nvda/releases/2023.3.1/nvda_2023.3.1.exe"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "nvda_2023.3.1.exe", new IAtodChecksum.Sha256(new byte[] { 181, 55, 16, 36, 104, 67, 106, 185, 62, 15, 230, 60, 247, 140, 138, 220, 84, 66, 235, 190, 208, 88, 146, 119, 212, 92, 60, 185, 196, 239, 140, 114 })),
                    AtodOperation.InstallExe(AtodPath.ExistingPathKey("downloadfolder"), "nvda_2023.3.1.exe", "--minimal --install-silent", true),
                };
                break;
            case IdValue.ReadAndWrite:
                result = new List<AtodOperation>()
                {
                    AtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/us/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // US download MSI
                    //AtodOperation.Download(new Uri("https://fastdownloads2.texthelp.com/readwrite12/installers/uk/setup.zip"), AtodPath.CreateTemporaryFolderForNewPathKey("downloadfolder"), "setup.zip", null), // UK download MSI
                    AtodOperation.Unzip(AtodPath.ExistingPathKey("downloadfolder"), "setup.zip", AtodPath.CreateTemporaryFolderForNewPathKey("setupfolder")),
                    AtodOperation.InstallMsi(AtodPath.ExistingPathKey("setupfolder"), "setup.msi", requiresElevation: true),
                };
                break;
            default:
                throw new Exception("invalid code path");
        }

        return result;
    }

    public List<AtodOperation>? GetUninstallOperations()
    {
        List<AtodOperation>? result;

        switch (this.Id)
        {
            case IdValue.Nvda:
                // no silent uninstaller available
                result = null;
                break;
            case IdValue.ReadAndWrite:
                result = new List<AtodOperation>()
                {
                    AtodOperation.Uninstall(this.GetWindowsInstallerProductCode()!.Value, requiresElevation: true),
                };
                break;
            default:
                throw new Exception("invalid code path");
        }

        return result;
    }

    private Guid? GetWindowsInstallerProductCode()
    {
        switch (this.Id)
        {
            case IdValue.Nvda:
                return null;
            case IdValue.ReadAndWrite:
                return KnownApplicationProductCode.READ_AND_WRITE;
            default:
                throw new Exception("invalid code path");
        }
    }
}
