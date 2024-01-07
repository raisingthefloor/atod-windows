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

namespace atod;

internal enum ExitCode : int
{
    // success
    Success                 = 0x0000,
    // command line related errors
    InvalidCommand          = 0x0001,
    MissingArgument         = 0x0002,
    UnknownProduct          = 0x0003,
    // msi installation-related failures
    WindowsInstallerMiscError = 0x0100,
    // windows service related errors
 //   WindowsServiceMiscError = 0x0200,
}
