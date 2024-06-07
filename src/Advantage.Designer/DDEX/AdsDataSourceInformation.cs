using Microsoft.VisualStudio.Data;
using Microsoft.VisualStudio.Data.AdoDotNet;

namespace Advantage.Data.DDEX
{
    internal class AdsDataSourceInformation : AdoDotNetDataSourceInformation
    {
        public AdsDataSourceInformation(DataConnection connection)
            : base(connection)
        {
            AddProperty("SupportsAnsi92Sql", true);
            AddProperty("SupportsQuotedIdentifierParts", true);
            AddProperty("IdentifierOpenQuote", "[");
            AddProperty("IdentifierCloseQuote", "]");
            AddProperty("ServerSeparator", ".");
            AddProperty("CatalogSupported", true);
            AddProperty("CatalogSupportedInDml", false);
            AddProperty("SchemaSupported", true);
            AddProperty("SchemaSupportedInDml", true);
            AddProperty("SchemaSeparator", ".");
            AddProperty("CatalogSeparator", ".");
            AddProperty("ParameterPrefix", ":");
            AddProperty("ParameterPrefixInName", true);
            AddProperty("DefaultSchema", "::this");
            AddProperty("TableSupported", true);
            AddProperty("ProcedureSupported", true);
            AddProperty("CommandExecuteSupport", "1,3,4");
            AddProperty("CommandPrepareSupport", "1,4");
        }

        protected virtual object RetrieveValue(string propertyName) => base.RetrieveValue(propertyName);
    }
}