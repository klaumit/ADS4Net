using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;

namespace Advantage.Data.Provider
{
    internal static class MetadataHelpers
    {
        internal const string MaxLengthFacetName = "MaxLength";
        internal const string UnicodeFacetName = "Unicode";
        internal const string FixedLengthFacetName = "FixedLength";
        internal const string PrecisionFacetName = "Precision";
        internal const string ScaleFacetName = "Scale";
        internal const string NullableFacetName = "Nullable";
        internal const string DefaultValueFacetName = "DefaultValue";
        internal const string TableMetadata = "Table";
        internal const string SchemaMetadata = "Schema";
        internal const string DefiningQueryMetadata = "DefiningQuery";
        internal const string CommandTextMetadata = "CommandTextAttribute";
        internal const string StoreFunctionNameMetadata = "StoreFunctionNameAttribute";
        internal const string BuiltInMetadata = "BuiltInAttribute";
        internal const string NiladicFunctionMetadata = "NiladicFunctionAttribute";
        internal const string OracleCursorParameterNameMetadata = "EFOracleProviderExtensions:CursorParameterName";
        internal const string EdmNamespaceName = "Edm";

        internal static FacetDescription GetFacet(
            IEnumerable<FacetDescription> facetCollection,
            string facetName)
        {
            foreach (var facet in facetCollection)
            {
                if (facet.FacetName == facetName)
                    return facet;
            }

            return null;
        }

        internal static bool TryGetBooleanFacetValue(
            TypeUsage type,
            string facetName,
            out bool boolValue)
        {
            boolValue = false;
            Facet facet;
            if (!type.Facets.TryGetValue(facetName, false, out facet) || facet.Value == null)
                return false;
            boolValue = (bool)facet.Value;
            return true;
        }

        internal static bool TryGetByteFacetValue(TypeUsage type, string facetName, out byte byteValue)
        {
            byteValue = 0;
            Facet facet;
            if (!type.Facets.TryGetValue(facetName, false, out facet) || facet.Value == null || facet.IsUnbounded)
                return false;
            byteValue = (byte)facet.Value;
            return true;
        }

        internal static bool TryGetIntFacetValue(TypeUsage type, string facetName, out int intValue)
        {
            intValue = 0;
            Facet facet;
            if (!type.Facets.TryGetValue(facetName, false, out facet) || facet.Value == null || facet.IsUnbounded)
                return false;
            intValue = (int)facet.Value;
            return true;
        }

        internal static bool TryGetIsFixedLength(TypeUsage type, out bool isFixedLength)
        {
            if (IsPrimitiveType(type, (PrimitiveTypeKind)12) ||
                IsPrimitiveType(type, 0))
                return TryGetBooleanFacetValue(type, "FixedLength", out isFixedLength);
            isFixedLength = false;
            return false;
        }

        internal static bool TryGetIsUnicode(TypeUsage type, out bool isUnicode)
        {
            if (IsPrimitiveType(type, (PrimitiveTypeKind)12))
                return TryGetBooleanFacetValue(type, "Unicode", out isUnicode);
            isUnicode = false;
            return false;
        }

        internal static bool IsFacetValueConstant(TypeUsage type, string facetName)
        {
            return GetFacet(((PrimitiveType)type.EdmType).FacetDescriptions, facetName)
                .IsConstant;
        }

        internal static bool TryGetMaxLength(TypeUsage type, out int maxLength)
        {
            if (IsPrimitiveType(type, (PrimitiveTypeKind)12) ||
                IsPrimitiveType(type, 0))
                return TryGetIntFacetValue(type, "MaxLength", out maxLength);
            maxLength = 0;
            return false;
        }

        internal static bool TryGetPrecision(TypeUsage type, out byte precision)
        {
            if (IsPrimitiveType(type, (PrimitiveTypeKind)4))
                return TryGetByteFacetValue(type, "Precision", out precision);
            precision = 0;
            return false;
        }

        internal static bool TryGetScale(TypeUsage type, out byte scale)
        {
            if (IsPrimitiveType(type, (PrimitiveTypeKind)4))
                return TryGetByteFacetValue(type, "Scale", out scale);
            scale = 0;
            return false;
        }

