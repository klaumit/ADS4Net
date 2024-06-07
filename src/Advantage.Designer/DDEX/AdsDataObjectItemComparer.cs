using System;
using System.Globalization;
using Microsoft.VisualStudio.Data;

namespace Advantage.Data.DDEX
{
    internal class AdsDataObjectItemComparer : DataObjectItemComparer
    {
        private DataConnection _connection;

        public AdsDataObjectItemComparer(DataConnection connection) => _connection = connection;

        private string RemoveBrackets(string strValue) => strValue;

        public virtual int Compare(
            string typeName,
            object[] identifier,
            int identifierPart,
            object value)
        {
            if (typeName == null)
                throw new ArgumentNullException(nameof(typeName));
            var strB = RemoveBrackets(value as string);
            return string.Compare(RemoveBrackets(identifier[identifierPart] as string), strB, true,
                CultureInfo.CurrentCulture);
        }
    }
}