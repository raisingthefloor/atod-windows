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

namespace Atod;

using Atod.UI;
using Morphic.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        // print out program header
        //
        Program.WriteBannerToConsole();

        // determine if our process is running elevated (and also if UAC is enabled by policy)
        bool isCurrentProcessElevated;
        var isProcessElevatedResult = Atod.WindowsNative.Process.ProcessUtils.IsProcessElevated(Process.GetCurrentProcess());
        if (isProcessElevatedResult.IsSuccess)
        {
            isCurrentProcessElevated = isProcessElevatedResult.Value!;
        }
        else
        {
            Debug.Assert(false, "Could not determine if process is elevated; falling back to 'not elevated'");
            isCurrentProcessElevated = false;
        }
        //
        //// NOTE: if UAC is not enabled, it may not be necessary for this process to be elevated
        //var isUacEnabledResult = Atod.WindowsNative.Uac.UacUtils.IsUacEnabled();
        //bool isUacEnabledByPolicy;
        //if (isUacEnabledResult.IsSuccess)
        //{
        //    isUacEnabledByPolicy = isUacEnabledResult.Value!;
        //}
        //else
        //{
        //    Debug.Assert(false, "Could not determine if UAC is enabled by policy; falling back to 'UAC is enabled'");
        //    isUacEnabledByPolicy = true;
        //}

        var showGeneralHelp = false;

        AtodSequenceType? atodSequenceType = new();
        List<IAtodOperation> atodOperations = new();

        ExitCode? exitCode = null;

        // parse the command-line arguments
        //
        var commandLineArgs = Environment.GetCommandLineArgs();
        if (commandLineArgs is not null)
        {
            if (commandLineArgs.Length > 1)
            {
                // parse and process the first command-line argument (i.e. the command)
                var commandArg = commandLineArgs[1]!;
                switch (commandArg.ToUpperInvariant())
                {
                    case "HELP":
                    case "--HELP":
                    case "--?":
                    case "-H":
                        {
                            showGeneralHelp = true;
                        }
                        break;
#if DEBUG
                    case "CHECKSUM":
                        {
                            if (commandLineArgs.Length > 2)
                            {
                                var algorithmOrFilename = commandLineArgs[2];

                                string? fullPath;

                                AtodChecksumAlgorithm? checksumAlgorithm = AtodChecksumAlgorithmFactory.TryFrom(algorithmOrFilename);
                                if (checksumAlgorithm is null)
                                {
                                    // default algorithm
                                    checksumAlgorithm = AtodChecksumAlgorithm.Sha256;

                                    // this argument was a filename
                                    fullPath = algorithmOrFilename;
                                }
                                else
                                {
                                    if (commandLineArgs.Length > 3)
                                    {
                                        fullPath = commandLineArgs[3];
                                    }
                                    else
                                    {
                                        // the path was not provided; the command is incomplete					
                                        Console.WriteLine("Path was not provided.");
                                        Console.WriteLine();
                                        //
                                        Program.WriteCalculateChecksumUsageToConsole();
                                        return (int)ExitCode.MissingArgument;
                                    }
                                }

                                atodOperations = new()
                                {
                                    new IAtodOperation.CalculateChecksum(AtodPath.None, fullPath, checksumAlgorithm!.Value)
                                };
                                atodSequenceType = AtodSequenceType.CalculateChecksum;
                            }
                            else
                            {
                                // the path was not provided; the command is incomplete					
                                Console.WriteLine("Path was not provided.");
                                Console.WriteLine();
                                //
                                Program.WriteCalculateChecksumUsageToConsole();
                                return (int)ExitCode.MissingArgument;
                            }
                        }
                        break;
#endif
                    case "INSTALL":
                        {
                            if (commandLineArgs.Length > 2)
                            {
                                var applicationNameOrInstallerPath = commandLineArgs[2];
#if DEBUG
                                // if the provided "install" argument is the path to an installer, capture its full path
                                var lowercaseApplicationNameOrInstallerPath = applicationNameOrInstallerPath.ToLowerInvariant();
                                string? installerType = null;
                                if (lowercaseApplicationNameOrInstallerPath.Length >= ".msi".Length && lowercaseApplicationNameOrInstallerPath.Substring(lowercaseApplicationNameOrInstallerPath.Length - 4) == ".msi")
                                {
                                    installerType = "msi";
                                }
                                if (installerType is not null)
                                {
                                    string fullPath;
                                    try
                                    {
                                        fullPath = System.IO.Path.GetFullPath(applicationNameOrInstallerPath);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Installer path is invalid.");
                                        Console.WriteLine();
                                        return (int)(ExitCode.InvalidPath);
                                    }

                                    if (System.IO.File.Exists(fullPath) == false)
                                    {
                                        Console.WriteLine("Installer file does not exist.");
                                        Console.WriteLine();
                                        return (int)(ExitCode.FileNotFound);
                                    }

                                    switch (installerType!)
                                    {
                                        case "msi":
                                            atodOperations = new()
                                            {
                                                new IAtodOperation.InstallMsi(AtodPath.None, fullPath, null, RequiresElevation: true)
                                            };
                                            break;
                                        default:
                                            throw new Exception("invalid code path");
                                    }
                                }
                                else
                                {
#endif
                                    // capture the application name
                                    var knownApplication = KnownApplication.TryFromProductName(applicationNameOrInstallerPath);
                                    if (knownApplication is null)
                                    {
                                        Console.WriteLine("Application name is not recognized.");
                                        Console.WriteLine();
                                        return (int)ExitCode.UnknownProduct;
                                    }

                                    // capture the installation operations for the application
                                    atodOperations = knownApplication!.Value.GetInstallOperations();
#if DEBUG
                                }
#endif

                                atodSequenceType = AtodSequenceType.Install;
                            }
                            else
                            {
                                // the application name was not provided; the command is incomplete					
                                Console.WriteLine("Application name was not provided.");
                                Console.WriteLine();
                                //
                                Program.WriteInstallUsageToConsole();
                                return (int)ExitCode.MissingArgument;
                            }
                        }
                        break;
                    case "UNINSTALL":
                        {
                            if (commandLineArgs.Length > 2)
                            {
                                // capture the application
                                string? applicationName;
                                applicationName = commandLineArgs[2];

                                var knownApplication = KnownApplication.TryFromProductName(applicationName);
                                if (knownApplication is null)
                                {
                                    Console.WriteLine("Application name is not recognized.");
                                    Console.WriteLine();
                                    return (int)ExitCode.UnknownProduct;
                                }

                                // capture the installation operations for the application
                                var nullableAtodOperations = knownApplication!.Value.GetUninstallOperations();
                                if (nullableAtodOperations is null)
                                {
                                    Console.WriteLine("Application does not have a registered uninstall procedure.");
                                    Console.WriteLine();
                                    return (int)ExitCode.UninstallerNotRegistered;
                                }
                                atodOperations = nullableAtodOperations!;

                                atodSequenceType = AtodSequenceType.Uninstall;
                            }
                            else
                            {
                                // the application name was not provided; the command is incomplete					
                                Console.WriteLine("Application name was not provided.");
                                Console.WriteLine();
                                //
                                Program.WriteUninstallUsageToConsole();
                                return (int)ExitCode.MissingArgument;
                            }
                        }
                        break;
                    default:
                        {
                            // invalid command
                            Console.WriteLine("Unknown command: " + commandArg);
                            Console.WriteLine();
                            exitCode = ExitCode.InvalidCommand;

                            showGeneralHelp = true;
                        }
                        break;
                }
            }
            else
            {
                // the command argument was missing (i.e. no parameters were passed to our application)
                showGeneralHelp = true;
            }
        }

        if (atodSequenceType is null || showGeneralHelp == true)
        {
            Program.WriteGeneralUsageToConsole();
            return (int)(exitCode ?? ExitCode.Success);
        }

        // NOTE: at this point, we know we have a valid sequence of AToD operations

        // create a collection for paths which will be used by the installation process
        var atodAbsolutePathsWithLowercaseKeys = new Dictionary<string, string>();

        // create a collection of files and folders to clean up after the operations have completed; note that files will be cleaned up first and then folders (since the files could be located inside those folders, and we don't want to deal with arbitrary "file not found" failures)
        var newAbsolutePathsForTemporaryFiles = new List<string>();
        var newAbsolutePathsForTemporaryFolders = new List<string>();

        // variable to track if any uninstallers were skipped (in case some components of a multi-step uninstall were already uninstalled or otherwise not present)
        bool uninstallOperationSkippedDueToNotInstalled = false;

        // if the operations require elevation, make sure that our process is elevated
        bool atodOperationsRequireElevation = false;
        foreach (var operation in atodOperations)
        {
            if (operation.GetRequiresElevation() == true)
            {
                atodOperationsRequireElevation = true;
            }
        }

        if (atodOperationsRequireElevation == true && isCurrentProcessElevated == false)
        {
            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Cannot start AToD operation");
            Console.WriteLine();
            Console.WriteLine("This operation requires elevated privileges (e.g. 'run as Administrator').");
            Console.WriteLine("Please restart atod.exe in a terminal session with elevated privileges.");
            Console.WriteLine();
            //
            return (int)ExitCode.ElevationRequired;
        }

        // execute the operations
        //
        var atodOperationCount = atodOperations.Count;
        //
        string initialProgressBarText = atodSequenceType.Value! switch
        {
            AtodSequenceType.CalculateChecksum => "Calculating checksum...",
            AtodSequenceType.Install => "Starting installation...",
            AtodSequenceType.Uninstall => "Starting uninstallation...",
            _ => throw new Exception("invalid code path")
        };

        bool rebootRequiredAfterSequence = false;

        var progressBar = new ConsoleProgressBar()
        {
            Minimum = 0,
            Maximum = 1.0,
            Value = 0,
            //
            TrailingText = initialProgressBarText ?? "Starting AToD operation...",
        };
        progressBar.Show();
        //
        for (var iOperation = 0; iOperation < atodOperationCount; iOperation += 1)
        {
            var atodOperation = atodOperations[iOperation];

            switch (atodOperation!)
            {
                case IAtodOperation.CalculateChecksum { SourcePath: AtodPath operationSourcePath, Filename: string operationFilename, ChecksumAlgorithm: AtodChecksumAlgorithm operationChecksumAlgorithm }:
#if DEBUG
                    {
                        string fileFullPath;
                        switch (operationSourcePath.Value)
                        {
                            case AtodPath.Values.ExistingPathKey:
                                string? existingPath;
                                var existingPathExists = atodAbsolutePathsWithLowercaseKeys.TryGetValue(operationSourcePath.Key!.ToLowerInvariant(), out existingPath);
                                if (existingPathExists == false)
                                {
                                    Console.WriteLine("Source path for file not found; this probably indicates a download failure (or internal failure).");
                                    Console.WriteLine();
                                    //
                                    return (int)ExitCode.FileNotFound;
                                }
                                fileFullPath = Path.Combine(existingPath!, operationFilename);
                                break;
                            case AtodPath.Values.None:
                                fileFullPath = operationFilename;
                                break;
                            default:
                                throw new Exception("unsupported choice");
//                                throw new Exception("invalid code path");
                        }

                        progressBar.TrailingText = "Calculating checksum...";

                        var calculateChecksumResult = await Program.CalculateChecksumAsync(fileFullPath, operationChecksumAlgorithm);
                        if (calculateChecksumResult.IsError == true)
                        {
                            Debug.Assert(false, "Could not calculate checksum; this may indicate a cryptography library failure");
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Checksum Calculation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Checksum could not be calculated.");
                            return (int)ExitCode.ChecksumOperationFailed;
                        }
                        var checksumAsByteArray = calculateChecksumResult.Value! switch
                        {
                            IAtodChecksum.Sha256 { Checksum: var checksum } => checksum,
                            _ => throw new Exception("unknown algorithm; invalid code path"),
                        };
                        var checksumAsCsvString = String.Join<byte>(", ", checksumAsByteArray);

                        progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                        progressBar.TrailingText = "Checksum: [" + checksumAsCsvString + "]";
                    }
                    break;
#else
                    throw new Exception("invalid code path");
#endif
                case IAtodOperation.CreateRegistryKey { RootKey: Microsoft.Win32.RegistryKey rootKey, SubKeyName: string subKeyName, RequiresElevation: _ }:
                    {
                        // check to see if the subkey already exists
                        try
                        {
                            var existingSubKey = rootKey.OpenSubKey(subKeyName, false);
                            if (existingSubKey is not null)
                            {
                                // non-null means that the subkey already exists, there is nothing to create
                                break;
                            }
                            // NOTE: a null existingSubKey is a success condition and indicates that the registry key does not exist
                        }
                        catch (Exception ex)
                        {
                            // NOTE: if the registry key did not exist, we would have gotten a null result; an exception indicates an access or other failure (i.e. not that the subkey does not exist)
                            Debug.Assert(false, "Could not try to open registry key (to see if it exists); exception: " + ex.Message); // see: https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.registrykey.createsubkey?view=net-8.0
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Registry Key Creation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Registry access failed.");
                            return (int)ExitCode.RegistryAccessFailed;
                        }

                        Microsoft.Win32.RegistryKey? newSubKey;
                        try
                        {
                            newSubKey = rootKey.CreateSubKey(subKeyName, true);
                        }
                        catch (Exception ex)
                        {
                            Debug.Assert(false, "Could not create registry key; exception: " + ex.Message); // see: https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.registrykey.createsubkey?view=net-8.0
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Registry Key Creation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Registry key could not be created.");
                            return (int)ExitCode.RegistryAccessFailed;
                        }
                        if (newSubKey is null)
                        {
                            Debug.Assert(false, "Could not create registry key"); // see: https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.registrykey.createsubkey?view=net-8.0
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Registry Key Creation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Registry key could not be created.");
                            return (int)ExitCode.RegistryAccessFailed;
                        }
                    }
                    break;
                case IAtodOperation.Download { Uri: Uri operationUri, DestinationPath: AtodPath operationDestinationPath, Filename: string operationFilename, Checksum: var operationChecksumAsNullable }:
                    {
                        string? destinationFullPath;
                        switch (operationDestinationPath!.Value)
                        {
                            case AtodPath.Values.CreateTemporaryFolderForNewPathKey:
                                var temporaryFolderKey = operationDestinationPath.Key!.ToLowerInvariant();
                                try
                                {
                                    var tempDirectoryInfo = System.IO.Directory.CreateTempSubdirectory("atod_");
                                    atodAbsolutePathsWithLowercaseKeys[temporaryFolderKey] = tempDirectoryInfo.FullName!;
                                    newAbsolutePathsForTemporaryFolders.Add(tempDirectoryInfo.FullName!);
                                    //
                                    destinationFullPath = Path.Combine(tempDirectoryInfo.FullName, operationFilename);
                                    newAbsolutePathsForTemporaryFiles.Add(destinationFullPath);
                                }
                                catch
                                {
                                    Console.WriteLine("Could not create a temporary folder for downloaded file.");
                                    Console.WriteLine();
                                    //
                                    return (int)ExitCode.FileNotFound;
                                }
                                break;
                            default:
                                throw new Exception("unsupported choice");
//                                throw new Exception("invalid code path");
                        }

                        Stopwatch stopwatch = Stopwatch.StartNew();

                        int? lastProgressPercentAsWholeNumber = null;
                        char[] spinnerChars = new char[] { '/', '-', '\\', '|' };
                        int lastSpinnerCharIndex = 0;
                        //
                        long? elapsedMillisecondsAtLastProgressBarUpdate = null;
                        var progressUpdate = new Action<double>((percentageComplete) =>
                        {
                            var percent = ((double)iOperation / (double)atodOperationCount) + (percentageComplete / (double)atodOperationCount);
                            if (percent != 0)
                            {
                                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                                var percentAsWholeNumber = (int)(percent * 100);
                                if (lastProgressPercentAsWholeNumber is null || lastProgressPercentAsWholeNumber.Value != percentAsWholeNumber)
                                {
                                    progressBar.Value = percent;

                                    lastProgressPercentAsWholeNumber = percentAsWholeNumber;
                                }

                                const long MINIMUM_MILLISECONDS_BETWEEN_TRAILING_TEXT_UPDATES = 250;
                                if (elapsedMillisecondsAtLastProgressBarUpdate is null || elapsedMilliseconds >= elapsedMillisecondsAtLastProgressBarUpdate.Value + MINIMUM_MILLISECONDS_BETWEEN_TRAILING_TEXT_UPDATES)
                                {
                                    var spinnerCharIndex = (lastSpinnerCharIndex + 1) % spinnerChars.Length;
                                    progressBar.TrailingText = spinnerChars[spinnerCharIndex] + " Downloading";
                                    lastSpinnerCharIndex = spinnerCharIndex;

                                    elapsedMillisecondsAtLastProgressBarUpdate = elapsedMilliseconds;
                                }
                            }
                        });

                        var downloadFileResult = await Atod.Networking.DownloadUtils.DownloadFileAsync(operationUri, destinationFullPath!, false, progressUpdate);
                        if (downloadFileResult.IsError == true)
                        {
                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Download Failed");
                            Console.WriteLine();
                            Console.WriteLine("Could not download file.");
                            Console.WriteLine();
                            //
                            return (int)ExitCode.DownloadFailed;
                        }

                        // if a checksum was provided, verify it now
                        if (operationChecksumAsNullable is not null)
                        {
                            var checksum = operationChecksumAsNullable!;

                            progressBar.TrailingText = "Verifying checksum...";

                            var verifyChecksumResult = await Program.VerifyChecksumMatchesAsync(destinationFullPath!, checksum);
                            if (verifyChecksumResult.IsError == true)
                            {
                                Debug.Assert(false, "Could not verify checksum; this may indicate a cryptography library failure");
                                progressBar.Hide();

                                Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Checksum Verification Failed");
                                Console.WriteLine();
                                Console.WriteLine("Checksum on downloaded file could not be verified.");
                                return (int)ExitCode.ChecksumOperationFailed;
                            }
                            var checksumMatches = verifyChecksumResult.Value!;

                            if (checksumMatches == false)
                            {
                                progressBar.Hide();

                                Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Checksum Verification Failed");
                                Console.WriteLine();
                                Console.WriteLine("Downloaded file's checksum does not match.");
                                return (int)ExitCode.ChecksumMismatch;
                            }

                            // if we reach here, the checksum verification was required--and checksum verification passed
                        }

                        progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                        progressBar.TrailingText = "Downloaded";
                    }
                    break;
                case IAtodOperation.InstallExe { SourcePath: AtodPath operationSourcePath, Filename: string operationFilename, CommandLineArgs: var operationCommandLineArgsAsNullable, Conditions: List<IAtodOperationCondition> conditions, RebootRequiredExitCode: var rebootRequiredExitCodeAsNullable, RequiresElevation: bool _ }:
                    {
                        // test installation conditions
                        var evaluateShouldSkipOperationResult = Program.EvaluateShouldSkipOperation(conditions);
                        if (evaluateShouldSkipOperationResult.IsError)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Installation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Application could not be installed.");
                            Console.WriteLine("Could not evaluate conditions for operation.");
                            return (int)ExitCode.ExeInstallerMiscError;
                        }
                        var shouldSkipOperation = evaluateShouldSkipOperationResult.Value!;

                        if (shouldSkipOperation == false)
                        {
                            string exeFileFullPath;
                            switch (operationSourcePath.Value)
                            {
                                case AtodPath.Values.ExistingPathKey:
                                    string? existingPath;
                                    var existingPathExists = atodAbsolutePathsWithLowercaseKeys.TryGetValue(operationSourcePath.Key!.ToLowerInvariant(), out existingPath);
                                    if (existingPathExists == false)
                                    {
                                        Console.WriteLine("Source path for EXE not found; this probably indicates a download failure (or internal failure).");
                                        Console.WriteLine();
                                        //
                                        return (int)ExitCode.FileNotFound;
                                    }
                                    exeFileFullPath = Path.Combine(existingPath!, operationFilename);
                                    break;
                                case AtodPath.Values.None:
                                    exeFileFullPath = operationFilename;
                                    break;
                                default:
                                    throw new Exception("unsupported choice");
                                    //                                throw new Exception("invalid code path");
                            }
                            //
                            // set up the command line arguments
                            string? exeArguments = operationCommandLineArgsAsNullable;

                            progressBar.TrailingText = "Installing";

                            System.Diagnostics.ProcessStartInfo startInfo;
                            if (exeArguments is not null)
                            {
                                startInfo = new System.Diagnostics.ProcessStartInfo(exeFileFullPath, exeArguments!);
                            }
                            else
                            {
                                startInfo = new System.Diagnostics.ProcessStartInfo(exeFileFullPath);
                            }
                            startInfo.UseShellExecute = true;

                            System.Diagnostics.Process? exeProcess;
                            System.ComponentModel.Win32Exception? win32Exception = null;
                            try
                            {
                                exeProcess = System.Diagnostics.Process.Start(startInfo);
                            }
                            catch (System.ComponentModel.Win32Exception ex)
                            {
                                exeProcess = null;
                                win32Exception = ex;
                            }
                            //
                            if (exeProcess is null)
                            {
                                progressBar.Hide();

                                Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Installation Failed");
                                Console.WriteLine();
                                Console.WriteLine("Application could not be installed.");
                                if (win32Exception is not null)
                                {
                                    Console.WriteLine("Win32 error code: " + win32Exception!.ErrorCode.ToString());
                                }
                                return (int)ExitCode.ExeInstallerMiscError;
                            }

                            await exeProcess!.WaitForExitAsync();

                            var rebootRequiredFromOperation = false;

                            var exeExitCode = exeProcess!.ExitCode;
                            if (rebootRequiredExitCodeAsNullable is not null && exeExitCode == rebootRequiredExitCodeAsNullable!.Value)
                            {
                                rebootRequiredFromOperation = true;
                                rebootRequiredAfterSequence = true;
                            }
                            else if (exeExitCode != 0)
                            {
                                progressBar.Hide();

                                Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Installation Failed");
                                Console.WriteLine();
                                Console.WriteLine("Application could not be installed.");
                                Console.WriteLine("EXE installer exit code: " + exeExitCode.ToString());
                                Debug.Assert(false, "EXE exited with non-zero status code; make sure this is not a status code indicating a reboot requirement, etc.");
                                return (int)ExitCode.ExeInstallerMiscError;
                            }

                            progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                            progressBar.TrailingText = rebootRequiredFromOperation switch
                            {
                                false => "Installed",
                                true => "Reboot required to complete install",
                            };
                        }
                        else // if (shouldSkipOperation == true)
                        {
                            // operation skipped
                            //progressBar.TrailingText = "Skipped operation";
                            break;
                        }
                    }
                    break;
                case IAtodOperation.InstallMsi { SourcePath: AtodPath operationSourcePath, Filename: string operationFilename, PropertySettings: var propertySettingsAsNullable, RequiresElevation: bool _}:
                    {
                        string msiFileFullPath;
                        switch (operationSourcePath.Value)
                        {
                            case AtodPath.Values.ExistingPathKey:
                                string? existingPath;
                                var existingPathExists = atodAbsolutePathsWithLowercaseKeys.TryGetValue(operationSourcePath.Key!.ToLowerInvariant(), out existingPath);
                                if (existingPathExists == false)
                                {
                                    Console.WriteLine("Source path for MSI not found; this probably indicates a download failure (or internal failure).");
                                    Console.WriteLine();
                                    //
                                    return (int)ExitCode.FileNotFound;
                                }
                                msiFileFullPath = Path.Combine(existingPath!, operationFilename);
                                break;
                            case AtodPath.Values.None:
                                msiFileFullPath = operationFilename;
                                break;
                            default:
                                throw new Exception("unsupported choice");
//                                throw new Exception("invalid code path");
                        }
                        //
                        // set up the property settings (i.e. installer properties, etc.)
                        var propertySettings = new Dictionary<string, string>();
                        if (propertySettingsAsNullable is not null)
                        {
                            foreach (var (property, value) in propertySettingsAsNullable)
                            {
                                propertySettings.Add(property, value);
                            }
                            propertySettings = propertySettingsAsNullable!;
                        }
                        //
                        // suppress all reboot prompts and the actual reboots; this will cause the operation to return ERROR_SUCCESS_REBOOT_REQUIRED instead of ERROR_SUCCESS if a reboot is required
                        // NOTE: this should be a standard MSI parameter, but if we find MSIs where it this causes issues or is not supported we will need to revisit this flag
                        propertySettings.Add("REBOOT", "ReallySuppress");

                        var windowsInstaller = new Atod.Deployment.Msi.WindowsInstaller();

                        Stopwatch stopwatch = Stopwatch.StartNew();

                        int? lastProgressPercentAsWholeNumber = null;
                        char[] spinnerChars = new char[] { '/', '-', '\\', '|' };
                        int lastSpinnerCharIndex = 0;
                        //
                        long? elapsedMillisecondsAtLastProgressBarUpdate = null;
                        windowsInstaller.ProgressUpdate += (sender, args) =>
                        {
                            var percent = ((double)iOperation / (double)atodOperationCount) + (args.Percent / (double)atodOperationCount);
                            if (percent != 0)
                            {
                                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                                var percentAsWholeNumber = (int)(percent * 100);
                                if (lastProgressPercentAsWholeNumber is null || lastProgressPercentAsWholeNumber.Value != percentAsWholeNumber)
                                {
                                    progressBar.Value = percent;

                                    lastProgressPercentAsWholeNumber = percentAsWholeNumber;
                                }

                                const long MINIMUM_MILLISECONDS_BETWEEN_TRAILING_TEXT_UPDATES = 250;
                                if (elapsedMillisecondsAtLastProgressBarUpdate is null || elapsedMilliseconds >= elapsedMillisecondsAtLastProgressBarUpdate.Value + MINIMUM_MILLISECONDS_BETWEEN_TRAILING_TEXT_UPDATES)
                                {
                                    var spinnerCharIndex = (lastSpinnerCharIndex + 1) % spinnerChars.Length;
                                    progressBar.TrailingText = spinnerChars[spinnerCharIndex] + " Installing";
                                    lastSpinnerCharIndex = spinnerCharIndex;

                                    elapsedMillisecondsAtLastProgressBarUpdate = elapsedMilliseconds;
                                }
                            }
                        };

                        var installResult = await windowsInstaller.InstallAsync(msiFileFullPath, propertySettings);
                        if (installResult.IsError == true)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Installation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Application could not be installed.");
                            Console.WriteLine("Error: " + installResult.Error!.Win32ErrorCode.ToString());
                            return (int)ExitCode.WindowsInstallerMiscError;
                        }
                        var installResultValue = installResult.Value!;
                        rebootRequiredAfterSequence = installResultValue.RebootRequired;

                        // NOTE: some installers may require a reboot before the next steps in the sequence can continue; we may need to expand our installation sequence schema to accomodate those (instead of requesting a reboot after the full sequence completes)

                        // otherwise, we succeeded.
                        progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                        switch (installResultValue.RebootRequired)
                        {
                            case false:
                                progressBar.TrailingText = "Installed";
                                break;
                            case true:
                                progressBar.TrailingText = "Reboot required to complete install";
                                break;
                        }
                    }
                    break;
                case IAtodOperation.UninstallUsingRegistryUninstallString { UninstallSubKeyName: string operationUninstallSubKeyName, OptionalSupplementalArgs: var operationOptionalAddedArgsAsNullable, RebootRequiredExitCode: var rebootRequiredExitCodeAsNullable, RequiresElevation: _ }:
                    {
                        progressBar.TrailingText = "Uninstalling";

                        // retrieve the UninstallRegistryEntry for this application
                        var tryGetUninstallRegistryEntryResult = Atod.Deployment.Uninstall.UninstallRegistry.TryGetUninstallRegistryEntry(operationUninstallSubKeyName);
                        if (tryGetUninstallRegistryEntryResult.IsError)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Uninstallation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Uninstall registry key for this application is unaccessible or corrupted.");
                            return (int)ExitCode.RegistryUninstallerMiscError;
                        }
                        if (tryGetUninstallRegistryEntryResult.Value is null)
                        {
                            // product does not appear to be installed
                            uninstallOperationSkippedDueToNotInstalled = true;
                            break;
                        }
                        var uninstallRegistryEntry = tryGetUninstallRegistryEntryResult.Value!.Value;

                        // NOTE: we prefer the "by convention" quiet uninstaller's QuietUninstallString, but we'll use the UninstallString as a backup
                        string uninstallString = uninstallRegistryEntry.QuietUninstallString ?? uninstallRegistryEntry.UninstallString;

                        // break UninstallString into executable and path
                        var splitResult = Program.SplitUninstallStringIntoFilenameAndArgs(uninstallString);
                        if (splitResult.IsError)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Uninstallation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Uninstall registry key for this application is corrupted.");
                            return (int)ExitCode.RegistryUninstallerMiscError;
                        }
                        string exeFileFullPath = splitResult.Value!.Filename;
                        string? exeArguments = splitResult.Value!.Args;
                        if (operationOptionalAddedArgsAsNullable is not null)
                        {
                            exeArguments = Program.AddSupplementalArgs(exeArguments, operationOptionalAddedArgsAsNullable!);
                        }
                        // NOTE: if our arguments are empty, change the value of exeArguments to null (so that we don't pass in an empty string arguments parameter)
                        if (exeArguments.Trim() == "")
                        {
                            exeArguments = null;
                        }

                        System.Diagnostics.ProcessStartInfo startInfo;
                        if (exeArguments is not null)
                        {
                            startInfo = new System.Diagnostics.ProcessStartInfo(exeFileFullPath, exeArguments!);
                        }
                        else
                        {
                            startInfo = new System.Diagnostics.ProcessStartInfo(exeFileFullPath);
                        }
                        startInfo.UseShellExecute = true;

                        System.Diagnostics.Process? exeProcess;
                        System.ComponentModel.Win32Exception? win32Exception = null;
                        try
                        {
                            exeProcess = System.Diagnostics.Process.Start(startInfo);
                        }
                        catch (System.ComponentModel.Win32Exception ex)
                        {
                            exeProcess = null;
                            win32Exception = ex;
                        }
                        //
                        if (exeProcess is null)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Uninstallation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Application could not be uninstalled.");
                            if (win32Exception is not null)
                            {
                                Console.WriteLine("Win32 error code: " + win32Exception!.ErrorCode.ToString());
                            }
                            return (int)ExitCode.RegistryUninstallerMiscError;
                        }

                        await exeProcess!.WaitForExitAsync();

                        var rebootRequiredFromOperation = false;

                        var exeExitCode = exeProcess!.ExitCode;
                        if (rebootRequiredExitCodeAsNullable is not null && exeExitCode == rebootRequiredExitCodeAsNullable!.Value)
                        {
                            rebootRequiredFromOperation = true;
                            rebootRequiredAfterSequence = true;
                        }
                        else if (exeExitCode != 0)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Uninstallation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Application could not be uninstalled.");
                            Console.WriteLine("Uninstaller exit code: " + exeExitCode.ToString());
                            Debug.Assert(false, "EXE exited with non-zero status code; make sure this is not a status code indicating a reboot requirement, etc.");
                            return (int)ExitCode.RegistryUninstallerMiscError;
                        }

                        progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                        progressBar.TrailingText = rebootRequiredFromOperation switch
                        {
                            false => "Uninstalled",
                            true => "Reboot required to complete uninstall",
                        };
                    }
                    break;
                case IAtodOperation.UninstallUsingWindowsInstaller { WindowsInstallerProductCode: Guid operationWindowsInstallerProductCode, PropertySettings: var propertySettingsAsNullable, RequiresElevation: bool _ }:
                    {
                        // resolve product name into product code
                        var msiProductCode = operationWindowsInstallerProductCode;

                        //
                        // set up the property settings (i.e. installer properties, etc.)
                        var propertySettings = new Dictionary<string, string>();
                        if (propertySettingsAsNullable is not null)
                        {
                            foreach (var (property, value) in propertySettingsAsNullable)
                            {
                                propertySettings.Add(property, value);
                            }
                            propertySettings = propertySettingsAsNullable!;
                        }
                        //
                        // suppress all reboot prompts and the actual reboots; this will cause the operation to return ERROR_SUCCESS_REBOOT_REQUIRED instead of ERROR_SUCCESS if a reboot is required
                        // NOTE: this should be a standard MSI parameter, but if we find MSIs where it this causes issues or is not supported we will need to revisit this flag
                        propertySettings.Add("REBOOT", "ReallySuppress");

                        var windowsInstaller = new Atod.Deployment.Msi.WindowsInstaller();

                        Stopwatch stopwatch = Stopwatch.StartNew();

                        int? lastProgressPercentAsWholeNumber = null;
                        char[] spinnerChars = new char[] { '/', '-', '\\', '|' };
                        int lastSpinnerCharIndex = 0;
                        //
                        long? elapsedMillisecondsAtLastProgressBarUpdate = null;
                        windowsInstaller.ProgressUpdate += (sender, args) =>
                        {
                            var percent = ((double)iOperation / (double)atodOperationCount) + (args.Percent / (double)atodOperationCount);
                            if (percent != 0)
                            {
                                var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                                var percentAsWholeNumber = (int)(percent * 100);
                                if (lastProgressPercentAsWholeNumber is null || lastProgressPercentAsWholeNumber.Value != percentAsWholeNumber)
                                {
                                    progressBar.Value = percent;

                                    lastProgressPercentAsWholeNumber = percentAsWholeNumber;
                                }

                                const long MINIMUM_MILLISECONDS_BETWEEN_TRAILING_TEXT_UPDATES = 250;
                                if (elapsedMillisecondsAtLastProgressBarUpdate is null || elapsedMilliseconds >= elapsedMillisecondsAtLastProgressBarUpdate.Value + MINIMUM_MILLISECONDS_BETWEEN_TRAILING_TEXT_UPDATES)
                                {
                                    var spinnerCharIndex = (lastSpinnerCharIndex + 1) % spinnerChars.Length;
                                    progressBar.TrailingText = spinnerChars[spinnerCharIndex] + " Uninstalling";
                                    lastSpinnerCharIndex = spinnerCharIndex;

                                    elapsedMillisecondsAtLastProgressBarUpdate = elapsedMilliseconds;
                                }
                            }
                        };

                        var uninstallResult = await windowsInstaller.UninstallAsync(msiProductCode, propertySettings);
                        if (uninstallResult.IsError == true)
                        {
                            if (uninstallResult.Error!.Win32ErrorCode == 1605 /* ERROR_UNKNOWN_PRODUCT */) 
                            {
                                // product does not appear to be installed
                                uninstallOperationSkippedDueToNotInstalled = true;
                                break;
                            }

                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Uninstallation Failed");
                            Console.WriteLine();
                            Console.WriteLine("Application could not be uninstalled.");
                            Console.WriteLine("Error: " + uninstallResult.Error!.Win32ErrorCode.ToString());
                            return (int)ExitCode.WindowsInstallerMiscError;
                        }
                        var uninstallResultValue = uninstallResult.Value!;
                        rebootRequiredAfterSequence = uninstallResultValue.RebootRequired;

                        // NOTE: some installers may require a reboot before the next steps in the sequence can continue; we may need to expand our uninstallation sequence schema to accomodate those (instead of recommending/requesting a reboot after the full sequence completes)

                        // otherwise, we succeeded.
                        progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                        switch (uninstallResultValue.RebootRequired)
                        {
                            case false:
                                progressBar.TrailingText = "Uninstalled";
                                break;
                            case true:
                                progressBar.TrailingText = "Reboot required to complete uninstall";
                                break;
                        }
                    }
                    break;
                case IAtodOperation.Unzip { SourcePath: AtodPath operationSourcePath, Filename: string operationFilename, DestinationPath: AtodPath operationDestinationPath }:
                    {
                        string zipFileFullPath;
                        switch (operationSourcePath.Value)
                        {
                            case AtodPath.Values.ExistingPathKey:
                                string? existingPath;
                                var existingPathExists = atodAbsolutePathsWithLowercaseKeys.TryGetValue(operationSourcePath.Key!.ToLowerInvariant(), out existingPath);
                                if (existingPathExists == false)
                                {
                                    Console.WriteLine("Source path for MSI not found; this probably indicates a download failure (or internal failure).");
                                    Console.WriteLine();
                                    //
                                    return (int)ExitCode.FileNotFound;
                                }
                                zipFileFullPath = Path.Combine(existingPath!, operationFilename);
                                break;
                            case AtodPath.Values.None:
                                zipFileFullPath = operationFilename;
                                break;
                            default:
                                throw new Exception("unsupported choice");
//                                throw new Exception("invalid code path");
                        }
                        string destinationFolderPath;
                        switch (operationDestinationPath.Value)
                        {
                            case AtodPath.Values.CreateTemporaryFolderForNewPathKey:
                                var temporaryFolderKey = operationDestinationPath.Key!.ToLowerInvariant();
                                try
                                {
                                    var tempDirectoryInfo = System.IO.Directory.CreateTempSubdirectory("atod_");
                                    atodAbsolutePathsWithLowercaseKeys[temporaryFolderKey] = tempDirectoryInfo.FullName!;
                                    newAbsolutePathsForTemporaryFolders.Add(tempDirectoryInfo.FullName!);
                                    //
                                    destinationFolderPath = tempDirectoryInfo.FullName;
                                }
                                catch
                                {
                                    Console.WriteLine("Could not create a temporary folder for unzipping operation.");
                                    Console.WriteLine();
                                    //
                                    return (int)ExitCode.FileNotFound;
                                }
                                break;
                            default:
                                throw new Exception("unsupported choice");
//                                throw new Exception("invalid code path");
                        }

                        progressBar.TrailingText = "Extracting files";

                        var unzipResult = await Atod.Compression.ZipUtils.UnzipAsync(zipFileFullPath, destinationFolderPath);
                        if (unzipResult.IsError == true)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Extraction Failed");
                            Console.WriteLine();
                            Console.WriteLine("Zip file could not be extracted.");
                            return (int)ExitCode.UnarchiveFailed;
                        }

                        // otherwise, we succeeded.
                        progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                        progressBar.TrailingText = "Files extracted";
                    }
                    break;
                case IAtodOperation.VerifyChecksum { SourcePath: AtodPath operationSourcePath, Filename: string operationFilename, Checksum: IAtodChecksum operationChecksum }:
                    {
                        string fileFullPath;
                        switch (operationSourcePath.Value)
                        {
                            case AtodPath.Values.ExistingPathKey:
                                string? existingPath;
                                var existingPathExists = atodAbsolutePathsWithLowercaseKeys.TryGetValue(operationSourcePath.Key!.ToLowerInvariant(), out existingPath);
                                if (existingPathExists == false)
                                {
                                    Console.WriteLine("Source path for file not found; this probably indicates a download failure (or internal failure).");
                                    Console.WriteLine();
                                    //
                                    return (int)ExitCode.FileNotFound;
                                }
                                fileFullPath = Path.Combine(existingPath!, operationFilename);
                                break;
                            case AtodPath.Values.None:
                                fileFullPath = operationFilename;
                                break;
                            default:
                                throw new Exception("unsupported choice");
//                                throw new Exception("invalid code path");
                        }

                        progressBar.TrailingText = "Verifying checksum...";

                        var verifyChecksumResult = await Program.VerifyChecksumMatchesAsync(fileFullPath, operationChecksum);
                        if (verifyChecksumResult.IsError == true)
                        {
                            Debug.Assert(false, "Could not verify checksum; this may indicate a cryptography library failure");
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Checksum Verification Failed");
                            Console.WriteLine();
                            Console.WriteLine("Checksum could not be verified.");
                            return (int)ExitCode.ChecksumOperationFailed;
                        }
                        var checksumMatches = verifyChecksumResult.Value!;

                        if (checksumMatches == false)
                        {
                            progressBar.Hide();

                            Console.WriteLine("  XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  Checksum Verification Failed");
                            Console.WriteLine();
                            Console.WriteLine("File checksum does not match.");
                            return (int)ExitCode.ChecksumMismatch;
                        }

                        progressBar.Value = ((double)iOperation + 1) / (double)atodOperationCount;
                        progressBar.TrailingText = "Checksum Verified";
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        // clean up any temporary files and folders
        // NOTE: we clean up temporary files before temporary folders because they may be contained within the temporary folders
        foreach (var absolutePathForTemporaryFile in newAbsolutePathsForTemporaryFiles)
        {
            try
            {
                System.IO.File.Delete(absolutePathForTemporaryFile);
            }
            catch (System.IO.FileNotFoundException)
            {
                // NOTE: temporary files might have been cleaned up by the system automatically
                Debug.Assert(false, "Temporary file not found, cannot be deleted");
            }
            catch
            {
                Debug.Assert(false, "Could not delete temporary file");
                // NOTE: we may want to consider asking the operating system to clean up this file later
                Console.WriteLine("WARNING -- Could not delete temporary file: " + absolutePathForTemporaryFile);
            }
        }
        foreach (var absolutePathForTemporaryFolder in newAbsolutePathsForTemporaryFolders)
        {
            try
            {
                System.IO.Directory.Delete(absolutePathForTemporaryFolder, true);
            }
            catch (System.IO.FileNotFoundException)
            {
                Debug.Assert(false, "Temporary folder not found, cannot be deleted");
                Console.WriteLine("Temporary folder could not be found (but was marked for cleanup deletion): " + absolutePathForTemporaryFolder);
            }
            catch
            {
                Debug.Assert(false, "Could not delete temporary folder");
                // NOTE: we may want to consider asking the operating system to clean up this folder later
                Console.WriteLine("WARNING -- Could not delete temporary folder: " + absolutePathForTemporaryFolder);
            }
        }

        Console.WriteLine(); // line-terminate the current progress bar, while leaving it SHOWING
        Console.WriteLine();
        switch (atodSequenceType.Value!)
        {
            case AtodSequenceType.CalculateChecksum:
                {
                    Console.WriteLine("Checksum has been calculated.");
                }
                break;
            case AtodSequenceType.Install:
                Console.WriteLine("Application has been installed.");
                if (rebootRequiredAfterSequence == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("*** REBOOT REQUIRED: the computer needs to be restarted to complete the installation. ***");
                }
                break;
            case AtodSequenceType.Uninstall:
                Console.WriteLine("Application has been uninstalled.");
                if (uninstallOperationSkippedDueToNotInstalled == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("*** Uninstall is complete, but one or more components was not present. ***");
                }
                if (rebootRequiredAfterSequence == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("*** REBOOT REQUIRED: the computer needs to be restarted to complete the uninstallation. ***");
                }
                break;
            default:
                throw new Exception("invalid code path");
        }

        return (int)(exitCode ?? ExitCode.Success);
    }

    private static void WriteBannerToConsole()
    {
        var executingAssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version!;
        var versionString = executingAssemblyVersion.Major.ToString() + "." + executingAssemblyVersion.Minor.ToString();
        // NOTE: manually-compiled (dev/test/custom) builds will typically have a build # of 0
        if (executingAssemblyVersion.Build != 0)
        {
            versionString += " (build " + executingAssemblyVersion.Build.ToString() + ")";
        }
        else
        {
            versionString += " (manual build)";
        }
        //
        var executingAssemblyCopyrightAttribute = Assembly.GetExecutingAssembly().GetCustomAttribute(typeof(AssemblyCopyrightAttribute));
        string? copyrightString = null;
        if (executingAssemblyCopyrightAttribute is not null)
        {
            copyrightString = ((AssemblyCopyrightAttribute)executingAssemblyCopyrightAttribute).Copyright;
        }
        //
        Console.WriteLine("AT on Demand v" + versionString);
        if (copyrightString is not null)
        {
            Console.WriteLine(copyrightString);
        }
        //
        Console.WriteLine();
    }

    //

    private static void WriteGeneralUsageToConsole()
    {
        Console.WriteLine("'atod' command-line utility installs and uninstalls AT software.");
        Console.WriteLine();
        Console.WriteLine("usage: atod <command> [arguments]");
        Console.WriteLine();
        Console.WriteLine("The following commands are available:");
#if DEBUG
        Console.WriteLine("  checksum    Calculate the checksum for a file.");
#endif
        Console.WriteLine("  install     Install an application.");
        //Console.WriteLine("  settings   Configure settings for an installed application (requires account).");
        Console.WriteLine("  uninstall   Uninstall an application.");
        Console.WriteLine();
    }

    private static void WriteCalculateChecksumUsageToConsole()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  atod checksum [ALGORITHM] <PATH>");
        Console.WriteLine();
        Console.WriteLine("Required arguments:");
        Console.WriteLine("  <PATH>              The full path to the file.");
        Console.WriteLine();
        Console.WriteLine("Optional arguments:");
        Console.WriteLine("  [ALGORITHM]         The checksum algorithm (default is sha256)");
        Console.WriteLine();
    }

    private static void WriteInstallUsageToConsole()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  atod install <APPLICATION_NAME>");
