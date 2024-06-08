using Microsoft.VisualStudio.Data.Framework.AdoDotNet;
using System;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal class AdsMappedObjectConverter : AdoDotNetMappedObjectConverter
    {
        protected virtual object ConvertToMappedMember(
            string typeName,
            string mappedMemberName,
            object[] underlyingValues,
            object[] parameters)
        {
            if (typeName == null)
                throw new ArgumentNullException(nameof(typeName));
            if (mappedMemberName == null)
                throw new ArgumentNullException(nameof(mappedMemberName));
            if (parameters != null)
                throw new ArgumentException("This mapped object converter does not accept parameters.",
                    nameof(parameters));
            return base.ConvertToMappedMember(typeName, mappedMemberName, underlyingValues, parameters);
        }
    }
}