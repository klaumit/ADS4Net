using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using Advantage.Data.Provider.SqlGen;

namespace Advantage.Data.Provider
{
    public class AdsProviderServices : DbProviderServices
    {
        private static readonly AdsProviderServices _instance = new AdsProviderServices();

        public static AdsProviderServices Instance => _instance;

        protected override DbCommandDefinition CreateDbCommandDefinition(
            DbProviderManifest providerManifest,
            DbCommandTree commandTree)
        {
            return providerManifest is AdsProviderManifest
                ? CreateCommandDefinition(CreateCommand((AdsProviderManifest)providerManifest, commandTree))
                : throw new ArgumentException(string.Format("The provider manifest given is not of type '{0}'.",
                    typeof(AdsProviderServices)));
        }

        private DbCommand CreateCommand(AdsProviderManifest providerManifest, DbCommandTree commandTree)
        {
            EntityUtils.CheckArgumentNull(providerManifest, nameof(providerManifest));
            EntityUtils.CheckArgumentNull(commandTree, nameof(commandTree));
            var storeVersion = AdsStoreVersionUtils.GetStoreVersion(providerManifest.Token);
            var command = new AdsCommand();
            Debugger.IsLogging();
            if (commandTree is DbQueryCommandTree queryCommandTree &&
                queryCommandTree.Query is DbProjectExpression query &&
                query.Projection.ResultType.EdmType is StructuralType edmType1)
            {
                for (var index = 0;
                     index < edmType1.Members.Count;
                     ++index)
                {
                    var edmType =
                        edmType1.Members[index]
                        .TypeUsage.EdmType;
                }
            }

            List<AdsParameter> parameters;
            CommandType commandType;
            command.CommandText = SqlGenerator.GenerateSql(commandTree, providerManifest, storeVersion,
                out parameters, out commandType);
            command.CommandType = commandType;
            Debugger.IsLogging();
            EdmFunction edmFunction = null;
            if (commandTree is DbFunctionCommandTree)
                edmFunction = ((DbFunctionCommandTree)commandTree).EdmFunction;
            foreach (var parameter in commandTree.Parameters)
            {
                FunctionParameter functionParameter;
                var adsParameter =
                    edmFunction == null ||
                    !edmFunction.Parameters.TryGetValue(parameter.Key, false, out functionParameter)
                        ? CreateAdsParameter(parameter.Key, parameter.Value, 0,
                            DBNull.Value)
                        : CreateAdsParameter(functionParameter.Name, functionParameter.TypeUsage,
                            functionParameter.Mode, DBNull.Value);
                command.Parameters.Add(adsParameter);
            }

            if (parameters != null && 0 < parameters.Count)
            {
                if (commandTree is DbFunctionCommandTree && commandTree is DbDeleteCommandTree &&
                    commandTree is DbInsertCommandTree && commandTree is DbUpdateCommandTree)
                    throw new InvalidOperationException("Internal error: SqlGenParametersNotPermitted");
                foreach (var adsParameter in parameters)
                    command.Parameters.Add(adsParameter);
            }

            return command;
        }

        protected override string GetDbProviderManifestToken(DbConnection connection)
        {
            EntityUtils.CheckArgumentNull(connection, nameof(connection));
            if (!(connection is AdsConnection connection1))
                throw new ArgumentException(string.Format("The connection is not of type '{0}'.",
                    typeof(AdsConnection)));
            if (string.IsNullOrEmpty(connection1.ConnectionString))
                throw new ArgumentException(
                    "Could not determine storage version; a valid storage connection or a version hint is required.");
            var flag = false;
            try
            {
                if (connection1.State != ConnectionState.Open)
                {
                    connection1.Open();
                    flag = true;
                }

                return AdsStoreVersionUtils.GetVersionHint(AdsStoreVersionUtils.GetStoreVersion(connection1));
            }
            finally
            {
                if (flag)
                    connection1.Close();
            }
        }

        protected override DbProviderManifest GetDbProviderManifest(string versionHint)
        {
            return !string.IsNullOrEmpty(versionHint)
                ? (DbProviderManifest)new AdsProviderManifest(versionHint)
                : throw new ArgumentException(
                    "Could not determine storage version; a valid storage connection or a version hint is required.");
        }

        internal static AdsParameter CreateAdsParameter(
            string name,
            TypeUsage type,
            ParameterMode mode,
            object value)
        {
            var adsParameter = new AdsParameter(name, value);
            var parameterDirection = MetadataHelpers.ParameterModeToParameterDirection(mode);
            if (adsParameter.Direction != parameterDirection)
                adsParameter.Direction = parameterDirection;
            var isOutParam = mode != 0;
            int? size;
            var dbType = GetDbType(type, isOutParam, out size);
            if (adsParameter.DbType != dbType)
                adsParameter.DbType = dbType;
            if (size.HasValue && (isOutParam || adsParameter.Size != size.Value))
                adsParameter.Size = size.Value;
            var flag = MetadataHelpers.IsNullable(type);
            if (isOutParam || flag != adsParameter.IsNullable)
                adsParameter.IsNullable = flag;
            return adsParameter;
        }

        private static DbType GetDbType(TypeUsage type, bool isOutParam, out int? size)
        {
            var primitiveTypeKind = MetadataHelpers.GetPrimitiveTypeKind(type);
            size = new int?();
            switch ((int)primitiveTypeKind)
            {
                case 0:
                    size = GetParameterSize(type, isOutParam);
                    return DbType.Binary;
                case 1:
                    return DbType.Boolean;
                case 2:
                    return DbType.Byte;
                case 3:
                    return DbType.DateTime;
                case 4:
                    return DbType.Decimal;
                case 5:
                    return DbType.Double;
                case 6:
                    return DbType.Binary;
                case 7:
                    return DbType.Double;
                case 8:
                    return DbType.Int16;
                case 9:
                    return DbType.Int16;
                case 10:
                    return DbType.Int32;
                case 11:
                    return DbType.Int64;
                case 12:
                    size = GetParameterSize(type, isOutParam);
                    return GetStringDbType(type);
                case 13:
                    return DbType.Time;
                case 14:
                    return DbType.DateTimeOffset;
                default:
                    return DbType.Object;
            }
        }

        private static int? GetParameterSize(TypeUsage type, bool isOutParam)
        {
            int maxLength;
            if (MetadataHelpers.TryGetMaxLength(type, out maxLength))
                return maxLength;
            return isOutParam ? int.MaxValue : new int?();
        }

        private static DbType GetStringDbType(TypeUsage type)
        {
            DbType stringDbType;
            if (type.EdmType.Name.ToLowerInvariant() == "xml")
            {
                stringDbType = DbType.Xml;
            }
            else
            {
                bool isFixedLength;
                if (!MetadataHelpers.TryGetIsFixedLength(type, out isFixedLength))
                    isFixedLength = false;
                bool isUnicode;
                if (!MetadataHelpers.TryGetIsUnicode(type, out isUnicode))
                    isUnicode = true;
                stringDbType = !isFixedLength
                    ? (isUnicode ? DbType.AnsiString : DbType.String)
                    : (isUnicode ? DbType.AnsiStringFixedLength : DbType.StringFixedLength);
            }

            return stringDbType;
        }

        private static DbType GetBinaryDbType(TypeUsage type)
        {
            bool isFixedLength;
            if (!MetadataHelpers.TryGetIsFixedLength(type, out isFixedLength))
                isFixedLength = false;
            return DbType.Binary;
        }
    }
}