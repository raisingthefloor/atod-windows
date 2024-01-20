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

namespace Atod;

internal struct KnownApplicationProductCode
{
    // main products
    public static readonly Guid DRAGGER = Guid.Parse("{EC8EF634-CE16-4134-B719-CAEA072E5656}"); // {EC8EF634-CE16-4134-B719-CAEA072E5656}
    public static readonly Guid PURPLE_P3 = Guid.Parse("{8B1DFBBC-E163-4099-A23B-0B52A0ADE4BC}"); // {8B1DFBBC-E163-4099-A23B-0B52A0ADE4BC}
    public static readonly Guid READ_AND_WRITE = new Guid(0x355AB00F, 0x48E8, 0x474E, 0xAC, 0xC4, 0xD9, 0x17, 0xBA, 0xFA, 0x4D, 0x58); // {355AB00F-48E8-474E-ACC4-D917BAFA4D58}
    public static readonly Guid SOFTYPE = Guid.Parse("{2DD1676A-A465-4A4C-911C-F37E76675E4C}"); // {2DD1676A-A465-4A4C-911C-F37E76675E4C}

    // components
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE = Guid.Parse("{CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}"); // {CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES = Guid.Parse("{E3475DD5-5EB4-4A80-A323-C2C580E55400}"); // {E3475DD5-5EB4-4A80-A323-C2C580E55400}
    public static readonly Guid FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0 = Guid.Parse("{209841A6-CBAD-4042-8E92-64E76A064288}"); // {209841A6-CBAD-4042-8E92-64E76A064288}
    public static readonly Guid FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY = Guid.Parse("{38464CAB-C140-4C39-BE4C-C68D062130DA}"); // {38464CAB-C140-4C39-BE4C-C68D062130DA}

    //public static Guid? TryFromProductName(string productName)
    //{
    //    switch (productName.ToLowerInvariant())
    //    {
    //        case "readandwrite":
    //            return KnownApplicationProductCode.READ_AND_WRITE;
    //        default:
    //            return null;
    //    }
    //}
}
