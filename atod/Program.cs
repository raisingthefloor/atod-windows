// Copyright 2022 Raising the Floor - US, Inc.
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

using atod;
using atod.UI;
using System;
using System.Reflection;

// print out program header
//
var executingAssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version!;
var versionString = executingAssemblyVersion.Major.ToString() + "." + executingAssemblyVersion.Minor.ToString() + " (build " + executingAssemblyVersion.Build.ToString() + ")";
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

var showGeneralHelp = false;

AtodOperation? atodOperationAsNullable = null;

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
            case "INSTALL":
                {
                    var showInstallCommandHelp = false;

                    string? applicationName;
                    if (commandLineArgs.Length > 2)
                    {
                        // capture the application name					
                        applicationName = commandLineArgs[2];

                        string? fullPath = null;
                        if (commandLineArgs.Length > 3)
                        {
                            // if (optionally) provided, capture the full path to the installer
                            fullPath = commandLineArgs[3];

                            atodOperationAsNullable = AtodOperation.Install(applicationName, fullPath);
                        }
                        else
                        {
                            // TODO: if the full path was not specified, search for the path to the installer
                            Console.WriteLine("Sorry, path is required in the current implementation; automatic download will be added in a future release");
                            Console.WriteLine();
                            //
                            showInstallCommandHelp = true;

                            // TODO: if we could successfully find the installer's full path, populate the atodOperationAsNullable here
                            //atodOperationAsNullable = AtodOperation.Install(applicationName, fullPath);
                        }
                    }
                    else
                    {
                        // the application name was not provided; the command is incomplete					
                        Console.WriteLine("Application name was not provided.");
                        Console.WriteLine();
                        //
                        showInstallCommandHelp = true;
                    }

                    if (showInstallCommandHelp == true)
                    {
                        // missing application name
                        Console.WriteLine("Usage:");
                        Console.WriteLine("  atod install <APPLICATION_NAME> [path]");
                        Console.WriteLine();
                        Console.WriteLine("Required arguments:");
                        Console.WriteLine("  <APPLICATION_NAME>  The name of the application to install.");
                        Console.WriteLine();
                        Console.WriteLine("Optional arguments:");
                        Console.WriteLine("  [path]              The full path to the application installer.");
                        Console.WriteLine();
                        return;
                    }
                }
                break;
            case "HELP":
                {
                    showGeneralHelp = true;
                }
                break;
            default:
                {
                    // invalid command
                    Console.WriteLine("Unknown command: " + commandArg);
                    Console.WriteLine();
                }
                //
                showGeneralHelp = true;
                break;
        }
    }
    else
    {
        // the command argument was missing (i.e. no parameters were passed to our application)
        showGeneralHelp = true;
    }
}

if (atodOperationAsNullable is null || showGeneralHelp == true)
{
    Console.WriteLine("'atod' command-line utility installs/uninstalls applications.");
    Console.WriteLine();
    Console.WriteLine("usage: atod <command> [arguments]");
    Console.WriteLine();
    Console.WriteLine("The following commands are available:");
    Console.WriteLine("  install    Install an application.");
    //Console.WriteLine("  settings   Configure settings for an installed application (requires account).");
	// TODO: we need to determine if we automatically roll-back settings when an application is uninstalled (and if we should have a "revert settings" type of command)
    //Console.WriteLine("  uninstall  Uninstall an application.");
    Console.WriteLine();
    return;
}

// NOTE: at this point, we know we have a valid operation
var atodOperation = atodOperationAsNullable!;

return;