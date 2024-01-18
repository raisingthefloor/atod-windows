// Copyright 2023-2024 Raising the Floor - US, Inc.
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Atod.Deployment.Uninstall;

public static class UninstallRegistry
{
    public static MorphicResult<List<string>, MorphicUnit> GetUninstallRegistryKeyNames()
    {
        List<string> registryKeyNames = new();

        var getAllRootUninstallRegistryKeysResult = UninstallRegistry.GetAllRootUninstallRegistryKeys();
        if (getAllRootUninstallRegistryKeysResult.IsError)
        {
            return MorphicResult.ErrorResult();
        }
        var uninstallRegistryKeys = getAllRootUninstallRegistryKeysResult.Value!;

        // iterate through each uninstall registry key, enumerating the products' registry subkey names
        foreach (var uninstallRegistryKey in uninstallRegistryKeys)
        {
            var uninstallRegistrySubKeyNames = uninstallRegistryKey.GetSubKeyNames();
            registryKeyNames.AddRange(uninstallRegistrySubKeyNames);

            uninstallRegistryKey.Dispose();
        }

        return MorphicResult.OkResult(registryKeyNames);
    }

    public static MorphicResult<UninstallRegistryEntry?, MorphicUnit> TryGetUninstallRegistryEntry(string registryKeyName)
    {
        return UninstallRegistry.TryGetUninstallRegistryEntry(null, registryKeyName);
    }

