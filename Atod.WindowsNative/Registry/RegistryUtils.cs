// Copyright 2024 Raising the Floor - US, Inc.
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

namespace Atod.WindowsNative.Registry;

public static class RegistryUtils
{
    // NOTE: this registry key helper utility converts the Win32 dword into an actual dword (i.e. uint32); this is necessary since the built-in .NET class treats DWORDs like SDWORDs (i.e. returns them as Int32s instead of UInt32s)
    public static uint? GetRegistryValueDataAsUInt32(Microsoft.Win32.RegistryKey key, string? valueName)
    {
        // NOTE: although the value is a DWORD, the Windows registry library treats this as an Int32 (SDWORD) instead of as a UInt32 (DWORD)
        int? valueDataAsInt32 = (int?)key.GetValue(valueName);
        if (valueDataAsInt32 is null)
        {
            return null;
        }
        // bitwise-cast the SDWORD to a DWORD
        var valueDataAsUInt32 = unchecked((uint)valueDataAsInt32!.Value);

        return valueDataAsUInt32;
    }
}
