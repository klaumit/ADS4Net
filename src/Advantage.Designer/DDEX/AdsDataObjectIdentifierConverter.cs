using Microsoft.VisualStudio.Data;
using Microsoft.VisualStudio.Data.AdoDotNet;

namespace Advantage.Data.DDEX
{
    internal class AdsDataObjectIdentifierConverter : AdoDotNetObjectIdentifierConverter
    {
        private DataConnection _connection;

        public AdsDataObjectIdentifierConverter(DataConnection connection)
            : base(connection)
        {
            _connection = connection;
        }

        protected virtual string[] SplitIntoParts(string typeName, string identifier)
        {
            return base.SplitIntoParts(typeName, identifier);
        }

        protected virtual object UnformatPart(string typeName, string identifierPart)
        {
            return base.UnformatPart(typeName, identifierPart);
        }

        protected virtual string FormatPart(string typeName, object identifierPart, bool withQuotes)
        {
            return base.FormatPart(typeName, identifierPart, withQuotes);
        }
    }
}