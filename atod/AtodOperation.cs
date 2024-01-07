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

namespace atod;

public record AtodOperation : MorphicAssociatedValueEnum<AtodOperation.Values>
{
    // enum members
    public enum Values
    {
        Install,
#if DEBUG		
        InstallMsi,
#endif
        Uninstall,
    }

    // functions to create member instances
    public static AtodOperation Install(string applicationName, string? fullPath) => new(Values.Install) { ApplicationName = applicationName, FullPath = fullPath };
#if DEBUG		
    public static AtodOperation InstallMsi(string fullPath) => new(Values.InstallMsi) { FullPath = fullPath };
#endif
    public static AtodOperation Uninstall(string applicationName) => new(Values.Uninstall) { ApplicationName = applicationName };

    // associated values
    public string? ApplicationName { get; private set; }
    public string? FullPath { get; private set; }

    // verbatim required constructor implementation for MorphicAssociatedValueEnums
    private AtodOperation(Values value) : base(value) { }
}