#if DEBUG
        Console.WriteLine("  atod install <PATH>");
#endif
        Console.WriteLine();
        Console.WriteLine("Required arguments:");
        Console.WriteLine("  <APPLICATION_NAME>  The name of the application to install.");
#if DEBUG
        Console.WriteLine("  <PATH>              The full path to the application installer, including filename.");
#endif
        Console.WriteLine();
    }

    private static void WriteUninstallUsageToConsole()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  atod uninstall <APPLICATION_NAME>");
        Console.WriteLine();
        Console.WriteLine("Required arguments:");
        Console.WriteLine("  <APPLICATION_NAME>  The name of the application to uninstall.");
        Console.WriteLine();
    }

    private static async Task<MorphicResult<IAtodChecksum, MorphicUnit>> CalculateChecksumAsync(string path, AtodChecksumAlgorithm checksumAlgorithm)
    {
        System.IO.FileStream fileStream;
        try
        {
            fileStream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
        }
        catch
        {
            return MorphicResult.ErrorResult();
        }
        //
        try
        {
            try
            {
                var sha256 = System.Security.Cryptography.SHA256.Create();
                var checksum = await sha256.ComputeHashAsync(fileStream);
                return MorphicResult.OkResult<IAtodChecksum>(new IAtodChecksum.Sha256(checksum));
            }
            catch
            {
                return MorphicResult.ErrorResult();
            }
        }
        finally
        {
            fileStream.Close();
        }
    }

    private static async Task<MorphicResult<bool, MorphicUnit>> VerifyChecksumMatchesAsync(string path, IAtodChecksum checksum)
    {
        var checksumAlgorithm = checksum.GetAlgorithm();

        var calculateChecksumResult = await Program.CalculateChecksumAsync(path, checksumAlgorithm);
        if (calculateChecksumResult.IsError)
        {
            return MorphicResult.ErrorResult();
        }
        var verifyChecksum = calculateChecksumResult.Value!;

        var result = checksum.ChecksumEqual(verifyChecksum);
        return MorphicResult.OkResult(result);
    }

    private static MorphicResult<(string Filename, string Args), MorphicUnit> SplitUninstallStringIntoFilenameAndArgs(string uninstallString)
    {
        // in our initial implementation, we simply separate out the args after the first space character; if the filename is enclosed in double-quotes then we terminate it at its closing quotation marks
        
        var mutableUninstallString = uninstallString.TrimStart();
        if (mutableUninstallString.Length == 0)
        {
            return MorphicResult.OkResult(("", ""));
        }

        string filename;
        string args;

        if (mutableUninstallString[0] == '"')
        {
            // filename is enclosed in double quotes
            var indexOfClosingQuote = mutableUninstallString.IndexOf('"', 1);
            if (indexOfClosingQuote >= 0)
            {
                filename = mutableUninstallString.Substring(1, indexOfClosingQuote - 1);
                args = mutableUninstallString.Substring(indexOfClosingQuote + 1);
            }
            else
            {
                // opening double-quote did not have a closing double-quote to finish the pair; cmd.exe seems to allow this (and just ignores the missing closing quote), so we will too
                filename = mutableUninstallString.Substring(1);
                args = "";
            }
        }
        else
        {
            var indexOfBackslash = mutableUninstallString.IndexOf('\\');
            var indexOfSpace = mutableUninstallString.IndexOf(' ');
            if (indexOfBackslash >= 0)
            {
                // NOTE: if the filename has backslashes in it but is not enclosed in double-quotes, then we assume the full string is the filename; if this causes us problems, then we could evaluate a strategy such as manually parsing the path and looking for folders and files with its name
                filename = mutableUninstallString;
                args = "";
            }
            else if (indexOfSpace < 0)
            {
                // no spaces; this must be a filename
                filename = mutableUninstallString;
                args = "";
            }
            else
            {
                // spaces in string; separate the filename and args by the index of that space
                filename = mutableUninstallString.Substring(0, indexOfSpace);
                args = mutableUninstallString.Substring(indexOfSpace + 1);
            }
        }

        // trim any left-most whitespace from args
        args = args.TrimStart();

        var result = (filename, args);
        return MorphicResult.OkResult(result);
    }

    private static string AddSupplementalArgs(string args, string[] supplementalArgs)
    {
        var result = args;
        
        // split our current args into a list
        var existingArgs = Program.SplitArgsStringIntoList(args);

        foreach (var supplementalArg in supplementalArgs)
        {
            // NOTE: we are doing a case-sensitive compare
            if (args.Contains(supplementalArg) == false)
            {
                result += " " + supplementalArg;
            }
        }

        result = result.TrimStart();
        return result;
    }

    // see: https://web.archive.org/web/20171214211548/http://www.microsoft.com/resources/documentation/windows/xp/all/proddocs/en-us/ntcmds_shelloverview.mspx?mfr=true
    private static List<string> SplitArgsStringIntoList(string args)
    {
        // NOTE: arguments must be contained in quotes if they contain &, |, (, ) or ^ -- or they can be prefixed by the escape character '^'
        // NOTE: arguments must be contained in quotes if they contain spaces

        var result = new List<string>();

        int? startIndexOfQuotedArg = null;
        StringBuilder currentArg = new();
        for (var index = 0; index < args.Length; index += 1)
        {
            if (args[index] == '^')
            {
                if (args.Length > index + 1)
                {
                    // carat is an escape character; capture the FOLLOWING character and then skip both characters
                    currentArg.Append(args[index + 1]);
                    index += 1;
                    continue;
                }
                else
                {
                    // carat is at end of sequence; gracefully fail by simply exiting and ignoring the trailing carat
                    index += 1;
                    continue;
                }
            }

            if (args[index] == '"')
            {
                if (startIndexOfQuotedArg is null)
                {
                    // start of quoted argument
                    startIndexOfQuotedArg = index;
                    continue;
                }
                else
                {
                    // end of quoted argument
                    result.Add(currentArg.ToString());
                    currentArg = new();

                    startIndexOfQuotedArg = null;
                    continue;
                }
            }

            if (args[index] == ' ')
            {
                if (currentArg.Length > 0)
                {
                    // end of current arg; do not capture space character
                    result.Add(currentArg.ToString());
                    currentArg = new();

                    continue;
                }
                else
                {
                    // extra whitespace in between args; ignore
                    continue;
                }
            }

            // otherwise, just capture the character
            currentArg.Append(args[index]);
        }

        if (startIndexOfQuotedArg is not null)
        {
            // if a quoted argument is missing its closing double quote, gracefully fail by capturing the quoted argument and assuming an implied final double-quote
            result.Add(currentArg.ToString());
            currentArg = new();

            startIndexOfQuotedArg = null;
        }

        return result;
    }

    private static MorphicResult<bool, MorphicUnit> EvaluateShouldSkipOperation(List<IAtodOperationCondition> conditions)
    {
        foreach (var condition in conditions)
        {
            switch (condition) {
                case IAtodOperationCondition.SkipOperationIfRegistryValueIsNonZeroVersion { RootKey: Microsoft.Win32.RegistryKey rootKey, SubKeyName: string subKeyName, ValueName: string valueName }:
                    {
                        Microsoft.Win32.RegistryKey? registrySubKey;
                        try
                        {
                            registrySubKey = rootKey.OpenSubKey(subKeyName, false);
                        }
                        catch
                        {
                            return MorphicResult.ErrorResult();
                        }
                        if (registrySubKey is null)
                        {
                            // if the key does not exist, then we should not skip the operation
                            continue;
                        }

                        object? valueAsNullableObject;
                        try
                        {
                            valueAsNullableObject = registrySubKey!.GetValue(valueName);
                        }
                        catch
                        {
                            return MorphicResult.ErrorResult();
                        }
                        if (valueAsNullableObject is null)
                        {
                            // if the value does not exist, then we should not skip the operation
                            continue;
                        }

                        if (registrySubKey!.GetValueKind(valueName) != Microsoft.Win32.RegistryValueKind.String)
                        {
                            return MorphicResult.ErrorResult();
                        }

                        var valueAsString = (string?)valueAsNullableObject!;
                        if (String.IsNullOrEmpty(valueAsString) == true || valueAsString == "0.0.0.0")
                        {
                            // if the value is an empty string or "0.0.0.0", then we should not skip the operation
                            continue;
                        }

                        // if we cannot parse the version, return an error
                        // NOTE: technically we do not need to parse the version to return "skip", as just the presence of a non-null non-empty non-zero version string is in theory a non-zero version
                        // see: https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution#installing-the-runtime-as-per-machine-or-per-user
                        var parseSuccess = Version.TryParse(valueAsString, out var valueAsVersion);
                        if (parseSuccess == false || valueAsVersion is null)
                        {
                            Debug.Assert(valueAsVersion is not null, "'valueAsVersion' should never be null here, as we already checked for a null value and also for an empty string");
                            return MorphicResult.ErrorResult();
                        }

                        // if we reach here, the version is non-zero; skip this AToD operation
                        return MorphicResult.OkResult(true);
                    }
                default:
                    // this function doesn't process not-skip conditions
                    break;
            }
        }

        // if no operations indicated that we should skip the operation, return false
        return MorphicResult.OkResult(false);
    }
}