    // NOTE: if rootUninstallRegistryKey is null, this function will search all root registry keys
    private static MorphicResult<UninstallRegistryEntry?, MorphicUnit> TryGetUninstallRegistryEntry(Microsoft.Win32.RegistryKey? rootUninstallRegistryKey, string registrySubKeyName)
    {
        List<Microsoft.Win32.RegistryKey> uninstallRegistryKeys = new();

        bool retrievedAllRootUninstallRegistryKeys = false;

        if (rootUninstallRegistryKey is not null)
        {
            uninstallRegistryKeys.Add(rootUninstallRegistryKey!);
        }
        else
        {
            var getAllRootUninstallRegistryKeysResult = UninstallRegistry.GetAllRootUninstallRegistryKeys();
            if (getAllRootUninstallRegistryKeysResult.IsError)
            {
                return MorphicResult.ErrorResult();
            }
            uninstallRegistryKeys.AddRange(getAllRootUninstallRegistryKeysResult.Value!);
            retrievedAllRootUninstallRegistryKeys = true;
        }

        try
        {
            foreach (var uninstallRegistryKey in uninstallRegistryKeys)
            {
                Microsoft.Win32.RegistryKey? productRegistryKey;
                try
                {
                    productRegistryKey = uninstallRegistryKey.OpenSubKey(registrySubKeyName);
                }
                catch
                {
                    Debug.Assert(false, "Could not access product subkey; we may want to consider 'continue;' instead of returning an error in this circumstance");
                    return MorphicResult.ErrorResult();
                }
                if (productRegistryKey is null)
                {
                    // registry key was not found in this root key
                    continue;
                }

                var allValueNames = productRegistryKey!.GetValueNames();

                // retrieve details of product
                MorphicResult<string?, MorphicUnit> getStringValueResult;
                MorphicResult<uint?, MorphicUnit> getUInt32ValueResult;
                //
                // DisplayName
                getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsString(productRegistryKey!, allValueNames, "DisplayName");
                if (getStringValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry; alternatively, we could return an error
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                if (getStringValueResult.Value is null)
                {
                    // skip this entry; this field was required
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                string displayNameData = getStringValueResult.Value!;
                //
                // UninstallString (or UninstallPath, if UninstallString is not present)
                string uninstallStringData;
                getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsStringOrExpandedString(productRegistryKey!, allValueNames, "UninstallString");
                if (getStringValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry; alternatively, we could return an error
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                if (getStringValueResult.Value is not null)
                {
                    uninstallStringData = getStringValueResult.Value!;
                }
                else
                {
                    // backup, for very old (Windows XP era) installers
                    getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsStringOrExpandedString(productRegistryKey!, allValueNames, "UninstallPath");
                    if (getStringValueResult.IsError)
                    {
                        // if we encountered an error while retrieving this legacy value (rather than it simply not being present), skip this entry; alternatively, we could return an error
                        return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                    }
                    if (getStringValueResult.Value is null)
                    {
                        // skip this entry; this field was required (in at least one of its two forms)
                        return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                    }
                    uninstallStringData = getStringValueResult.Value!;
                }
                //
                // InstallLocation (optional)
                getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsStringOrExpandedString(productRegistryKey!, allValueNames, "InstallLocation");
                if (getStringValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                string? installLocationData = getStringValueResult.Value;
                //
                // Publisher
                getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsString(productRegistryKey!, allValueNames, "Publisher");
                if (getStringValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                string? publisherData = getStringValueResult.Value;
                //
                // VersionMajor
                getUInt32ValueResult = UninstallRegistry.GetProductKeyRegistryValueAsUInt32(productRegistryKey!, allValueNames, "VersionMajor");
                if (getUInt32ValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                uint? versionMajorData = getUInt32ValueResult.Value;
                //
                // VersionMinor
                getUInt32ValueResult = UninstallRegistry.GetProductKeyRegistryValueAsUInt32(productRegistryKey!, allValueNames, "VersionMinor");
                if (getUInt32ValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                uint? versionMinorData = getUInt32ValueResult.Value;
                //
                // ParentKeyName
                getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsString(productRegistryKey!, allValueNames, "ParentKeyName");
                if (getStringValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                string? parentKeyNameData = getStringValueResult.Value;
                //
                // ParentDisplayName
                getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsString(productRegistryKey!, allValueNames, "ParentDisplayName");
                if (getStringValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                string? parentDisplayNameData = getStringValueResult.Value;

                // optional "by convention" strings (Documented by Nullsoft)
                //
                // QuietUninstallString (optional)
                getStringValueResult = UninstallRegistry.GetProductKeyRegistryValueAsStringOrExpandedString(productRegistryKey!, allValueNames, "QuietUninstallString");
                if (getStringValueResult.IsError)
                {
                    // if we encountered an error while retrieving this optional value (rather than it simply not being present), skip this entry
                    return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
                }
                string? quietUninstallStringData = getStringValueResult.Value;

                var uninstallRegistryEntry = new UninstallRegistryEntry()
                {
                    DisplayName = displayNameData,
                    UninstallString = uninstallStringData,
                    //
                    InstallLocation = installLocationData,
                    Publisher = publisherData,
                    VersionMajor = versionMajorData,
                    VersionMinor = versionMinorData,
                    //
                    ParentKeyName = parentKeyNameData,
                    ParentDisplayName = parentDisplayNameData,
                    //
                    QuietUninstallString = quietUninstallStringData,
                };
                return MorphicResult.OkResult<UninstallRegistryEntry?>(uninstallRegistryEntry);
            }
        }
        finally
        {
            if (retrievedAllRootUninstallRegistryKeys == true)
            {
                foreach (var uninstallRegistryKey in uninstallRegistryKeys)
                {
                    uninstallRegistryKey.Dispose();
                }
            }
        }

        // if we could not find the registry key, return null
        return MorphicResult.OkResult<UninstallRegistryEntry?>(null);
    }

    public static MorphicResult<Dictionary<string, UninstallRegistryEntry>, MorphicUnit> GetUninstallRegistryEntries()
    {
        Dictionary<string, UninstallRegistryEntry> result = new();

        var getAllRootUninstallRegistryKeysResult = UninstallRegistry.GetAllRootUninstallRegistryKeys();
        if (getAllRootUninstallRegistryKeysResult.IsError)
        {
            return MorphicResult.ErrorResult();
        }
        var uninstallRegistryKeys = getAllRootUninstallRegistryKeysResult.Value!;

        // iterate through each uninstall registry key, enumerating products (to return UninstallRegistryEntry records for each one)
        foreach (var uninstallRegistryKey in uninstallRegistryKeys)
        {
            var uninstallRegistrySubKeyNames = uninstallRegistryKey.GetSubKeyNames();
            foreach (var productSubKeyName in uninstallRegistrySubKeyNames)
            {
                var tryGetUninstallRegistryEntryResult = UninstallRegistry.TryGetUninstallRegistryEntry(uninstallRegistryKey, productSubKeyName);
                if (tryGetUninstallRegistryEntryResult.IsError == true)
                {
                    // if we encountered an error, return it now
                    return MorphicResult.ErrorResult();
                }
                if (tryGetUninstallRegistryEntryResult.Value is null)
                {
                    Debug.Assert(false, "Product uninstall registry subkey was present, but could not be found");
                    // if we could not get the value, continue; it may have just been uninstalled
                    continue;
                }
                var uninstallRegistryEntry = tryGetUninstallRegistryEntryResult.Value!.Value;

                result.Add(productSubKeyName, uninstallRegistryEntry);
            }

            uninstallRegistryKey.Dispose();
        }

        return MorphicResult.OkResult(result);
    }

    //

    private static MorphicResult<List<Microsoft.Win32.RegistryKey>, MorphicUnit> GetAllRootUninstallRegistryKeys()
    {
        List<Microsoft.Win32.RegistryKey> uninstallRegistryKeys = new();

        // NOTE: there are up to four locations in the registry where uninstall keys are stored (HKLM for native bitness, HKLM for WOW64, HKCU for native bitness, HKCU for WOW64)

        // native-bitness installers, per-machine based installation
        // HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
        try
        {
            var uninstallRegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (uninstallRegistryKey is null)
            {
                return MorphicResult.ErrorResult();
            }
            uninstallRegistryKeys.Add(uninstallRegistryKey!);
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        //
        // on 64-bit machines: 32-bit bit installers, per-machine based installation
        // HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall
        try
        {
            var uninstallRegistryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (uninstallRegistryKey is not null)
            {
                uninstallRegistryKeys.Add(uninstallRegistryKey!);
            }
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        //
        // native-bitness installers, per-user based installation
        // HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
        try
        {
            var uninstallRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (uninstallRegistryKey is null)
            {
                return MorphicResult.ErrorResult();
            }
            uninstallRegistryKeys.Add(uninstallRegistryKey!);
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        //
        // on 64-bit machines: 32-bit bit installers, per-user based installation
        // HKCU\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall
        try
        {
            var uninstallRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (uninstallRegistryKey is not null)
            {
                uninstallRegistryKeys.Add(uninstallRegistryKey!);
            }
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }

        return MorphicResult.OkResult(uninstallRegistryKeys);
    }

    //

    private static bool StringArrayContainsCaseSensitiveString(string[] strings, string value)
    {
        return strings.Contains(value);
    }

    //private static bool StringArrayContainsInvariantString(string[] strings, string value)
    //{
    //    foreach (var stringEntry in strings)
    //    {
    //        if (stringEntry.ToLowerInvariant() == value)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    private static MorphicResult<uint?, MorphicUnit> GetProductKeyRegistryValueAsUInt32(Microsoft.Win32.RegistryKey registryKey, string[] allValueNames, string valueName)
    {
        if (UninstallRegistry.StringArrayContainsCaseSensitiveString(allValueNames, valueName) == true)
        {
            if (registryKey.GetValueKind(valueName) != Microsoft.Win32.RegistryValueKind.DWord)
            {
                Debug.Assert(false, "Uninstall registry product subkey contains '" + valueName + "' of invalid type");
                return MorphicResult.ErrorResult();
            }
            var valueData = Atod.WindowsNative.Registry.RegistryUtils.GetRegistryValueDataAsUInt32(registryKey, valueName)!;
            return MorphicResult.OkResult<uint?>(valueData);
        }
        else
        {
            return MorphicResult.OkResult<uint?>(null);
        }
    }

    private static MorphicResult<string?, MorphicUnit> GetProductKeyRegistryValueAsString(Microsoft.Win32.RegistryKey registryKey, string[] allValueNames, string valueName)
    {
        if (UninstallRegistry.StringArrayContainsCaseSensitiveString(allValueNames, valueName) == true)
        {
            if (registryKey.GetValueKind(valueName) != Microsoft.Win32.RegistryValueKind.String)
            {
                Debug.Assert(false, "Uninstall registry product subkey contains '" + valueName + "' of invalid type");
                return MorphicResult.ErrorResult();
            }
            var valueData = (string)registryKey.GetValue(valueName)!;
            return MorphicResult.OkResult<string?>(valueData);
        }
        else
        {
            return MorphicResult.OkResult<string?>(null);
        }
    }

    private static MorphicResult<string?, MorphicUnit> GetProductKeyRegistryValueAsStringOrExpandedString(Microsoft.Win32.RegistryKey registryKey, string[] allValueNames, string valueName)
    {
        if (UninstallRegistry.StringArrayContainsCaseSensitiveString(allValueNames, valueName) == true)
        {
            if (UninstallRegistry.IsRegistryValueTypeStringOrExpandedString(registryKey, valueName) == false)
            {
                Debug.Assert(false, "Uninstall registry product subkey contains '" + valueName + "' of invalid type");
                return MorphicResult.ErrorResult();
            }
            var valueData = (string)registryKey.GetValue(valueName)!;
            return MorphicResult.OkResult<string?>(valueData);
        }
        else
        {
            return MorphicResult.OkResult<string?>(null);
        }
    }

    private static bool IsRegistryValueTypeStringOrExpandedString(Microsoft.Win32.RegistryKey registryKey, string valueName)
    {
        switch (registryKey.GetValueKind(valueName))
        {
            case Microsoft.Win32.RegistryValueKind.String:
            case Microsoft.Win32.RegistryValueKind.ExpandString:
                // allowed
                return true;
            default:
                // not allowed
                return false;
        }
    }
}