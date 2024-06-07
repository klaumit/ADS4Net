using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;
using System.Xml;

namespace Advantage.Data.Provider
{
    internal class AdsProviderManifest : DbXmlEnabledProviderManifest
    {
        internal const string TokenAdvantage9 = "9";
        internal const string TokenAdvantage10 = "10";
        internal const string TokenAdvantage11 = "11";
        internal const string TokenAdvantage12 = "12";
        internal const string ProviderInvariantName = "Advantage.Data.Provider";
        private const int BinaryMaxSize = 65530;
        private const int Nvarchar2MaxSize = 2000;
        private const int Varchar2MaxSize = 4000;
        private string _token = "9";
        private AdsStoreVersion _version;
        private ReadOnlyCollection<PrimitiveType> _primitiveTypes;
        private ReadOnlyCollection<EdmFunction> _functions;

        public AdsProviderManifest(string manifestToken)
            : base(GetProviderManifest())
        {
            _version = AdsStoreVersionUtils.GetStoreVersion(manifestToken);
            _token = manifestToken;
        }

        internal string Token => _token;

        private static XmlReader GetProviderManifest()
        {
            return GetXmlResource(
                "Advantage.Data.Provider.Resources.EFAdvantageProviderManifest.xml");
        }

        protected override XmlReader GetDbInformation(string informationType)
        {
            switch (informationType)
            {
                case "StoreSchemaDefinition":
                    return GetStoreSchemaDescription();
                case "StoreSchemaMapping":
                    return GetStoreSchemaMapping();
                case "ConceptualSchemaDefinition":
                    return null;
                default:
                    throw new ProviderIncompatibleException(string.Format(
                        "The provider returned null for the informationType '{0}'.", informationType));
            }
        }

        public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
        {
            if (_primitiveTypes == null && (_version == AdsStoreVersion.Advantage9 ||
                                                 _version == AdsStoreVersion.Advantage10 ||
                                                 _version == AdsStoreVersion.Advantage11 ||
                                                 _version == AdsStoreVersion.Advantage12))
                _primitiveTypes = base.GetStoreTypes();
            return _primitiveTypes;
        }

        public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
        {
            if (_functions == null && (_version == AdsStoreVersion.Advantage9 ||
                                            _version == AdsStoreVersion.Advantage10 ||
                                            _version == AdsStoreVersion.Advantage11 ||
                                            _version == AdsStoreVersion.Advantage12))
                _functions = base.GetStoreFunctions();
            return _functions;
        }

