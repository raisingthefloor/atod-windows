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
    public static readonly Guid DUXBURY_BRAILLE_TRANSLATOR = Guid.Parse("{7E6283B4-390C-4278-8E78-CF9A5FEDD4D5}"); // {7E6283B4-390C-4278-8E78-CF9A5FEDD4D5}
    public static readonly Guid EQUATIO = Guid.Parse("{880BE667-C890-40D6-AD30-BB46089BCFE3}"); // {880BE667-C890-40D6-AD30-BB46089BCFE3}
    public static readonly Guid GHOTIT = Guid.Parse("{9DA08608-3D8A-48F6-820E-6B6FC7DEA78A}"); // {9DA08608-3D8A-48F6-820E-6B6FC7DEA78A}
    public static readonly Guid KURZWEIL_1000 = Guid.Parse("{797FB785-D88C-40D9-83BD-CDD34DBB7EC6}"); // {797FB785-D88C-40D9-83BD-CDD34DBB7EC6}
    public static readonly Guid KURZWEIL_3000 = Guid.Parse("{937E8874-9FC0-4954-8CAA-4C8533E1CB20}"); // {937E8874-9FC0-4954-8CAA-4C8533E1CB20}
    public static readonly Guid NATURAL_READER = Guid.Parse("{B95170BA-E2E6-471B-8EB6-DFB0D278D10B}"); // {B95170BA-E2E6-471B-8EB6-DFB0D278D10B}
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
    public static readonly Guid FREEDOM_SCIENTIFIC_BRAILLE = Guid.Parse("{C1561B87-EA5B-46E9-8E60-61017B0E5270}"); // {C1561B87-EA5B-46E9-8E60-61017B0E5270}
    public static readonly Guid FREEDOM_SCIENTIFIC_DOCUMENT_SERVER = Guid.Parse("{D146C431-EA89-4528-8C05-F33CEFE989B7}"); // {D146C431-EA89-4528-8C05-F33CEFE989B7}
    public static readonly Guid FREEDOM_SCIENTIFIC_ELEVATION = Guid.Parse("{AF6A5953-FE5F-451C-BD86-D0EB3F76A6E0}"); // {AF6A5953-FE5F-451C-BD86-D0EB3F76A6E0}
    public static readonly Guid FREEDOM_SCIENTIFIC_BOOK_SEARCH = Guid.Parse("{E5BAA0EA-39A4-46DD-BB41-F7BBA99AED7A}"); // {E5BAA0EA-39A4-46DD-BB41-F7BBA99AED7A}
    public static readonly Guid FREEDOM_SCIENTIFIC_ELOQUENCE = Guid.Parse("{F4DA19E5-A560-4313-8623-3493DCE3C681}"); // {F4DA19E5-A560-4313-8623-3493DCE3C681}
    public static readonly Guid FREEDOM_SCIENTIFIC_ERROR_REPORTING = Guid.Parse("{136F1D08-5DD2-4B52-8B2E-FA70AFE505A5}"); // {136F1D08-5DD2-4B52-8B2E-FA70AFE505A5}
    public static readonly Guid FREEDOM_SCIENTIFIC_FINE_READER = Guid.Parse("{73B95EC9-F084-4C2B-A190-C3F2381210BC}"); // {73B95EC9-F084-4C2B-A190-C3F2381210BC}
    public static readonly Guid FREEDOM_SCIENTIFIC_FS_READER_3_0 = Guid.Parse("{8400289E-E434-4948-A7F6-43622D29251F}"); // {8400289E-E434-4948-A7F6-43622D29251F}
    public static readonly Guid FREEDOM_SCIENTIFIC_FUSION_2024 = Guid.Parse("{7FF11167-228A-41F0-A0AA-413417FD3DF3}"); // {7FF11167-228A-41F0-A0AA-413417FD3DF3}
    public static readonly Guid FREEDOM_SCIENTIFIC_FUSION_INTERFACE = Guid.Parse("{481435FF-97AC-11E6-8DDB-64006A5729E1}"); // {481435FF-97AC-11E6-8DDB-64006A5729E1}
    public static readonly Guid FREEDOM_SCIENTIFIC_GATE_MANAGER = Guid.Parse("{03DD9561-31E3-4733-8432-C52356C89CF4}"); // {03DD9561-31E3-4733-8432-C52356C89CF4}
    public static readonly Guid FREEDOM_SCIENTIFIC_GLOBAL_HOOKS_DISPATCHER = Guid.Parse("{8DD4BD2A-7043-423B-A598-2465F57F382E}"); // {8DD4BD2A-7043-423B-A598-2465F57F382E}
    public static readonly Guid FREEDOM_SCIENTIFIC_HOOK_MANAGER_2_0 = Guid.Parse("{B5B7D6B7-111D-4A6A-824D-EB4631F5C722}"); // {B5B7D6B7-111D-4A6A-824D-EB4631F5C722}
    public static readonly Guid FREEDOM_SCIENTIFIC_KEYBOARD_MANAGER = Guid.Parse("{7671E8C5-0279-4432-AB0D-382128C4020B}"); // {7671E8C5-0279-4432-AB0D-382128C4020B}
    public static readonly Guid FREEDOM_SCIENTIFIC_IMPORT_PRINTER_1 = Guid.Parse("{98C3AB73-25BE-41B3-A194-65596D82905E}"); // {98C3AB73-25BE-41B3-A194-65596D82905E}
    public static readonly Guid FREEDOM_SCIENTIFIC_IMPORT_PRINTER_2 = Guid.Parse("{AA400467-BF94-4ACD-AEC7-2E73FC45C455}"); // {AA400467-BF94-4ACD-AEC7-2E73FC45C455}
    public static readonly Guid FREEDOM_SCIENTIFIC_IMPORT_PRINTER_3 = Guid.Parse("{B1983570-5A4E-4B70-A9A0-B687EDFC96BE}"); // {B1983570-5A4E-4B70-A9A0-B687EDFC96BE}
    public static readonly Guid FREEDOM_SCIENTIFIC_JAWS_2024_BASE = Guid.Parse("{9D12E9E0-7E29-461A-A757-7C830E00AFC4}"); // {9D12E9E0-7E29-461A-A757-7C830E00AFC4}
    public static readonly Guid FREEDOM_SCIENTIFIC_JAWS_2024_LANGUAGE_ENU = Guid.Parse("{8672D29D-F72B-4435-91F0-254FB2FC2573}"); // {8672D29D-F72B-4435-91F0-254FB2FC2573}
    public static readonly Guid FREEDOM_SCIENTIFIC_JAWS_START = Guid.Parse("{56DD7A70-E2AF-4EAF-9680-602CABE99B19}"); // {56DD7A70-E2AF-4EAF-9680-602CABE99B19}
    public static readonly Guid FREEDOM_SCIENTIFIC_JAWS_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES = Guid.Parse("{AE1E7553-752E-4D04-9695-EE1FB83C54AE}"); // {AE1E7553-752E-4D04-9695-EE1FB83C54AE}
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_EXTERNAL_VIDEO_INTERFACE = Guid.Parse("{CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}"); // {CBABC63D-8FF4-11E2-8181-B8AC6F9E17F4}
    public static readonly Guid FREEDOM_SCIENTIFIC_MAGIC_TRAINING_TABLE_OF_CONTENTS_DAISY_FILES = Guid.Parse("{E3475DD5-5EB4-4A80-A323-C2C580E55400}"); // {E3475DD5-5EB4-4A80-A323-C2C580E55400}
    public static readonly Guid FREEDOM_SCIENTIFIC_OCR_X64 = Guid.Parse("{194B7DA0-20B8-4E8B-9AFB-4D893187A9A9}"); // {194B7DA0-20B8-4E8B-9AFB-4D893187A9A9}
    public static readonly Guid FREEDOM_SCIENTIFIC_OCR_X86 = Guid.Parse("{72596573-EEDF-4173-BF45-71E8FA0067DD}"); // {72596573-EEDF-4173-BF45-71E8FA0067DD}
    public static readonly Guid FREEDOM_SCIENTIFIC_OCR_X64_OPENBOOK_9_0 = Guid.Parse("{07D0D31E-333E-49CC-8BE2-AEA4A64A7D62}"); // {07D0D31E-333E-49CC-8BE2-AEA4A64A7D62}
    public static readonly Guid FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X64 = Guid.Parse("{1CCBF2EE-E481-4A55-B7AF-EE729078B5EE}"); // {1CCBF2EE-E481-4A55-B7AF-EE729078B5EE}
    public static readonly Guid FREEDOM_SCIENTIFIC_OCR_TOMBSTONE_X86 = Guid.Parse("{9CBFFF67-CB9F-4D77-9093-1B456381EC9D}"); // {9CBFFF67-CB9F-4D77-9093-1B456381EC9D}
    public static readonly Guid FREEDOM_SCIENTIFIC_OMNIPAGE_16 = Guid.Parse("{9A5A9BA6-5B13-4D31-8B00-438FF59B02F0}"); // {9A5A9BA6-5B13-4D31-8B00-438FF59B02F0}
    public static readonly Guid FREEDOM_SCIENTIFIC_OMNIPAGE_19 = Guid.Parse("{FC72F904-F3FA-4711-B591-376F7207EC00}"); // {FC72F904-F3FA-4711-B591-376F7207EC00}
    public static readonly Guid FREEDOM_SCIENTIFIC_OMNIPAGE_20 = Guid.Parse("{E63CDB0E-C508-44CA-AE57-8733ACA0AEDC}"); // {E63CDB0E-C508-44CA-AE57-8733ACA0AEDC}
    public static readonly Guid FREEDOM_SCIENTIFIC_OPEN_BOOK_9_0 = Guid.Parse("{4E590917-B265-4101-B75A-F71A2EA540F6}"); // {4E590917-B265-4101-B75A-F71A2EA540F6}
    public static readonly Guid FREEDOM_SCIENTIFIC_RDP_SUPPORT = Guid.Parse("{92F55251-4B8C-4159-98FE-B6744E925E0D}"); // {92F55251-4B8C-4159-98FE-B6744E925E0D}
    public static readonly Guid FREEDOM_SCIENTIFIC_SUPPORT_TOOL = Guid.Parse("{A9207F06-3358-4E6D-9A87-08BAA33EEC71}"); // {A9207F06-3358-4E6D-9A87-08BAA33EEC71}
    public static readonly Guid FREEDOM_SCIENTIFIC_SYNTH = Guid.Parse("{D1B43B83-02A9-4D36-BBC4-ED34532D728D}"); // {D1B43B83-02A9-4D36-BBC4-ED34532D728D}
    public static readonly Guid FREEDOM_SCIENTIFIC_TALKING_INSTALLER_18_0 = Guid.Parse("{209841A6-CBAD-4042-8E92-64E76A064288}"); // {209841A6-CBAD-4042-8E92-64E76A064288}
    public static readonly Guid FREEDOM_SCIENTIFIC_TELEMETRY = Guid.Parse("{BD25E96E-17B1-487B-8342-0406697FC48D}"); // {BD25E96E-17B1-487B-8342-0406697FC48D}
    public static readonly Guid FREEDOM_SCIENTIFIC_TEXT_TO_AUDIO = Guid.Parse("{8D2FCEFD-5D2E-4007-B640-1575700B6172}"); // {8D2FCEFD-5D2E-4007-B640-1575700B6172}
    public static readonly Guid FREEDOM_SCIENTIFIC_TOUCH_SERVER = Guid.Parse("{4466ADE6-D352-4BFF-904C-B7948BA89982}"); // {4466ADE6-D352-4BFF-904C-B7948BA89982}
    public static readonly Guid FREEDOM_SCIENTIFIC_UIA_HOOKS_1_0 = Guid.Parse("{6C654742-DA97-4B78-B1CA-A0859A9B1243}"); // {6C654742-DA97-4B78-B1CA-A0859A9B1243}
    public static readonly Guid FREEDOM_SCIENTIFIC_USB_CAMERA_DRIVER = Guid.Parse("{C9FFAB1B-D20C-46CC-B72E-F1A2038A005C}"); // {C9FFAB1B-D20C-46CC-B72E-F1A2038A005C}
    public static readonly Guid FREEDOM_SCIENTIFIC_UTILITIES = Guid.Parse("{A334FFCA-53ED-4C84-9A60-48CA885382AB}"); // {A334FFCA-53ED-4C84-9A60-48CA885382AB}
    public static readonly Guid FREEDOM_SCIENTIFIC_VIDEO_ACCESSIBILITY = Guid.Parse("{38464CAB-C140-4C39-BE4C-C68D062130DA}"); // {38464CAB-C140-4C39-BE4C-C68D062130DA}
    public static readonly Guid FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_TOM_COMPACT = Guid.Parse("{7AA79F76-442B-4566-A9FF-907CD318D5EF}"); // {7AA79F76-442B-4566-A9FF-907CD318D5EF}
    public static readonly Guid FREEDOM_SCIENTIFIC_VOCALIZER_EXPRESSIVE_2_2_ZOE_COMPACT = Guid.Parse("{8B834522-5FF4-4E4B-A973-99F2C0F0DD0F}"); // {8B834522-5FF4-4E4B-A973-99F2C0F0DD0F}
    public static readonly Guid FREEDOM_SCIENTIFIC_VOICE_ASSISTANT = Guid.Parse("{3DD27DD4-43F7-4921-BC60-BA9FB14CAE0E}"); // {3DD27DD4-43F7-4921-BC60-BA9FB14CAE0E}
    public static readonly Guid FREEDOM_SCIENTIFIC_WYNN_7_0 = Guid.Parse("{43E9C496-53FF-40E8-840C-F531F7BBF5C9}"); // {43E9C496-53FF-40E8-840C-F531F7BBF5C9}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_2004 = Guid.Parse("{A2FDC24A-1A0C-403D-B4AF-83A94CF57CAB}"); // {A2FDC24A-1A0C-403D-B4AF-83A94CF57CAB}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ARA = Guid.Parse("{37BB69A3-2A6F-4700-84DD-EC578FE1BACA}"); // {37BB69A3-2A6F-4700-84DD-EC578FE1BACA}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_CHT = Guid.Parse("{B15160BA-F8A0-43A6-AF9C-9E0E11F9643B}"); // {B15160BA-F8A0-43A6-AF9C-9E0E11F9643B}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_CSY = Guid.Parse("{869B04CA-5089-466A-99F3-B8DD3EA4CB47}"); // {869B04CA-5089-466A-99F3-B8DD3EA4CB47}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_DAN = Guid.Parse("{7E6D025A-7827-4588-AFBC-15B69362C03B}"); // {7E6D025A-7827-4588-AFBC-15B69362C03B}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_DEU = Guid.Parse("{10DBE187-7828-456D-B56A-8404833FBB5C}"); // {10DBE187-7828-456D-B56A-8404833FBB5C}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ENG = Guid.Parse("{ABF6659A-430D-473B-865C-CD3FCC4F7185}"); // {ABF6659A-430D-473B-865C-CD3FCC4F7185}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ESN = Guid.Parse("{5471F43B-1FDF-4EB2-995A-674FFA4758B2}"); // {5471F43B-1FDF-4EB2-995A-674FFA4758B2}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ETI = Guid.Parse("{DD4CECCD-88FD-4ED9-93F7-213B9AFF5D80}"); // {DD4CECCD-88FD-4ED9-93F7-213B9AFF5D80}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_FIN = Guid.Parse("{319B5E30-B5DD-4CCA-9DBC-E5A215822790}"); // {319B5E30-B5DD-4CCA-9DBC-E5A215822790}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_FRA = Guid.Parse("{E29F3C90-BED2-44E1-A3D0-198D6998711C}"); // {E29F3C90-BED2-44E1-A3D0-198D6998711C}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_HEB = Guid.Parse("{27F5C874-3E70-4A2A-B7FD-B9C7C995CC98}"); // {27F5C874-3E70-4A2A-B7FD-B9C7C995CC98}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_HUN = Guid.Parse("{5649D423-C47F-4800-AD2E-6F43543F5296}"); // {5649D423-C47F-4800-AD2E-6F43543F5296}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ISL = Guid.Parse("{03ACEDE7-5CE9-454A-B580-459E217E0DE3}"); // {03ACEDE7-5CE9-454A-B580-459E217E0DE3}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_ITA = Guid.Parse("{463C3B56-1144-4C98-B732-7E8A13CB029C}"); // {463C3B56-1144-4C98-B732-7E8A13CB029C}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_JPN = Guid.Parse("{41A23CDE-840D-4660-8A7D-D8816FC97002}"); // {41A23CDE-840D-4660-8A7D-D8816FC97002}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_KKZ = Guid.Parse("{26055E8C-B168-4537-AB3C-3C86AFDE530A}"); // {26055E8C-B168-4537-AB3C-3C86AFDE530A}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_KOR = Guid.Parse("{7E15A889-8373-4522-8A0D-6ACEFFF7B3C6}"); // {7E15A889-8373-4522-8A0D-6ACEFFF7B3C6}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_LVI = Guid.Parse("{10C36BDE-BB3E-4229-AA16-DB9B970F7A2F}"); // {10C36BDE-BB3E-4229-AA16-DB9B970F7A2F}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_MKI = Guid.Parse("{762698F8-1001-40C0-837D-063D912B5D52}"); // {762698F8-1001-40C0-837D-063D912B5D52}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_NLD = Guid.Parse("{FC81B51E-4AC2-4AAF-8669-9073BE19BA47}"); // {FC81B51E-4AC2-4AAF-8669-9073BE19BA47}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_NOR = Guid.Parse("{BBE15351-0A19-43B9-84F4-03DB001B3731}"); // {BBE15351-0A19-43B9-84F4-03DB001B3731}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_PLK = Guid.Parse("{CC570A4B-2617-4C81-8CA8-8DD903B6A40F}"); // {CC570A4B-2617-4C81-8CA8-8DD903B6A40F}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_PTB = Guid.Parse("{C5167308-6B50-4F2A-985A-EFB9BB76FB59}"); // {C5167308-6B50-4F2A-985A-EFB9BB76FB59}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_RUS = Guid.Parse("{8C4AFAFB-8659-45D5-B915-CB55B62FD3D7}"); // {8C4AFAFB-8659-45D5-B915-CB55B62FD3D7}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_SKY = Guid.Parse("{FFF38E47-BC35-487B-ACC8-387C083CD221}"); // {FFF38E47-BC35-487B-ACC8-387C083CD221}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_SQI = Guid.Parse("{3539E78F-99AB-4428-BC0F-3DDF6E618C4D}"); // {3539E78F-99AB-4428-BC0F-3DDF6E618C4D}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_SVE = Guid.Parse("{2B5ACAAC-C808-482F-810F-B19CD8F1A54D}"); // {2B5ACAAC-C808-482F-810F-B19CD8F1A54D}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_TRK = Guid.Parse("{E3559B17-C40B-4873-BF56-61FF8A234D11}"); // {E3559B17-C40B-4873-BF56-61FF8A234D11}
    public static readonly Guid FREEDOM_SCIENTIFIC_ZOOMTEXT_LANGUAGE_UKR = Guid.Parse("{AE895877-5B3F-47CF-8135-EEEDAD727065}"); // {AE895877-5B3F-47CF-8135-EEEDAD727065}
    public static readonly Guid FREEDOM_SCIENTIFIC_WOW64_PROXY_OPENBOOK_9_0 = Guid.Parse("{5691110B-7FF5-4622-95FC-63AF49E4C4EB}"); // {5691110B-7FF5-4622-95FC-63AF49E4C4EB}
    public static readonly Guid QUILLSOFT_WORDQ_5 = Guid.Parse("{0E08F500-457B-499F-9FEB-C9DCFAE0D8E0}"); // {0E08F500-457B-499F-9FEB-C9DCFAE0D8E0}
    public static readonly Guid QUILLSOFT_ACAPELA_TTS_FOR_WORDQ_5_CORE = Guid.Parse("{F7D8A82E-8029-4532-B14C-7559B48CAB2A}"); // {F7D8A82E-8029-4532-B14C-7559B48CAB2A}
    public static readonly Guid QUILLSOFT_ACAPELA_TTS_FOR_WORDQ_5_NORTH_AMERICA = Guid.Parse("{54A5D9B8-26A9-4EDF-A35B-01EF54661B8B}"); // {54A5D9B8-26A9-4EDF-A35B-01EF54661B8B}
    public static readonly Guid SENSORY_SOFTWARE_GRID_3 = Guid.Parse("{F4678CF5-7FB8-4BA0-8F57-2847F7ED2AD0}"); // {F4678CF5-7FB8-4BA0-8F57-2847F7ED2AD0}
    public static readonly Guid SENSORY_SOFTWARE_RYAN_AMERICAN_ENGLISH_MALE_ADULT = Guid.Parse("{971865AD-C8EC-4375-B7E2-16C32253DD02}"); // {971865AD-C8EC-4375-B7E2-16C32253DD02}
    public static readonly Guid SENSORY_SOFTWARE_HEATHER_AMERICAN_ENGLISH_FEMALE_ADULT = Guid.Parse("{36350B3A-20AA-4751-90B4-969EEB1497A6}"); // {36350B3A-20AA-4751-90B4-969EEB1497A6}
    public static readonly Guid TECHEDOLOGY_INSTALLER_FOR_INSPIRATION_10_DEPLOYMENT = Guid.Parse("{8AA330A1-76FD-4954-8707-480BEFD98874}"); // {8AA330A1-76FD-4954-8707-480BEFD98874}
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
