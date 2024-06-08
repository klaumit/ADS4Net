using Microsoft.VisualStudio.Data.Framework;
using Microsoft.VisualStudio.Data.Framework.AdoDotNet;
using Microsoft.VisualStudio.Data.Services.SupportEntities;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal class AdsSourceInformation : AdoDotNetSourceInformation
    {
        public AdsSourceInformation() => this.Initialize();

        private void Initialize()
        {
            this.AddProperty("CatalogMaxLength", (object)(int)byte.MaxValue);
            this.AddProperty("CatalogSeparator", (object)".");
            this.AddProperty("ColumnAliasMaxLength", (object)128);
            this.AddProperty("ColumnMaxLength", (object)128);
            this.AddProperty("ColumnSupported", (object)false);
            this.AddProperty("CatalogSupportedInDml", (object)false);
            this.AddProperty("SchemaSupportedInDml", (object)true);
            this.AddProperty("DefaultSchema", (object)"::this");
            this.AddProperty("IdentifierOpenQuote", (object)"[");
            this.AddProperty("IdentifierCloseQuote", (object)"]");
            this.AddProperty("IndexSupported", (object)false);
            this.AddProperty("SupportsAnsi92Sql", (object)true);
            this.AddProperty("SupportsCommandTimeout", (object)false);
            this.AddProperty("SupportsNestedTransactions", (object)true);
            this.AddProperty("CommandDeriveParametersSupport", (object)"");
            this.AddProperty("CommandParameterSupport", (object)(DataParameterDirection)3);
            this.AddProperty("SupportsQuotedIdentifierParts", (object)true);
            this.AddProperty("TableSupported", (object)true);
            this.AddProperty("ParameterPrefix", (object)":");
            this.AddProperty("ParameterPrefixInName", (object)true);
            this.AddProperty("ServerSeparator", (object)".");
            this.AddProperty("CatalogSupported", (object)true);
            this.AddProperty("SchemaSupported", (object)true);
            this.AddProperty("SchemaSeparator", (object)".");
            this.AddProperty("ProcedureSupported", (object)true);
            this.AddProperty("CommandExecuteSupport", (object)"1,3,4");
            this.AddProperty("CommandPrepareSupport", (object)"1,4");
        }

        protected virtual object RetrieveValue(string propertyName) => base.RetrieveValue(propertyName);
    }
}