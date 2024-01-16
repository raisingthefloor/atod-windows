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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Atod.WindowsNative.Process;

public static class ProcessUtils
{
    public static MorphicResult<bool, MorphicUnit> IsProcessElevated(System.Diagnostics.Process process)
    {
        bool resultAsBool;

        var processSafeHandle = process.SafeHandle;

        // see: https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocesstoken
        Microsoft.Win32.SafeHandles.SafeFileHandle processTokenHandle;
        try
        {
            var openProcessSuccess = Windows.Win32.PInvoke.OpenProcessToken(processSafeHandle, Windows.Win32.Security.TOKEN_ACCESS_MASK.TOKEN_READ, out processTokenHandle);
            if (openProcessSuccess.Value == 0)
            {
                // optional: capture the win32 "last error" using GetLastError
                return MorphicResult.ErrorResult();
            }
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        //
        try
        {
            // see: https://learn.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-gettokeninformation
            Windows.Win32.Security.TOKEN_ELEVATION_TYPE tokenElevationType;
            // NOTE: Windows.Win32.Security.TOKEN_ELEVATION_TYPE is an enum that does not define a fixed size, so we manually use the size of a uint (which is what TOKEN_ELEVATION_TYPE should be at the OS level)
            var sizeOfTokenElevationType = (uint)Marshal.SizeOf<uint>();
            //
            uint returnLength;
            unsafe {
                Windows.Win32.Security.TOKEN_ELEVATION_TYPE* tokenInformation;
                var getTokenInformationSuccess = Windows.Win32.PInvoke.GetTokenInformation(processTokenHandle, Windows.Win32.Security.TOKEN_INFORMATION_CLASS.TokenElevationType, &tokenInformation, sizeOfTokenElevationType, out returnLength);
                if (getTokenInformationSuccess.Value == 0)
                {
                    // optional: capture the win32 "last error" using GetLastError
                    return MorphicResult.ErrorResult();
                }
                //
                // sanity check
                if (returnLength != sizeOfTokenElevationType)
                {
                    Debug.Assert(false, "Requested TOKEN_ELEVATION_TYPE and passed in a type of that size, but received a result with a different size (" + sizeOfTokenElevationType.ToString() + ") vs (" + returnLength.ToString() + ")");
                    return MorphicResult.ErrorResult();
                }
                tokenElevationType = (Windows.Win32.Security.TOKEN_ELEVATION_TYPE)(uint)tokenInformation;
            }

            resultAsBool = tokenElevationType switch
            {
                Windows.Win32.Security.TOKEN_ELEVATION_TYPE.TokenElevationTypeFull => true,
                _ => false,
            };
        }
        finally
        {
            processTokenHandle.Close();
        }

        return MorphicResult.OkResult(resultAsBool);
    }
}
