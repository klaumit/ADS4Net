using Microsoft.VisualStudio.Data.Framework;
using System;
using System.Globalization;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal class AdsDataObjectItemComparer : DataObjectMemberComparer
    {
        public virtual int Compare(
            string typeName,
            object[] identifier,
            int identifierPart,
            object value)
        {
            if (typeName == null)
                throw new ArgumentNullException(nameof(typeName));
            string strB = value as string;
            string strC = "..."; // TODO
            return typeName == "" || typeName == "User"
                ? string.Compare(identifier[identifierPart] as string, strB, true, CultureInfo.CurrentCulture)
                : (identifierPart != 2
                    ? string.Compare(identifier[identifierPart] as string, strB, true, CultureInfo.CurrentCulture)
                    : (!(identifier[identifierPart] is string strA) || strB == null
                        ? (strC != null || strB != null
                            ? (strC == null || strB != null
                                ? (string.Compare("::this", strB, true, CultureInfo.CurrentCulture) != 0
                                    ? string.Compare(strC, strB, true, CultureInfo.CurrentCulture)
                                    : 0)
                                : (string.Compare(strC, "::this", true, CultureInfo.CurrentCulture) != 0
                                    ? string.Compare(strC, strB, true, CultureInfo.CurrentCulture)
                                    : 0))
                            : 0)
                        : string.Compare(strA, strB, true, CultureInfo.CurrentCulture)));
        }
    }
}