        public override TypeUsage GetEdmType(TypeUsage storeType)
        {
            EntityUtils.CheckArgumentNull(storeType, nameof(storeType));
            var lowerInvariant = storeType.EdmType.Name.ToLowerInvariant();
            var primitiveType = StoreTypeNameToEdmPrimitiveType.ContainsKey(lowerInvariant)
                ? StoreTypeNameToEdmPrimitiveType[lowerInvariant]
                : throw new ArgumentException(string.Format("The underlying provider does not support the type '{0}'.",
                    lowerInvariant));
            var maxLength = 0;
            var flag1 = true;
            PrimitiveTypeKind primitiveTypeKind1;
            bool flag2;
            bool flag3;
            switch (lowerInvariant)
            {
                case "longinteger":
                case "integer":
                case "short":
                case "logical":
                case "autoinc":
                case "double":
                case "curdouble":
                case "time":
                    return TypeUsage.CreateDefaultTypeUsage(primitiveType);
                case "date":
                    return TypeUsage.CreateDateTimeTypeUsage(PrimitiveType.GetEdmPrimitiveType((PrimitiveTypeKind)3),
                        10);
                case "rowversion":
                    return TypeUsage.CreateDefaultTypeUsage(primitiveType);
                case "numeric":
                    byte precision1;
                    byte scale;
                    return MetadataHelpers.TryGetPrecision(storeType, out precision1) &&
                           MetadataHelpers.TryGetScale(storeType, out scale)
                        ? TypeUsage.CreateDecimalTypeUsage(primitiveType, precision1, scale)
                        : TypeUsage.CreateDecimalTypeUsage(primitiveType);
                case "money":
                    return TypeUsage.CreateDecimalTypeUsage(primitiveType, 19, 4);
                case "memo":
                    primitiveTypeKind1 = (PrimitiveTypeKind)12;
                    flag2 = true;
                    flag1 = false;
                    flag3 = false;
                    break;
                case "nmemo":
                    primitiveTypeKind1 = (PrimitiveTypeKind)12;
                    flag2 = true;
                    flag1 = true;
                    flag3 = false;
                    break;
                case "varbinary":
                case "varbinaryfox":
                    primitiveTypeKind1 = 0;
                    flag2 = !MetadataHelpers.TryGetMaxLength(storeType, out maxLength);
                    flag3 = false;
                    break;
                case "varcharfox":
                case "varchar":
                    primitiveTypeKind1 = (PrimitiveTypeKind)12;
                    flag2 = !MetadataHelpers.TryGetMaxLength(storeType, out maxLength);
                    flag1 = false;
                    flag3 = false;
                    break;
                case "char":
                case "cichar":
                    primitiveTypeKind1 = (PrimitiveTypeKind)12;
                    flag2 = !MetadataHelpers.TryGetMaxLength(storeType, out maxLength);
                    flag1 = false;
                    flag3 = true;
                    break;
                case "nvarchar":
                    primitiveTypeKind1 = (PrimitiveTypeKind)12;
                    flag2 = !MetadataHelpers.TryGetMaxLength(storeType, out maxLength);
                    flag1 = true;
                    flag3 = false;
                    break;
                case "nchar":
                    primitiveTypeKind1 = (PrimitiveTypeKind)12;
                    flag2 = !MetadataHelpers.TryGetMaxLength(storeType, out maxLength);
                    flag1 = true;
                    flag3 = true;
                    break;
                case "blob":
                    primitiveTypeKind1 = 0;
                    flag2 = true;
                    flag3 = false;
                    break;
                case "raw":
                    primitiveTypeKind1 = 0;
                    flag2 = !MetadataHelpers.TryGetMaxLength(storeType, out maxLength);
                    flag3 = true;
                    break;
                case "guid":
                    return TypeUsage.CreateDefaultTypeUsage(primitiveType);
                case "timestamp":
                case "modtime":
                    byte precision2;
                    return MetadataHelpers.TryGetPrecision(storeType, out precision2)
                        ? TypeUsage.CreateDateTimeTypeUsage(PrimitiveType.GetEdmPrimitiveType((PrimitiveTypeKind)3),
                            precision2)
                        : TypeUsage.CreateDateTimeTypeUsage(PrimitiveType.GetEdmPrimitiveType((PrimitiveTypeKind)3),
                            new byte?());
                default:
                    throw new NotSupportedException(string.Format(
                        "The underlying provider does not support the type '{0}'.", lowerInvariant));
            }

            var primitiveTypeKind2 = primitiveTypeKind1;
            if (primitiveTypeKind2 != 0)
            {
                if (primitiveTypeKind2 != (PrimitiveTypeKind)12)
                    throw new NotSupportedException(string.Format(
                        "The underlying provider does not support the type '{0}'.", lowerInvariant));
                return !flag2
                    ? TypeUsage.CreateStringTypeUsage(primitiveType, flag1, flag3, maxLength)
                    : TypeUsage.CreateStringTypeUsage(primitiveType, flag1, flag3);
            }

            return !flag2
                ? TypeUsage.CreateBinaryTypeUsage(primitiveType, flag3, maxLength)
                : TypeUsage.CreateBinaryTypeUsage(primitiveType, flag3);
        }

