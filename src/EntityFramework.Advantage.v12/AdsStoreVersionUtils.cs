using System;

namespace Advantage.Data.Provider
{
    internal static class AdsStoreVersionUtils
    {
        internal static AdsStoreVersion GetStoreVersion(AdsConnection connection)
        {
            if (connection.ServerVersion.StartsWith("9.", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage9;
            if (connection.ServerVersion.StartsWith("10.", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage10;
            if (connection.ServerVersion.StartsWith("11.", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage11;
            if (connection.ServerVersion.StartsWith("12.", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage12;
            throw new ArgumentException("Unsupported version.");
        }

        internal static AdsStoreVersion GetStoreVersion(string providerManifestToken)
        {
            if (providerManifestToken.StartsWith("9", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage9;
            if (providerManifestToken.StartsWith("10", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage10;
            if (providerManifestToken.StartsWith("11", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage11;
            if (providerManifestToken.StartsWith("12", StringComparison.Ordinal))
                return AdsStoreVersion.Advantage12;
            throw new ArgumentException("Unsupported version.");
        }

        internal static string GetVersionHint(AdsStoreVersion version)
        {
            switch (version)
            {
                case AdsStoreVersion.Advantage9:
                    return "9";
                case AdsStoreVersion.Advantage10:
                    return "10";
                case AdsStoreVersion.Advantage11:
                    return "11";
                case AdsStoreVersion.Advantage12:
                    return "12";
                default:
                    throw new ArgumentException(
                        "Could not determine storage version; a valid storage connection or a version hint is required.");
            }
        }
    }
}