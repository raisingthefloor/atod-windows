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

public interface ISupplementalArgument
{
    public record PostfixArgument(string Arg) : ISupplementalArgument;
    public record PrefixArgument(string Arg) : ISupplementalArgument;
}

public interface IAtodOperation
{
    public record CalculateChecksum(AtodPath SourcePath, string Filename, AtodChecksumAlgorithm ChecksumAlgorithm) : IAtodOperation;
    public record CreateRegistryKey(Microsoft.Win32.RegistryKey RootKey, string SubKeyName, bool RequiresElevation) : IAtodOperation;
    public record Download(Uri Uri, AtodPath DestinationPath, string Filename, IAtodChecksum? Checksum) : IAtodOperation;
    public record InstallExe(AtodPath SourcePath, string Filename, string? CommandLineArgs, List<IAtodOperationCondition> Conditions, int? RebootRequiredExitCode, bool RequiresElevation) : IAtodOperation;
    public record InstallMsi(AtodPath SourcePath, string Filename, Dictionary<string, string>? PropertySettings, bool RequiresElevation) : IAtodOperation;
    public record UninstallUsingRegistryUninstallString(string UninstallSubKeyName, ISupplementalArgument[]? OptionalSupplementalArgs, int? RebootRequiredExitCode, bool RequiresElevation) : IAtodOperation;
    public record UninstallUsingWindowsInstaller(Guid WindowsInstallerProductCode, Dictionary<string, string>? PropertySettings, bool RequiresElevation) : IAtodOperation;
    public record Unzip(AtodPath SourcePath, string Filename, AtodPath DestinationPath) : IAtodOperation;
    public record VerifyChecksum(AtodPath SourcePath, string Filename, IAtodChecksum Checksum) : IAtodOperation;

    public bool GetRequiresElevation()
    {
        var result = this switch
        {
            IAtodOperation.CalculateChecksum => false,
            IAtodOperation.CreateRegistryKey { RequiresElevation: var requiresElevation } => requiresElevation,
            IAtodOperation.Download => false,
            IAtodOperation.InstallExe { RequiresElevation: var requiresElevation } => requiresElevation,
            IAtodOperation.InstallMsi { RequiresElevation: var requiresElevation } => requiresElevation,
            IAtodOperation.UninstallUsingRegistryUninstallString { RequiresElevation: var requiresElevation } => requiresElevation,
            IAtodOperation.UninstallUsingWindowsInstaller { RequiresElevation: var requiresElevation } => requiresElevation,
            IAtodOperation.Unzip => false,
            IAtodOperation.VerifyChecksum => false,
            _ => throw new Exception("invalid code path")
        };
        return result;
    }
}


