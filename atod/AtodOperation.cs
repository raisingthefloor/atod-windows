using Morphic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace atod;

public record AtodOperation : MorphicAssociatedValueEnum<AtodOperation.Values>
{
    // enum members
    public enum Values
    {
        Install,
    }

    // functions to create member instances
    public static AtodOperation Install(string applicationName, string? fullPath) => new AtodOperation(Values.Install) { ApplicationName = applicationName, FullPath = fullPath };

    // associated values
    public string? ApplicationName { get; private set; }
    public string? FullPath { get; private set; }

    // verbatim required constructor implementation for MorphicAssociatedValueEnums
    private AtodOperation(Values value) : base(value) { }
}


