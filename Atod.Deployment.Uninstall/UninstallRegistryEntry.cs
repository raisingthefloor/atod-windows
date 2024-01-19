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

namespace Atod.Deployment.Uninstall;

// Windows "remove programs" uninstall registry key details (Windows 2000)
// see: https://web.archive.org/web/20060828031646/http://msdn.microsoft.com/library/en-us/dnw2ksrv/html/w2kserve_chapter2.asp
//
// Windows Installer (MSI) uninstall registry key details
// see: https://web.archive.org/web/20061222030632/http://msdn.microsoft.com/library/en-us/msi/setup/uninstall_registry_key.asp
// see: https://web.archive.org/web/20150113042836/http://msdn.microsoft.com/en-us/library/aa368032(v%3Dvs.85).aspx
// see: https://web.archive.org/web/20150112060139/http://msdn.microsoft.com/en-us/library/aa372105(v=vs.85).aspx
// see: https://learn.microsoft.com/en-us/windows/win32/msi/uninstall-registry-key
//
// Historical notes (on why only "DisplayName" and "UninstallPath" were originally required)
// see: https://web.archive.org/web/20141021085755/http://technet.microsoft.com/en-us/magazine/gg558108.aspx
//
// Notes indicating that the required fields are "DisplayName" and "UninstallString" (in modern Windows)
// see: https://web.archive.org/web/20131203072202/http://msdn.microsoft.com/en-us/library/ms997548.aspx
//
// Notes on parent/child key relationships
// see: https://web.archive.org/web/20150622032139/http://msdn.microsoft.com/en-us/library/cc144162(v%3Dvs.85).aspx
//
// Nullsoft notes on uninstall registry entries (including recommended "QuietUninstallString" value, but also additional values which might be poorly documented otherwise)
// see: https://nsis.sourceforge.io/Add_uninstall_information_to_Add/Remove_Programs

public struct UninstallRegistryEntry
{
    // required fields
    public string DisplayName { get; internal init; }
    public string UninstallString { get; internal init; }
    //
    // optional fields
    // NOTE: InstallLocation is of type REG_EXPAND_SZ (which .GetValue() will auto-expand unless we pass in RegistryValueOptions.DoNotExpandEnvironmentNames as an option in the args)
    public string? InstallLocation { get; internal init; }
    //
    // NOTE: UninstallPath is of type REG_EXPAND_SZ (which .GetValue() will auto-expand unless we pass in RegistryValueOptions.DoNotExpandEnvironmentNames as an option in the args)
    // NOTE: UnexpandedUninstallPath is the legacy "full path" to the uninstaller
    // NOTE: instead of passing this through as a parameter, we simply assign it to "UninstallString" if "UninstallString" is otherwise not found
    //public string? LegacyUninstallPath { get; internal init; }
    //
    public string? Publisher { get; internal init; }
    public uint? VersionMajor { get; internal init; }
    public uint? VersionMinor { get; internal init; }
    //
    public string? ParentKeyName { get; internal init; }
    public string? ParentDisplayName { get; internal init; }
    //
    // NOTE: QuietUninstallString is written "by convention"; we have not found any documentation which specifies it
    public string? QuietUninstallString { get; internal init; }
}
