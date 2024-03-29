﻿// Copyright 2022-2024 Raising the Floor - US, Inc.
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

namespace Atod;

public record AtodOperation : MorphicAssociatedValueEnum<AtodOperation.Values>
{
    // enum members
    public enum Values
    {
        Download,
        InstallMsi,
        Uninstall,
        Unzip,
    }

    // functions to create member instances
    public static AtodOperation Download(Uri uri, AtodPath destinationPath, string filename) => new(Values.Download) { Uri = uri, DestinationPath = destinationPath, Filename = filename };
    public static AtodOperation InstallMsi(AtodPath sourcePath, string filename, bool requiresElevation) => new(Values.InstallMsi) { SourcePath = sourcePath, Filename = filename, RequiresElevation = requiresElevation };
    public static AtodOperation Uninstall(Guid windowsInstallerProductCode, bool requiresElevation) => new(Values.Uninstall) { WindowsInstallerProductCode = windowsInstallerProductCode, RequiresElevation = requiresElevation };
    public static AtodOperation Unzip(AtodPath sourcePath, string filename, AtodPath destinationPath) => new(Values.Unzip) { SourcePath = sourcePath, Filename = filename, DestinationPath = destinationPath };

    // associated values
    public AtodPath? DestinationPath { get; private set; }
    public string? Filename { get; private set; }
    public bool? RequiresElevation { get; private set; }
    public AtodPath? SourcePath { get; private set; }
    public Uri? Uri { get; private set; }
    public Guid? WindowsInstallerProductCode { get; private set; }

    // verbatim required constructor implementation for MorphicAssociatedValueEnums
    private AtodOperation(Values value) : base(value) { }
}


