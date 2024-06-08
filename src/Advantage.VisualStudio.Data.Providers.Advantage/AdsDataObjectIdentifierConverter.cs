using Microsoft.VisualStudio.Data.Framework.AdoDotNet;
using Microsoft.VisualStudio.Data.Services;
using System;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal class AdsDataObjectIdentifierConverter : AdoDotNetObjectIdentifierConverter
    {
        protected virtual string BuildString(
            string typeName,
            string[] identifierParts,
            DataObjectIdentifierFormat format)
        {
            return base.BuildString(typeName, identifierParts, format);
        }

        protected virtual string[] SplitIntoParts(string typeName, string identifier)
        {
            return base.SplitIntoParts(typeName, identifier);
        }

        protected virtual object UnformatPart(string typeName, string identifierPart)
        {
            return identifierPart == null ? (object)null : base.UnformatPart(typeName, identifierPart);
        }

        protected virtual string FormatPart(
            string typeName,
            object identifierPart,
            DataObjectIdentifierFormat format)
        {
            switch (identifierPart)
            {
                case null:
                case DBNull _:
                    return (string)null;
                default:
                    return base.FormatPart(typeName, identifierPart, format);
            }
        }
    }
}