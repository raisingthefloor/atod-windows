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
    public static readonly Guid CLICK_N_TYPE = Guid.Parse("{CC02581D-B1F9-4B22-8E82-024B9D8EB702}"); // {CC02581D-B1F9-4B22-8E82-024B9D8EB702}
    public static readonly Guid DRAGGER = Guid.Parse("{EC8EF634-CE16-4134-B719-CAEA072E5656}"); // {EC8EF634-CE16-4134-B719-CAEA072E5656}
    public static readonly Guid PURPLE_P3 = Guid.Parse("{8B1DFBBC-E163-4099-A23B-0B52A0ADE4BC}"); // {8B1DFBBC-E163-4099-A23B-0B52A0ADE4BC}
    public static readonly Guid READ_AND_WRITE = new Guid(0x355AB00F, 0x48E8, 0x474E, 0xAC, 0xC4, 0xD9, 0x17, 0xBA, 0xFA, 0x4D, 0x58); // {355AB00F-48E8-474E-ACC4-D917BAFA4D58}
    public static readonly Guid SOFTYPE = Guid.Parse("{2DD1676A-A465-4A4C-911C-F37E76675E4C}"); // {2DD1676A-A465-4A4C-911C-F37E76675E4C}

    // components
    public static readonly Guid CLARO_SOFTWARE_CLAROREAD = Guid.Parse("{0B1861FC-2864-4ED3-974A-CD1FC0C4943D}"); // {0B1861FC-2864-4ED3-974A-CD1FC0C4943D}
    public static readonly Guid CLARO_SOFTWARE_CLAROREAD_SE = Guid.Parse("{F888E3D6-3065-4A1B-B8E6-FFF1D2C80EF9}"); // {F888E3D6-3065-4A1B-B8E6-FFF1D2C80EF9}
    public static readonly Guid CLARO_SOFTWARE_CLAROVIEW_SCREEN_READER_SCREEN_MARKER = Guid.Parse("{1A7ADEA9-F14F-4986-8B3C-5080E89AB251}"); // {1A7ADEA9-F14F-4986-8B3C-5080E89AB251}
    public static readonly Guid CLARO_SOFTWARE_CLAROIDEAS = Guid.Parse("{3E7F6C59-C51F-49A1-86A2-D43A5E96F063}"); // {3E7F6C59-C51F-49A1-86A2-D43A5E96F063}
    public static readonly Guid CLARO_SOFTWARE_SCAN_SCREEN_PLUS = Guid.Parse("{A0EAA828-4D9D-421D-8753-E731273A1417}"); // {A0EAA828-4D9D-421D-8753-E731273A1417}
    public static readonly Guid CLARO_SOFTWARE_SCAN2TEXT = Guid.Parse("{7E73A96E-C053-49B5-89A2-C4F8969A8825}"); // {7E73A96E-C053-49B5-89A2-C4F8969A8825}
    public static readonly Guid CLARO_SOFTWARE_CAPTURE = Guid.Parse("{8F0C38AB-BFAD-4DA4-A1CB-B24CDDA39B6A}"); // {8F0C38AB-BFAD-4DA4-A1CB-B24CDDA39B6A}
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE = Guid.Parse("{CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}"); // {CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES = Guid.Parse("{E3475DD5-5EB4-4A80-A323-C2C580E55400}"); // {E3475DD5-5EB4-4A80-A323-C2C580E55400}
    public static readonly Guid FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0 = Guid.Parse("{209841A6-CBAD-4042-8E92-64E76A064288}"); // {209841A6-CBAD-4042-8E92-64E76A064288}
    public static readonly Guid FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY = Guid.Parse("{38464CAB-C140-4C39-BE4C-C68D062130DA}"); // {38464CAB-C140-4C39-BE4C-C68D062130DA}
}
