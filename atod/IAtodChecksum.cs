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

using System;
using System.Linq;

namespace Atod;

public enum AtodChecksumAlgorithm
{
    Sha256,
}

public static class AtodChecksumAlgorithmFactory
{
    public static AtodChecksumAlgorithm From(string checksumAlgorithmAsString)
    {
        var result = checksumAlgorithmAsString.ToLowerInvariant() switch
        {
            "sha256" => AtodChecksumAlgorithm.Sha256,
            _ => throw new ArgumentOutOfRangeException(),
        };
        return result;
    }

    public static AtodChecksumAlgorithm? TryFrom(string checksumAlgorithmAsString)
    {
        AtodChecksumAlgorithm? result = checksumAlgorithmAsString.ToLowerInvariant() switch
        {
            "sha256" => AtodChecksumAlgorithm.Sha256,
            _ => null,
        };
        return result;
    }
}

public static class AtodChecksumAlgorithmExtensions
{
    public static string ToString(this AtodChecksumAlgorithm checksumAlgorithm)
    {
        var result = checksumAlgorithm switch
        {
            AtodChecksumAlgorithm.Sha256 => "sha256",
            _ => throw new Exception("unknown algorithm"),
        };
        return result;
    }
}

public interface IAtodChecksum
{
    public record Sha256(byte[] Checksum) : IAtodChecksum;

    public AtodChecksumAlgorithm GetAlgorithm()
    {
        var result = this switch
        {
            Sha256 { Checksum: _ } => AtodChecksumAlgorithm.Sha256,
            _ => throw new Exception("invalid code path"),
        };
        return result;
    }

    public bool ChecksumEqual(IAtodChecksum rhs)
    {
        if (this.GetAlgorithm() != rhs.GetAlgorithm())
        {
            return false;
        }

        switch (this)
        {
            case Sha256 { Checksum: var checksum }:
                switch (rhs)
                {
                    case Sha256 { Checksum: var rhsChecksum }:
                        return checksum.SequenceEqual(rhsChecksum);
                    default:
                        throw new Exception("invalid code path");
                }
            default:
                throw new Exception("unknown algorithm; invalid code path");
        }
    }
}