        internal static StoreGeneratedPattern GetStoreGeneratedPattern(EdmMember member)
        {
            Facet facet;
            return member.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && facet.Value != null
                ? (StoreGeneratedPattern)facet.Value
                : 0;
        }

        internal static bool TryGetPrimitiveTypeKind(TypeUsage type, out PrimitiveTypeKind typeKind)
        {
// TODO
            typeKind = PrimitiveTypeKind.Geography;

            if (type != null && type.EdmType != null &&
                type.EdmType.BuiltInTypeKind == (BuiltInTypeKind)26)
            {
// ISSUE: cast to a reference type
// ISSUE: explicit reference operation
// TODO ^(int&) ref typeKind = (int) ((PrimitiveType) type.EdmType).PrimitiveTypeKind;
                return true;
            }

// ISSUE: cast to a reference type
// ISSUE: explicit reference operation
// TODO ^(int&) ref typeKind = 0;
            return false;
        }

        internal static PrimitiveTypeKind GetPrimitiveTypeKind(TypeUsage typeUsage)
        {
            return ((PrimitiveType)typeUsage.EdmType).PrimitiveTypeKind;
        }

        internal static bool IsPrimitiveType(EdmType type)
        {
            return 26 == (int)type.BuiltInTypeKind;
        }

        internal static bool IsPrimitiveType(TypeUsage type, PrimitiveTypeKind primitiveTypeKind)
        {
            PrimitiveTypeKind typeKind;
            return TryGetPrimitiveTypeKind(type, out typeKind) && typeKind == primitiveTypeKind;
        }

        internal static bool IsNullable(TypeUsage type)
        {
            Facet facet;
            return !type.Facets.TryGetValue("Nullable", false, out facet) || (bool)facet.Value;
        }

        internal static bool IsReferenceType(GlobalItem item)
        {
            return 31 == (int)item.BuiltInTypeKind;
        }

        internal static bool IsRowType(GlobalItem item) => 36 == (int)item.BuiltInTypeKind;

        internal static bool IsCollectionType(GlobalItem item)
        {
            return 6 == (int)item.BuiltInTypeKind;
        }

        internal static TypeUsage GetElementTypeUsage(TypeUsage type)
        {
            if (IsCollectionType(type.EdmType))
                return ((CollectionType)type.EdmType).TypeUsage;
            return IsReferenceType(type.EdmType)
                ? TypeUsage.CreateDefaultTypeUsage(((RefType)type.EdmType).ElementType)
                : null;
        }

        internal static TEdmType GetEdmType<TEdmType>(TypeUsage typeUsage) where TEdmType : EdmType
        {
            return (TEdmType)typeUsage.EdmType;
        }

        internal static bool IsCanonicalFunction(EdmFunction function)
        {
            return function.NamespaceName.Equals("Edm", StringComparison.InvariantCulture);
        }

        internal static IList<EdmProperty> GetProperties(TypeUsage typeUsage)
        {
            return GetProperties(typeUsage.EdmType);
        }

        internal static IList<EdmProperty> GetProperties(EdmType edmType)
        {
            var builtInTypeKind = edmType.BuiltInTypeKind;
            if (builtInTypeKind == (BuiltInTypeKind)8)
                return ((ComplexType)edmType).Properties;
            if (builtInTypeKind == (BuiltInTypeKind)14)
                return ((EntityType)edmType).Properties;
            return builtInTypeKind == (BuiltInTypeKind)36
                ? ((RowType)edmType).Properties
                : new List<EdmProperty>();
        }

        internal static T GetMetadataProperty<T>(MetadataItem item, string propertyName)
        {
            MetadataProperty metadataProperty = default;
            return !item.MetadataProperties.TryGetValue(propertyName, true, out metadataProperty) ||
                   !(metadataProperty.Value is T)
                ? default(T)
                : (T)metadataProperty.Value;
        }

        internal static ParameterDirection ParameterModeToParameterDirection(ParameterMode mode)
        {
            switch ((int)mode)
            {
                case 0:
                    return ParameterDirection.Input;
                case 1:
                    return ParameterDirection.Output;
                case 2:
                    return ParameterDirection.InputOutput;
                case 3:
                    return ParameterDirection.ReturnValue;
                default:
                    return 0;
            }
        }
    }
}