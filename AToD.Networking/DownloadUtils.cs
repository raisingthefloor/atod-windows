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
using System.Net;
using System.Threading.Tasks;
using System;

namespace AToD.Networking;

public static class DownloadUtils
{
    // NOTE: this variant of DownloadFile downloads the specified file to the current user's temporary folder; it returns a path to the filename
    // NOTE: we should look into adding "file cleanup" code to DownloadFileAsync, in case the download was aborted
    internal static async Task<MorphicResult<string, MorphicUnit>> DownloadFileAsync(Uri uri, Action<double>? progressFunction = null)
    {
        // create a unique, zero-length file to store the download
        string destinationPath;
        try
        {
            destinationPath = System.IO.Path.GetTempFileName();
        }
        catch
        {
            // NOTE: in the future, we may want to return an appropriate error to let the caller know that we couldn't create a temporary file to store the downloaded data
            return MorphicResult.ErrorResult();
        }

        var result = await DownloadUtils.DownloadFileAsync(uri, destinationPath, true, progressFunction);
        if (result.IsError == true)
        {
            return MorphicResult.ErrorResult();
        }

        return MorphicResult.OkResult(destinationPath);
    }


    // NOTE: we should look into adding "file cleanup" code to DownloadFileAsync, in case the download was aborted
    internal static async Task<MorphicResult<MorphicUnit, MorphicUnit>> DownloadFileAsync(Uri uri, string destinationPath, bool overwriteExistingFile, Action<double>? progressFunction = null)
    {
        // NOTE: WebClient is deprecated, but we have been unable to find any other mechanism that consistently provides the total download size (i.e. content size).
        //       We tried System.Net.Http.HttpClient and it appears to provide _no_ way to get this information; trying to get the content length from the read stream results in an exception
        //       We tried Windows.Web.Http.HttpClient and this will provide the info to us in the Progress parameter of the GetAsync result -- but only if we download the entire file into memory (which doesn't work for our downloads which are often in the 100s of MBs) before saving to disk
        //       We tried using BackgroundDownloader, but that only works inside an AppContainer--and we have concerns about being able to protect the file between download and installation (i.e. know what our checksum is when downloaded and then again verify that when we open up the MSI installer)
        //       The one other potential option is BITS--which we need to look into.  It may be the best replacement (and may effectively be what the BackgroundDownloader which requires an AppContainer is using)

#pragma warning disable SYSLIB0014 // Type or member is obsolete
        var webClient = new System.Net.WebClient();
#pragma warning restore SYSLIB0014 // Type or member is obsolete

        // NOTE: for now, we only call the progressComplete callback if progress has increased at least 0.1% since the last callback
        const double MINIMUM_PERCENTAGE_INCREASE_BETWEEN_PROGRESS_CALLBACKS = 0.001;
        //
        double lastPercentageComplete = 0;
        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler((/*object*/ sender, /*DownloadProgressChangedEventArgs*/ e) =>
        {
            if (e.TotalBytesToReceive > 0)
            {
                // if our progress has increased by a whole-digit percent, then update our caller
                var percentageComplete = ((double)e.BytesReceived) / ((double)e.TotalBytesToReceive);
                if (percentageComplete > lastPercentageComplete + MINIMUM_PERCENTAGE_INCREASE_BETWEEN_PROGRESS_CALLBACKS)
                {
                    lastPercentageComplete = percentageComplete;

                    _ = Task.Run(() =>
                    {
                        progressFunction?.Invoke(percentageComplete);
                    });
                }
            }
        });

        if (overwriteExistingFile == false && System.IO.File.Exists(destinationPath) == true)
        {
            return MorphicResult.ErrorResult();
        }

        try
        {
            // NOTE: this will always overwrite the existing file; therefore we do a manual check (above) to protect against overwrite
            await webClient.DownloadFileTaskAsync(uri, destinationPath);

            return MorphicResult.OkResult();
        }
        catch
        {
            // NOTE: we may need to clean up our download here!
            return MorphicResult.ErrorResult();
        }
    }
}
