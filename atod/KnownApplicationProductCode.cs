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

using System;

namespace Atod;

internal struct KnownApplicationProductCode
{
    // main products
    public static readonly Guid CLICK_N_TYPE = Guid.Parse("{CC02581D-B1F9-4B22-8E82-024B9D8EB702}"); // {CC02581D-B1F9-4B22-8E82-024B9D8EB702}
    public static readonly Guid DON_JOHNSTON_CO_WRITER = Guid.Parse("{563C31AC-E23D-44E4-A80B-BA025197A8FB}"); // {563C31AC-E23D-44E4-A80B-BA025197A8FB}
    public static readonly Guid DRAGGER = Guid.Parse("{EC8EF634-CE16-4134-B719-CAEA072E5656}"); // {EC8EF634-CE16-4134-B719-CAEA072E5656}
    public static readonly Guid PURPLE_P3 = Guid.Parse("{8B1DFBBC-E163-4099-A23B-0B52A0ADE4BC}"); // {8B1DFBBC-E163-4099-A23B-0B52A0ADE4BC}
    public static readonly Guid READ_AND_WRITE = new Guid(0x355AB00F, 0x48E8, 0x474E, 0xAC, 0xC4, 0xD9, 0x17, 0xBA, 0xFA, 0x4D, 0x58); // {355AB00F-48E8-474E-ACC4-D917BAFA4D58}
    public static readonly Guid SOFTYPE = Guid.Parse("{2DD1676A-A465-4A4C-911C-F37E76675E4C}"); // {2DD1676A-A465-4A4C-911C-F37E76675E4C}

