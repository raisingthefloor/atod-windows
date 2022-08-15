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
        var commandArg = commandLineArgs[1]!;
        switch (commandArg.ToUpperInvariant())
        {
            case "INSTALL":
                {
                    var showInstallCommandHelp = false;

                    string? applicationName;
                    if (commandLineArgs.Length > 2)
                    {
                        applicationName = commandLineArgs[2];

                        string? fullPath = null;
                        if (commandLineArgs.Length > 3)
                        {
                            fullPath = commandLineArgs[3];
                        }
                        else
                        {
                            // TODO: if not provided, search for the path to the installer (and remove this block of code)
                            Console.WriteLine("Sorry, path is required in the current implementation; automatic download will be added in a future release");
                            Console.WriteLine();
                            //
                            showInstallCommandHelp = true;
                        }

                        atodOperationAsNullable = AtodOperation.Install(applicationName, fullPath);
                    }
                    else
                    {
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
        showGeneralHelp = true;
    }
}

if (atodOperationAsNullable is null || showGeneralHelp == true)
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  atod <command> [arguments]");
    Console.WriteLine();
    Console.WriteLine("Commands:");
    Console.WriteLine("  install        Install an application.");
    Console.WriteLine();
    return;
}

// NOTE: at this point, we know we have a valid operation
var atodOperation = atodOperationAsNullable!;

// execute the operation
//
switch (atodOperation.Value)
{
    case AtodOperation.Values.Install:
        {
            Console.WriteLine("TODO: trigger installation...");
        }
        break;
    default:
        throw new NotImplementedException();
}
return;