        public override TypeUsage GetStoreType(TypeUsage edmType)
        {
            EntityUtils.CheckArgumentNull(edmType, nameof(edmType));
            if (!(edmType.EdmType is PrimitiveType edmType1))
                throw new ArgumentException(string.Format("The underlying provider does not support the type '{0}'.",
                    edmType));
            var facets = edmType.Facets;
            switch ((int)edmType1.PrimitiveTypeKind)
            {
                case 0:
                    var flag1 = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
                    var facet1 = facets["MaxLength"];
                    var flag2 = facet1.IsUnbounded || facet1.Value == null || (int)facet1.Value > 65530;
                    var num1 = !flag2 ? (int)facet1.Value : int.MinValue;
                    return !flag1
                        ? (!flag2
                            ? TypeUsage.CreateBinaryTypeUsage(StoreTypeNameToStorePrimitiveType["varbinaryfox"],
                                false, num1)
                            : TypeUsage.CreateBinaryTypeUsage(StoreTypeNameToStorePrimitiveType["blob"], false,
                                65530))
                        : TypeUsage.CreateBinaryTypeUsage(StoreTypeNameToStorePrimitiveType["raw"], true,
                            flag2 ? 65530 : num1);
                case 1:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["logical"]);
                case 2:
                case 8:
                case 9:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["short"]);
                case 3:
                    return (facets["Precision"].Value == null ? 19 : (byte)facets["Precision"].Value) == 10
                        ? TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["date"])
                        : TypeUsage.CreateDefaultTypeUsage(
                            StoreTypeNameToStorePrimitiveType["timestamp"]);
                case 4:
                    byte precision;
                    byte scale;
                    if (!MetadataHelpers.TryGetPrecision(edmType, out precision) ||
                        !MetadataHelpers.TryGetScale(edmType, out scale))
                        return TypeUsage.CreateDefaultTypeUsage(
                            StoreTypeNameToStorePrimitiveType["numeric"]);
                    return precision == 19 && scale == 4
                        ? TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["money"])
                        : TypeUsage.CreateDecimalTypeUsage(StoreTypeNameToStorePrimitiveType["numeric"], precision,
                            scale);
                case 5:
                case 7:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["double"]);
                case 6:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["guid"]);
                case 10:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["integer"]);
                case 11:
                    return TypeUsage.CreateDefaultTypeUsage(
                        StoreTypeNameToStorePrimitiveType["longinteger"]);
                case 12:
                    var flag3 = facets["Unicode"].Value == null || (bool)facets["Unicode"].Value;
                    var flag4 = facets["FixedLength"].Value != null && (bool)facets["FixedLength"].Value;
                    var facet2 = facets["MaxLength"];
                    var flag5 = facet2.IsUnbounded || facet2.Value == null ||
                                (int)facet2.Value > (flag3 ? 2000 : 4000);
                    var num2 = !flag5 ? (int)facet2.Value : int.MinValue;
                    return !flag3
                        ? (!flag4
                            ? (!flag5
                                ? TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["varcharfox"],
                                    false, false, num2)
                                : TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["memo"], false,
                                    false))
                            : TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["char"], false,
                                true, flag5 ? 4000 : num2))
                        : (!flag4
                            ? (!flag5
                                ? TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["nvarchar"],
                                    true, false, num2)
                                : TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["nmemo"], true,
                                    false))
                            : TypeUsage.CreateStringTypeUsage(StoreTypeNameToStorePrimitiveType["nchar"], true,
                                true, flag5 ? 2000 : num2));
                case 13:
                    return TypeUsage.CreateDefaultTypeUsage(StoreTypeNameToStorePrimitiveType["time"]);
                default:
                    throw new NotSupportedException(string.Format(
                        "There is no store type corresponding to the conceptual side type '{0}' of PrimitiveType '{1}'.",
                        edmType, edmType1.PrimitiveTypeKind));
            }
        }

        public override bool SupportsInExpression() => true;

        private XmlReader GetStoreSchemaMapping()
        {
            return GetXmlResource(
                "Advantage.Data.Provider.Resources.EFAdvantageStoreSchemaMapping.msl");
        }

        private XmlReader GetStoreSchemaDescription()
        {
            return _version == AdsStoreVersion.Advantage9 || _version == AdsStoreVersion.Advantage10 ||
                   _version == AdsStoreVersion.Advantage11 || _version == AdsStoreVersion.Advantage12
                ? GetXmlResource(
                    "Advantage.Data.Provider.Resources.EFAdvantageStoreSchemaDefinition.ssdl")
                : null;
        }

        internal static XmlReader GetXmlResource(string resourceName)
        {
            return XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName),
                null, resourceName);
        }
    }
}