    // components
    public static readonly Guid CLARO_SOFTWARE_CAPTURE = Guid.Parse("{8F0C38AB-BFAD-4DA4-A1CB-B24CDDA39B6A}"); // {8F0C38AB-BFAD-4DA4-A1CB-B24CDDA39B6A}
    public static readonly Guid CLARO_SOFTWARE_CLAROIDEAS = Guid.Parse("{3E7F6C59-C51F-49A1-86A2-D43A5E96F063}"); // {3E7F6C59-C51F-49A1-86A2-D43A5E96F063}
    public static readonly Guid CLARO_SOFTWARE_CLAROREAD = Guid.Parse("{0B1861FC-2864-4ED3-974A-CD1FC0C4943D}"); // {0B1861FC-2864-4ED3-974A-CD1FC0C4943D}
    public static readonly Guid CLARO_SOFTWARE_CLAROREAD_SE = Guid.Parse("{F888E3D6-3065-4A1B-B8E6-FFF1D2C80EF9}"); // {F888E3D6-3065-4A1B-B8E6-FFF1D2C80EF9}
    public static readonly Guid CLARO_SOFTWARE_CLAROVIEW_SCREEN_READER_SCREEN_MARKER = Guid.Parse("{1A7ADEA9-F14F-4986-8B3C-5080E89AB251}"); // {1A7ADEA9-F14F-4986-8B3C-5080E89AB251}
    public static readonly Guid CLARO_SOFTWARE_SCAN_SCREEN_PLUS = Guid.Parse("{A0EAA828-4D9D-421D-8753-E731273A1417}"); // {A0EAA828-4D9D-421D-8753-E731273A1417}
    public static readonly Guid CLARO_SOFTWARE_SCAN2TEXT = Guid.Parse("{7E73A96E-C053-49B5-89A2-C4F8969A8825}"); // {7E73A96E-C053-49B5-89A2-C4F8969A8825}
    public static readonly Guid DOLPHIN_ORPHEUS = Guid.Parse("{A6C63D7A-BE1C-4E4E-B002-0609E1E664F0}"); // {A6C63D7A-BE1C-4E4E-B002-0609E1E664F0}
    public static readonly Guid DOLPHIN_SAM = Guid.Parse("{7868AFC8-CBA4-4094-B3B2-FB20FF0021A5}"); // {7868AFC8-CBA4-4094-B3B2-FB20FF0021A5}
    public static readonly Guid DOLPHIN_SAM_64_BIT_ADDON = Guid.Parse("{D6FD1376-C54C-4AED-9433-DFC43F7C0AE9}"); // {D6FD1376-C54C-4AED-9433-DFC43F7C0AE9}
    public static readonly Guid DOLPHIN_SAM_VOCALIZER_EXPRESSIVE_ENGLISH_TOM = Guid.Parse("{80E5AAF3-3407-489D-BD5D-6FBA6EA66CD0}"); // {80E5AAF3-3407-489D-BD5D-6FBA6EA66CD0}
    public static readonly Guid DOLPHIN_SAM_VOCALIZER_EXPRESSIVE_VOCALIZER_EXPRESSIVE_COMMON_FILES = Guid.Parse("{B06FD4B9-704D-43A5-B100-C5C628FE0885}"); // {B06FD4B9-704D-43A5-B100-C5C628FE0885}
    public static readonly Guid DOLPHIN_SCREEN_READER = Guid.Parse("{409F5697-C6F0-4FBA-B7EA-F3562AAEB0F3}"); // {409F5697-C6F0-4FBA-B7EA-F3562AAEB0F3}
    public static readonly Guid DOLPHIN_SUPERNOVA_MAGNIFIER = Guid.Parse("{07BE02CC-A202-4163-AAE1-AE6939BE6805}"); // {07BE02CC-A202-4163-AAE1-AE6939BE6805}
    public static readonly Guid DOLPHIN_SUPERNOVA_MAGNIFIER_AND_SPEECH = Guid.Parse("{3174CBFE-7EBC-47C7-9EF6-D0E71A536BE9}"); // {3174CBFE-7EBC-47C7-9EF6-D0E71A536BE9}
    public static readonly Guid FREEDOM_SCIENTIFIC_ACC_EVENT_CACHE = Guid.Parse("{91CAE8B6-F8C5-4CE2-9DF1-5EC1C46C6AD5}"); // {91CAE8B6-F8C5-4CE2-9DF1-5EC1C46C6AD5}
    public static readonly Guid FREEDOM_SCIENTIFIC_AUTHORIZATION = Guid.Parse("{B82C22BC-37A1-475A-B85F-FBA89830CB44}"); // {B82C22BC-37A1-475A-B85F-FBA89830CB44}
    public static readonly Guid FREEDOM_SCIENTIFIC_ELEVATION = Guid.Parse("{AF6A5953-FE5F-451C-BD86-D0EB3F76A6E0}"); // {AF6A5953-FE5F-451C-BD86-D0EB3F76A6E0}
    public static readonly Guid FREEDOM_SCIENTIFIC_ERROR_REPORTING = Guid.Parse("{136F1D08-5DD2-4B52-8B2E-FA70AFE505A5}"); // {136F1D08-5DD2-4B52-8B2E-FA70AFE505A5}
    public static readonly Guid FREEDOM_SCIENTIFIC_GATE_MANAGER = Guid.Parse("{03DD9561-31E3-4733-8432-C52356C89CF4}"); // {03DD9561-31E3-4733-8432-C52356C89CF4}
    public static readonly Guid FREEDOM_SCIENTIFIC_GLOBAL_HOOKS_DISPATCHER = Guid.Parse("{8DD4BD2A-7043-423B-A598-2465F57F382E}"); // {8DD4BD2A-7043-423B-A598-2465F57F382E}
    public static readonly Guid FREEDOM_SCIENTIFIC_HOOK_MANAGER_2_0 = Guid.Parse("{B5B7D6B7-111D-4A6A-824D-EB4631F5C722}"); // {B5B7D6B7-111D-4A6A-824D-EB4631F5C722}
    public static readonly Guid FREEDOM_SCIENTIFIC_KEYBOARD_MANAGER = Guid.Parse("{7671E8C5-0279-4432-AB0D-382128C4020B}"); // {7671E8C5-0279-4432-AB0D-382128C4020B}
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE = Guid.Parse("{CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}"); // {CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES = Guid.Parse("{E3475DD5-5EB4-4A80-A323-C2C580E55400}"); // {E3475DD5-5EB4-4A80-A323-C2C580E55400}
    public static readonly Guid FREEDOM_SCIENTIFIC_RDP_SUPPORT = Guid.Parse("{92F55251-4B8C-4159-98FE-B6744E925E0D}"); // {92F55251-4B8C-4159-98FE-B6744E925E0D}
    public static readonly Guid FREEDOM_SCIENTIFIC_SUPPORT_TOOL = Guid.Parse("{A9207F06-3358-4E6D-9A87-08BAA33EEC71}"); // {A9207F06-3358-4E6D-9A87-08BAA33EEC71}
    public static readonly Guid FREEDOM_SCIENTIFIC_SYNTH = Guid.Parse("{D1B43B83-02A9-4D36-BBC4-ED34532D728D}"); // {D1B43B83-02A9-4D36-BBC4-ED34532D728D}
    public static readonly Guid FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0 = Guid.Parse("{209841A6-CBAD-4042-8E92-64E76A064288}"); // {209841A6-CBAD-4042-8E92-64E76A064288}
    public static readonly Guid FREEDOM_SCIENTIFIC_TELEMETRY = Guid.Parse("{BD25E96E-17B1-487B-8342-0406697FC48D}"); // {BD25E96E-17B1-487B-8342-0406697FC48D}
    public static readonly Guid FREEDOM_SCIENTIFIC_UIA_HOOKS_1_0 = Guid.Parse("{6C654742-DA97-4B78-B1CA-A0859A9B1243}"); // {6C654742-DA97-4B78-B1CA-A0859A9B1243}
    public static readonly Guid FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY = Guid.Parse("{38464CAB-C140-4C39-BE4C-C68D062130DA}"); // {38464CAB-C140-4C39-BE4C-C68D062130DA}
    public static readonly Guid FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_TOM_COMPACT = Guid.Parse("{7AA79F76-442B-4566-A9FF-907CD318D5EF}"); // {7AA79F76-442B-4566-A9FF-907CD318D5EF}
    public static readonly Guid FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_ZOE_COMPACT = Guid.Parse("{8B834522-5FF4-4E4B-A973-99F2C0F0DD0F}"); // {8B834522-5FF4-4E4B-A973-99F2C0F0DD0F}
    public static readonly Guid FREEDOM_SCIENTIFIC_VOICE_ASSISTANT = Guid.Parse("{3DD27DD4-43F7-4921-BC60-BA9FB14CAE0E}"); // {3DD27DD4-43F7-4921-BC60-BA9FB14CAE0E}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_2004 = Guid.Parse("{A2FDC24A-1A0C-403D-B4AF-83A94CF57CAB}"); // {A2FDC24A-1A0C-403D-B4AF-83A94CF57CAB}
    public static readonly Guid TOBII_DYNAVOX_COMMUNICATOR_5 = Guid.Parse("{4C0BEA4E-A9D5-4186-A1CC-3A510F945331}"); // {4C0BEA4E-A9D5-4186-A1CC-3A510F945331}
    public static readonly Guid TOBII_DYNAVOX_LITERAACY_US_EN = Guid.Parse("{A2B80CFE-DC18-4C0C-84C3-5AF65BFF030C}"); // {A2B80CFE-DC18-4C0C-84C3-5AF65BFF030C}
    public static readonly Guid TOBII_DYNAVOX_PCS_FOR_COMMUNICATOR_5 = Guid.Parse("{0CED9059-2F1D-46F7-9968-323E2DCB779F}"); // {0CED9059-2F1D-46F7-9968-323E2DCB779F}
    public static readonly Guid TOBII_DYNAVOX_SONO_FLEX = Guid.Parse("{91E88AE2-D469-4EBC-A29C-27D36F8F606C}"); // {91E88AE2-D469-4EBC-A29C-27D36F8F606C}
    public static readonly Guid TOBII_DYNAVOX_SONO_KEY = Guid.Parse("{A24DDB09-3F27-4DAD-BFE8-D7C61AEA463C}"); // {A24DDB09-3F27-4DAD-BFE8-D7C61AEA463C}
    public static readonly Guid TOBII_DYNAVOX_SONO_LEXIS = Guid.Parse("{B89DF66A-58C2-4A3A-BA70-748361B00D43}"); // {B89DF66A-58C2-4A3A-BA70-748361B00D43}
    public static readonly Guid TOBII_DYNAVOX_SONO_PROMO_FOR_COMMUNICATOR = Guid.Parse("{C39E846A-CE9D-464A-873D-180F7CC9422A}"); // {C39E846A-CE9D-464A-873D-180F7CC9422A}
    public static readonly Guid TOBII_DYNAVOX_SYMBOLSTIX_2 = Guid.Parse("{E52A5CC3-5151-40C8-9DD1-41B811688294}"); // {E52A5CC3-5151-40C8-9DD1-41B811688294}
    public static readonly Guid TOBII_DYNAVOX_US_ENGLISH_VOICES_FOR_TOBII_COMMUNICATOR = Guid.Parse("{87FF87EA-2B2D-4404-BED9-77743D9A4E93}"); // {87FF87EA-2B2D-4404-BED9-77743D9A4E93}
}
