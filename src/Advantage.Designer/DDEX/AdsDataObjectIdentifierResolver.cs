using System;
using System.Text;
using Microsoft.VisualStudio.Data;
using Microsoft.VisualStudio.OLE.Interop;

namespace Advantage.Data.DDEX
{
    internal class AdsDataObjectIdentifierResolver : DataObjectIdentifierResolver, IObjectWithSite
    {
        private DataConnection _connection;

        public AdsDataObjectIdentifierResolver()
        {
        }

        public AdsDataObjectIdentifierResolver(DataConnection connection)
        {
            _connection = connection;
        }

        internal static int GetIdentifierLength(string typeName)
        {
            switch (typeName)
            {
                case "":
                    return 0;
                case "User":
                    return 1;
                case "Table":
                case "View":
                    return 3;
                case "StoredProcedure":
                case "Function":
                    return 3;
                case "Column":
                case "Index":
                case "ForeignKey":
                case "ViewColumn":
                case "StoredProcedureParameter":
                case "StoredProcedureColumn":
                case "FunctionParameter":
                case "FunctionColumn":
                    return 4;
                case "IndexColumn":
                case "ForeignKeyColumn":
                    return 5;
                default:
                    throw new NotSupportedException();
            }
        }

        protected virtual object[] QuickExpandIdentifier(string typeName, object[] partialIdentifier)
        {
            var length = typeName != null
                ? GetIdentifierLength(typeName)
                : throw new ArgumentNullException(nameof(typeName));
            var objArray = new object[length];
            if (partialIdentifier != null)
            {
                if (partialIdentifier.Length > length)
                    throw new InvalidOperationException();
                partialIdentifier.CopyTo(objArray, length - partialIdentifier.Length);
            }

            if (length > 0 && !(objArray[0] is string))
                objArray[0] = _connection.SourceInformation["DefaultCatalog"] as string;
            if (length > 1 && !(objArray[1] is string))
                objArray[1] = "::this";
            return objArray;
        }

        protected virtual object[] QuickContractIdentifier(string typeName, object[] fullIdentifier)
        {
            switch (typeName)
            {
                case null:
                    throw new ArgumentNullException(nameof(typeName));
                case "":
                case "User":
                    return base.QuickContractIdentifier(typeName, fullIdentifier);
                default:
                    var identifierLength = GetIdentifierLength(typeName);
                    var objArray = identifierLength != -1
                        ? new object[identifierLength]
                        : throw new NotSupportedException();
                    fullIdentifier?.CopyTo(objArray, identifierLength - fullIdentifier.Length);
                    if (objArray.Length > 0 && objArray[0] != null)
                    {
                        var str = _connection.SourceInformation["DefaultCatalog"] as string;
                        if (_connection.ObjectItemComparer.Compare("", objArray, 0, str) == 0)
                            objArray[0] = null;
                    }

                    if (objArray.Length > 1 && objArray[1] != null && (string)objArray[1] == "::this")
                        objArray[1] = null;
                    return objArray;
            }
        }

        private string ArrayToString(object[] identifier)
        {
            var stringBuilder = new StringBuilder("[");
            for (var index = 0; index < identifier.Length; ++index)
            {
                if (index > 0)
                    stringBuilder.Append(", ");
                if (identifier[index] == null)
                    stringBuilder.Append("null");
                else
                    stringBuilder.Append(identifier[index]);
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        void IObjectWithSite.GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            ppvSite = IntPtr.Zero;
            throw new NotImplementedException();
        }

        void IObjectWithSite.SetSite(object pUnkSite) => _connection = (DataConnection)pUnkSite;
    }
}