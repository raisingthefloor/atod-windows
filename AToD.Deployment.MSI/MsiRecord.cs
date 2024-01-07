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
using System.Diagnostics;
using System.Text;

namespace AToD.Deployment.MSI;

internal struct MsiRecord
{
    private uint _handle;

    public MsiRecord(uint handle)
    {
        _handle = handle;
    }

    // NOTE: GetInteger returns null if the field is null or if the field is a string which cannot be converter to an integer
    public int? GetInteger(uint field)
    {
        var result = ExtendedPInvoke.MsiRecordGetInteger(_handle, field);
        return (result == ExtendedPInvoke.MSI_NULL_INTEGER) ? null : result;
    }

    public string? GetString(uint field)
    {
        StringBuilder resultBuilder = new(0);

        // get the size required for our string
        uint sizeOfBuffer = 0;
        var msiRecordGetStringResult = ExtendedPInvoke.MsiRecordGetString(_handle, field, resultBuilder, ref sizeOfBuffer);
        if (msiRecordGetStringResult == (uint)PInvoke.Win32ErrorCode.ERROR_MORE_DATA)
        {
            // string has more than zero characters; enlarge our buffer and re-request the string (and result)
            //
            // set our buffer to the larger required capacity (note: this does not include the null terminator)
            resultBuilder.Capacity = (int)sizeOfBuffer;
            // increment sizeOfBuffer to allow for the extra character; the next call to MsiRecordGetString requires this larger value
            sizeOfBuffer += 1;
            // out of an abundance of caution, bump up the size of our buffer by one (even though we shouldn't need it, since StringBuilder should already account for the null terminator)
            resultBuilder.EnsureCapacity((int)sizeOfBuffer);
            //
            msiRecordGetStringResult = ExtendedPInvoke.MsiRecordGetString(_handle, field, resultBuilder, ref sizeOfBuffer);
        }

        if (msiRecordGetStringResult == (uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS)
        {
            var result = resultBuilder.ToString();
            Debug.Assert(result.Length == (int)sizeOfBuffer, "MsiRecordGetString returned a string but its length (excluding the null terminator) does not match sizeOfBuffer");
            return result;
        } 
        else
        {
            Debug.Assert(false, "MsiRecordGetString returned an undocumented error: " + msiRecordGetStringResult.ToString());
            return null;
        }
    }

    public string? TryFormatAsString()
    {
        StringBuilder resultBuilder = new(0);

        // get the size required for our string
        uint sizeOfBuffer = 0;
        var msiFormatRecordResult = ExtendedPInvoke.MsiFormatRecord(0, _handle, resultBuilder, ref sizeOfBuffer);
        if (msiFormatRecordResult == (uint)PInvoke.Win32ErrorCode.ERROR_MORE_DATA)
        {
            // string has more than zero characters; enlarge our buffer and re-request the string (and result)
            //
            // set our buffer to the larger required capacity (note: this does not include the null terminator)
            resultBuilder.Capacity = (int)sizeOfBuffer;
            // increment sizeOfBuffer to allow for the extra character; the next call to MsiFormatRecord requires this larger value
            sizeOfBuffer += 1;
            // out of an abundance of caution, bump up the size of our buffer by one (even though we shouldn't need it, since StringBuilder should already account for the null terminator)
            resultBuilder.EnsureCapacity((int)sizeOfBuffer);
            //
            msiFormatRecordResult = ExtendedPInvoke.MsiFormatRecord(0, _handle, resultBuilder, ref sizeOfBuffer);
        }

        if (msiFormatRecordResult == (uint)PInvoke.Win32ErrorCode.ERROR_SUCCESS)
        {
            var result = resultBuilder.ToString();
            Debug.Assert(result.Length == (int)sizeOfBuffer, "MsiFormatRecord returned a string but its length (excluding the null terminator) does not match sizeOfBuffer");
            return result;
        }
        else
        {
            Debug.Assert(false, "MsiFormatRecord returned an undocumented error: " + msiFormatRecordResult.ToString());
            return null;
        }
    }

}
