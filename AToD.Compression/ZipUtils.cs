﻿// Copyright 2023-2024 Raising the Floor - US, Inc.
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
using System.Threading.Tasks;

namespace Atod.Compression;

public class ZipUtils
{
    public static async Task<MorphicResult<MorphicUnit, MorphicUnit>> UnzipAsync(string zipFile, string destinationDirectory)
    {
        try
        {
            await Task.Run(() =>
            {
                // NOTE: this takes a moment, so we await; it would be ideal to show an "indeterminate" state temporarily
                System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, destinationDirectory);
            });
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }

        return MorphicResult.OkResult();
    }
}
