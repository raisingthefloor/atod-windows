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

using Morphic.Core;

namespace Atod.WindowsNative.Uac;

public static class UacUtils
{
    private const string SYSTEM_POLICY_REGISTRY_KEY_PATH = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";

    // this function returns whether or not UAC is enabled on the computer; if the function returns an error on a modern OS, the caller should probably assume that UAC is enabled.
    public static MorphicResult<bool, MorphicUnit> IsUacEnabled()
    {
        Microsoft.Win32.RegistryKey? systemPolicyKey;
        try
        {
            systemPolicyKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(UacUtils.SYSTEM_POLICY_REGISTRY_KEY_PATH);
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        if (systemPolicyKey is null)
        {
            return MorphicResult.ErrorResult();
        }

        const string UAC_ENABLED_VALUE_NAME = "EnableLUA";

        Microsoft.Win32.RegistryValueKind uacEnabledValueKind;
        try
        {
            uacEnabledValueKind = systemPolicyKey!.GetValueKind(UAC_ENABLED_VALUE_NAME);
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        if (uacEnabledValueKind != Microsoft.Win32.RegistryValueKind.DWord)
        {
            return MorphicResult.ErrorResult();
        }

        uint? uacEnabledDataAsUInt32;
        try
        {
            uacEnabledDataAsUInt32 = Atod.WindowsNative.Registry.RegistryUtils.GetRegistryValueDataAsUInt32(systemPolicyKey!, UAC_ENABLED_VALUE_NAME);
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        if (uacEnabledDataAsUInt32 is null)
        {
            return MorphicResult.ErrorResult();
        }

        var uacEnabledDataAsBool = uacEnabledDataAsUInt32 != 0;

        return MorphicResult.OkResult(uacEnabledDataAsBool);
    }

}
