using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;

namespace Advantage.Data.Provider
{
    public sealed class AdsCommandBuilder : DbCommandBuilder, IDisposable
    {
        private bool mbDisposed;
        private string mstrPrefix = "[";
        private string mstrSuffix = "]";
        private DataTable mSchemaTable;
        private string mstrTableName = "";
        private string mstrCatalogName = "";
        private AdsCommand mInsertCmd;
        private AdsCommand mUpdateCmd;
        private AdsCommand mDeleteCmd;
        private bool mbPKOnlyInWhere;
        private bool mbRequirePK = true;
        private bool mbUseOnlyRowversionInWhere;

        public AdsCommandBuilder()
        {
        }

        ~AdsCommandBuilder() => Dispose(false);

        public AdsCommandBuilder(AdsDataAdapter adapter) => DataAdapter = adapter;

        protected override void Dispose(bool bExplicitDispose)
        {
            if (mbDisposed)
                return;
            lock (this)
            {
                if (mbDisposed)
                    return;
                if (bExplicitDispose)
                {
                    if (mUpdateCmd != null)
                        mUpdateCmd.Dispose();
                    if (mInsertCmd != null)
                        mInsertCmd.Dispose();
                    if (mDeleteCmd != null)
                        mDeleteCmd.Dispose();
                    mUpdateCmd = mInsertCmd = mDeleteCmd = null;
                }

                base.Dispose(bExplicitDispose);
                mbDisposed = true;
            }
        }

        private void GetSchema()
        {
            var adsDataReader = DataAdapter != null && DataAdapter.SelectCommand != null
                ? DataAdapter.SelectCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo)
                : throw new InvalidOperationException(
                    "The DataAdapter.SelectCommand property needs to be initialized.");
            var schemaTable = adsDataReader.GetSchemaTable();
            adsDataReader.Close();
            if (adsDataReader.IsStatic)
                throw new InvalidOperationException("Command Builder unable to generate commands for static cursors.");
            if (schemaTable.Rows.Count <= 0)
                throw new InvalidOperationException("Unable to retrieve valid schema.");
            mstrTableName = schemaTable.Rows[0]["BaseTableName"].ToString();
            mstrCatalogName = schemaTable.Rows[0]["BaseCatalogName"].ToString();
            if (mbRequirePK)
            {
                var flag = false;
                foreach (DataRow row in (InternalDataCollectionBase)schemaTable.Rows)
                {
                    if (row["IsKey"].Equals(true) || row["IsUnique"].Equals(true))
                        flag = true;
                }

                if (!flag)
                    throw new InvalidOperationException(
                        "Select command must return a primary key or unique column because the RequirePrimaryKey option was selected.");
            }

            if (mbUseOnlyRowversionInWhere)
            {
                var flag = false;
                foreach (DataRow row in (InternalDataCollectionBase)schemaTable.Rows)
                {
                    if (row["IsRowversion"].Equals(true))
                        flag = true;
                }

                if (!flag)
                    throw new InvalidOperationException(
                        "Select command must return a RowVersion column because the UseRowversionOnlyInWhereClause option was selected.");
            }

            mSchemaTable = schemaTable;
        }

        private AdsCommand GetNewCommand()
        {
            var newCommand = new AdsCommand();
            newCommand.Connection = DataAdapter.SelectCommand.Connection;
            newCommand.Transaction = DataAdapter.SelectCommand.Transaction;
            newCommand.CommandTimeout = DataAdapter.SelectCommand.CommandTimeout;
            newCommand.UpdatedRowSource = UpdateRowSource.None;
            return newCommand;
        }

        private string QualifiedTableName()
        {
            if (mstrCatalogName == null || mstrCatalogName.Length == 0)
                return mstrPrefix + mstrTableName + mstrSuffix;
            return mstrPrefix + mstrCatalogName + mstrSuffix + "." + mstrPrefix +
                   mstrTableName + mstrSuffix;
        }

        private void GetDelete()
        {
            mDeleteCmd = GetNewCommand();
            var whereClause = GetWhereClause(mDeleteCmd, 1);
            mDeleteCmd.CommandText = "DELETE FROM " + QualifiedTableName() + " WHERE " + whereClause;
        }

        private void GetInsert()
        {
            var flag = false;
            mInsertCmd = GetNewCommand();
            var stringBuilder1 = new StringBuilder();
            var stringBuilder2 = new StringBuilder();
            var num = 1;
            foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
            {
                if (!row["IsAutoIncrement"].Equals(true) && !row["IsRowVersion"].Equals(true) &&
                    !row["IsReadOnly"].Equals(true))
                {
                    flag = true;
                    if (stringBuilder1.Length > 0)
                        stringBuilder1.Append(", ");
                    if (stringBuilder2.Length > 0)
                        stringBuilder2.Append(", ");
                    var precision = Type.GetTypeCode(row["NumericPrecision"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericPrecision"];
                    var scale = Type.GetTypeCode(row["NumericScale"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericScale"];
                    var adsParameter = new AdsParameter(":p" + num,
                        GetDbType((int)row["ProviderType"]), (int)row["ColumnSize"], ParameterDirection.Input,
                        (bool)row["AllowDBNull"], precision, scale, row["ColumnName"].ToString(),
                        DataRowVersion.Current, null);
                    mInsertCmd.Parameters.Add(adsParameter);
                    stringBuilder1.Append(mstrPrefix + row["BaseColumnName"] + mstrSuffix);
                    stringBuilder2.Append(":" + adsParameter.ParameterName);
                    ++num;
                }
            }

            if (!flag)
                throw new InvalidOperationException("There are no columns that can be updated.");
            mInsertCmd.CommandText = "INSERT INTO " + QualifiedTableName() + " (" +
                                          stringBuilder1 + ") VALUES (" + stringBuilder2 + ")";
        }

        private void GetUpdate()
        {
            var flag = false;
            mUpdateCmd = GetNewCommand();
            var stringBuilder = new StringBuilder();
            var iParamIndex = 1;
            foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
            {
                if (!row["IsRowVersion"].Equals(true) && !row["IsAutoIncrement"].Equals(true) &&
                    !row["IsReadOnly"].Equals(true))
                {
                    flag = true;
                    if (stringBuilder.Length > 0)
                        stringBuilder.Append(", ");
                    var precision = Type.GetTypeCode(row["NumericPrecision"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericPrecision"];
                    var scale = Type.GetTypeCode(row["NumericScale"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericScale"];
                    var adsParameter = new AdsParameter(":p" + iParamIndex,
                        GetDbType((int)row["ProviderType"]), (int)row["ColumnSize"], ParameterDirection.Input,
                        (bool)row["AllowDBNull"], precision, scale, row["ColumnName"].ToString(),
                        DataRowVersion.Current, null);
                    mUpdateCmd.Parameters.Add(adsParameter);
                    stringBuilder.Append(mstrPrefix + row["BaseColumnName"] + mstrSuffix + " = :" +
                                         adsParameter.ParameterName);
                    ++iParamIndex;
                }
            }

            if (!flag)
                throw new InvalidOperationException("There are no columns that can be updated.");
            var whereClause = GetWhereClause(mUpdateCmd, iParamIndex);
            mUpdateCmd.CommandText = "UPDATE " + QualifiedTableName() + " SET " + stringBuilder +
                                          " WHERE " + whereClause;
        }

        private string GetWhereClause(AdsCommand cmd, int iParamIndex)
        {
            var stringBuilder = new StringBuilder();
            var num = iParamIndex;
            foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
            {
                if (mbUseOnlyRowversionInWhere)
                {
                    if (row["IsRowversion"].Equals(false) && !row["ProviderType"].Equals(15))
                        continue;
                }
                else if (mbPKOnlyInWhere && row["IsKey"].Equals(false) &&
                         row["IsUnique"].Equals(false))
                    continue;

                if (!row["IsLong"].Equals(true))
                {
                    if (stringBuilder.Length > 0)
                        stringBuilder.Append(" AND ");
                    var precision = Type.GetTypeCode(row["NumericPrecision"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericPrecision"];
                    var scale = Type.GetTypeCode(row["NumericScale"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericScale"];
                    var adsParameter = new AdsParameter(":p" + num,
                        GetDbType((int)row["ProviderType"]), (int)row["ColumnSize"], ParameterDirection.Input,
                        (bool)row["AllowDBNull"], precision, scale, row["ColumnName"].ToString(),
                        DataRowVersion.Original, null);
                    cmd.Parameters.Add(adsParameter);
                    stringBuilder.Append("(" + mstrPrefix + row["BaseColumnName"] + mstrSuffix +
                                         " = :" + adsParameter.ParameterName + ")");
                    ++num;
                }
            }

            return stringBuilder.ToString();
        }

        private void OnRowUpdating(object obj, AdsRowUpdatingEventArgs args)
        {
            if (args.Status != UpdateStatus.Continue)
                return;
            switch (args.StatementType)
            {
                case StatementType.Select:
                    return;
                case StatementType.Insert:
                    GetInsertCommand();
                    args.Command = mInsertCmd;
                    break;
                case StatementType.Update:
                    GetUpdateCommand();
                    args.Command = mUpdateCmd;
                    break;
                case StatementType.Delete:
                    GetDeleteCommand();
                    args.Command = mDeleteCmd;
                    break;
            }

            var tableMapping = args.TableMapping;
            foreach (AdsParameter parameter in (DbParameterCollection)args.Command.Parameters)
                parameter.Value = args.Row[MapColumnName(tableMapping, parameter.SourceColumn),
                    parameter.SourceVersion];
        }

        private string MapColumnName(DataTableMapping dtm, string strName)
        {
            if (dtm == null || dtm.ColumnMappings.Count == 0 || !dtm.ColumnMappings.Contains(strName))
                return strName;
            var columnMapping = dtm.ColumnMappings[strName];
            return columnMapping == null ? strName : columnMapping.DataSetColumn;
        }

        private DbType GetDbType(int iAceType)
        {
            switch (iAceType)
            {
                case 0:
                    return DbType.Object;
                case 1:
                    return DbType.Boolean;
                case 2:
                    return DbType.Decimal;
                case 3:
                case 9:
                    return DbType.Date;
                case 4:
                case 5:
                case 8:
                case 20:
                case 23:
                case 26:
                case 27:
                case 28:
                    return DbType.String;
                case 6:
                case 7:
                case 16:
                case 24:
                    return DbType.Binary;
                case 10:
                case 17:
                    return DbType.Double;
                case 11:
                case 15:
                    return DbType.Int32;
                case 12:
                    return DbType.Int16;
                case 13:
                    return DbType.Time;
                case 14:
                case 22:
                    return DbType.DateTime;
                case 18:
                    return DbType.Currency;
                case 19:
                case 21:
                    return DbType.Int64;
                case 29:
                    return DbType.Guid;
                default:
                    throw new SystemException("Value is of unknown data type");
            }
        }

        public override void RefreshSchema()
        {
            mSchemaTable = null;
            mInsertCmd = null;
            mUpdateCmd = null;
            mDeleteCmd = null;
        }

        public static void DeriveParameters(AdsCommand command)
        {
            if (command.CommandType != CommandType.StoredProcedure)
                throw new InvalidOperationException(string.Format(
                    "DeriveParameters only supports CommandType.StoredProcedure, not CommandType.{0}",
                    command.CommandType));
            command.DeriveParameters();
        }

        protected override void ApplyParameterInfo(
            DbParameter parameter,
            DataRow datarow,
            StatementType statementType,
            bool whereClause)
        {
            var adsParameter = (AdsParameter)parameter;
            var iAceType = datarow[SchemaTableColumn.ProviderType];
            adsParameter.DbType = GetDbType((int)iAceType);
            var obj1 = datarow[SchemaTableColumn.NumericPrecision];
            if (DBNull.Value != obj1)
            {
                var num = (byte)(short)obj1;
                adsParameter.Precision = num != byte.MaxValue ? num : (byte)0;
            }

            var obj2 = datarow[SchemaTableColumn.NumericScale];
            if (DBNull.Value == obj2)
                return;
            var num1 = (byte)(short)obj2;
            if (num1 == byte.MaxValue)
                adsParameter.Scale = 0;
            else
                adsParameter.Scale = num1;
        }

        protected override string GetParameterName(int parameterOrdinal)
        {
            return ":p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
        }

        protected override string GetParameterName(string parameterName) => ":" + parameterName;

        protected override string GetParameterPlaceholder(int parameterOrdinal)
        {
            return ":p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
        }

        protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
        {
            if (adapter == base.DataAdapter)
                ((AdsDataAdapter)adapter).RowUpdating -= OnRowUpdating;
            else
                ((AdsDataAdapter)adapter).RowUpdating += OnRowUpdating;
        }

        public new AdsCommand GetDeleteCommand()
        {
            if (mSchemaTable == null)
                GetSchema();
            if (mDeleteCmd == null)
                GetDelete();
            return mDeleteCmd;
        }

        public new AdsCommand GetInsertCommand()
        {
            if (mSchemaTable == null)
                GetSchema();
            if (mInsertCmd == null)
                GetInsert();
            return mInsertCmd;
        }

        private string GetRefreshSelect()
        {
            var flag = true;
            var stringBuilder = new StringBuilder();
            if (mSchemaTable == null)
                GetSchema();
            stringBuilder.Append("SELECT ");
            foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
            {
                if (flag)
                    flag = false;
                else
                    stringBuilder.Append(", ");
                if (row["BaseColumnName"].ToString().ToUpper().Equals(row["ColumnName"].ToString().ToUpper()))
                    stringBuilder.Append(mstrPrefix + row["BaseColumnName"] + mstrSuffix);
                else
                    stringBuilder.Append(mstrPrefix + row["BaseColumnName"] + mstrSuffix + " " +
                                         mstrPrefix + row["ColumnName"] + mstrSuffix);
            }

            stringBuilder.Append(" FROM " + QualifiedTableName());
            return stringBuilder.ToString();
        }

        private string GetRefreshWhereClause(AdsCommand cmd)
        {
            var flag1 = true;
            var flag2 = false;
            var stringBuilder = new StringBuilder();
            foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
            {
                if ((row["ProviderType"].Equals(21) || row["ProviderType"].Equals(22)) &&
                    row["IsKey"].Equals(true))
                {
                    flag2 = true;
                    break;
                }
            }

            foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
            {
                if (!row["ProviderType"].Equals(21) && !row["ProviderType"].Equals(22) &&
                    !row["IsLong"].Equals(true) && (row["IsKey"].Equals(true) && !flag2 ||
                                                    row["IsUnique"].Equals(true)))
                {
                    AdsParameter adsParameter = null;
                    foreach (AdsParameter parameter in (DbParameterCollection)cmd.Parameters)
                    {
                        if (parameter.SourceColumn.Equals(row["ColumnName"]) &&
                            (parameter.SourceVersion == DataRowVersion.Current ||
                             row["ProviderType"].Equals(15)))
                        {
                            adsParameter = parameter;
                            break;
                        }
                    }

                    if (adsParameter != null)
                    {
                        if (flag1)
                            flag1 = false;
                        else
                            stringBuilder.Append(" AND ");
                        stringBuilder.Append("(" + mstrPrefix + row["BaseColumnName"] +
                                             mstrSuffix + " = :" + adsParameter.ParameterName + ")");
                    }
                }
            }

            if (stringBuilder.Length == 0)
            {
                var flag3 = true;
                foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
                {
                    if (!row["ProviderType"].Equals(21) && !row["ProviderType"].Equals(22) &&
                        !row["IsLong"].Equals(true))
                    {
                        AdsParameter adsParameter = null;
                        foreach (AdsParameter parameter in (DbParameterCollection)cmd.Parameters)
                        {
                            if (parameter.SourceColumn.Equals(row["ColumnName"]) &&
                                parameter.SourceVersion == DataRowVersion.Current)
                            {
                                adsParameter = parameter;
                                break;
                            }
                        }

                        if (adsParameter != null)
                        {
                            if (flag3)
                                flag3 = false;
                            else
                                stringBuilder.Append(" AND ");
                            stringBuilder.Append("(" + mstrPrefix + row["BaseColumnName"] +
                                                 mstrSuffix + " = :" + adsParameter.ParameterName + ")");
                        }
                    }
                }
            }

            return stringBuilder.Length != 0
                ? stringBuilder.ToString()
                : throw new InvalidOperationException(
                    "Select command must return a primary key or unique columns in order to generate the statement to refresh the updated row.");
        }

        public string GetInsertRefreshStatement()
        {
            var stringBuilder = new StringBuilder();
            GetInsertCommand();
            stringBuilder.Append(GetRefreshSelect());
            stringBuilder.Append(" WHERE ");
            foreach (DataRow row in (InternalDataCollectionBase)mSchemaTable.Rows)
            {
                if (row["ProviderType"].Equals(15))
                {
                    stringBuilder.Append("(" + mstrPrefix + row["BaseColumnName"] + mstrSuffix +
                                         " = LASTAUTOINC(statement))");
                    return stringBuilder.ToString();
                }
            }

            var refreshWhereClause = GetRefreshWhereClause(mInsertCmd);
            stringBuilder.Append(refreshWhereClause);
            return stringBuilder.ToString();
        }

        public new AdsCommand GetUpdateCommand()
        {
            if (mSchemaTable == null)
                GetSchema();
            if (mUpdateCmd == null)
                GetUpdate();
            return mUpdateCmd;
        }

        public string GetUpdateRefreshStatement()
        {
            var stringBuilder = new StringBuilder();
            GetUpdateCommand();
            stringBuilder.Append(GetRefreshSelect());
            stringBuilder.Append(" WHERE ");
            var refreshWhereClause = GetRefreshWhereClause(mUpdateCmd);
            stringBuilder.Append(refreshWhereClause);
            return stringBuilder.ToString();
        }

        [Description("The DataAdapter for which to automatically generate AdsCommands.")]
        public new AdsDataAdapter DataAdapter
        {
            get => (AdsDataAdapter)base.DataAdapter;
            set
            {
                RefreshSchema();
                DataAdapter = value;
            }
        }

        [Browsable(false)]
        public override string QuotePrefix
        {
            get => mstrPrefix;
            set
            {
                if (mSchemaTable != null)
                    throw new InvalidOperationException(
                        "QuotePrefix cannot be modified after commands have been generated.");
                mstrPrefix = !(value != "[") || !(value != "\"")
                    ? value
                    : throw new ArgumentException(
                        "The acceptable values for the property 'QuotePrefix' are '[' or '\"'.");
            }
        }

        [Browsable(false)]
        public override string QuoteSuffix
        {
            get => mstrSuffix;
            set
            {
                if (mSchemaTable != null)
                    throw new InvalidOperationException(
                        "QuoteSuffix cannot be modified after commands have been generated.");
                mstrSuffix = !(value != "]") || !(value != "\"")
                    ? value
                    : throw new ArgumentException(
                        "The acceptable values for the property 'QuoteSuffix' are ']' or '\"'.");
            }
        }

        [Browsable(false)]
        public bool UsePKOnlyInWhereClause
        {
            get => mbPKOnlyInWhere;
            set
            {
                mbPKOnlyInWhere = !value || mbRequirePK
                    ? value
                    : throw new InvalidOperationException(
                        "The UsePKOnlyInWhereClause property cannot be true when RequirePrimaryKey is set to false.");
            }
        }

        [Browsable(false)]
        public bool RequirePrimaryKey
        {
            get => mbRequirePK;
            set
            {
                mbRequirePK = value || !mbPKOnlyInWhere
                    ? value
                    : throw new InvalidOperationException(
                        "The RequirePrimaryKey property cannot be false when UsePKOnlyInWhereClause is set to true.");
            }
        }

        [Browsable(false)]
        public bool UseRowversionOnlyInWhereClause
        {
            get => mbUseOnlyRowversionInWhere;
            set
            {
                mbUseOnlyRowversionInWhere = !value || mbRequirePK
                    ? value
                    : throw new InvalidOperationException(
                        "The UseOnlyRowversion property cannot be true when RequirePrimaryKey is set to false.");
            }
        }

        protected override DataTable GetSchemaTable(DbCommand srcCommand)
        {
            var adsDataReader =
                (srcCommand as AdsCommand).ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
            adsDataReader.GetSchemaTable();
            adsDataReader.Close();
            return base.GetSchemaTable(srcCommand);
        }

        protected override DbCommand InitializeCommand(DbCommand command)
        {
            return base.InitializeCommand(command);
        }

        private void CheckQuoteConsistency()
        {
            if (mstrPrefix == "\"" && mstrSuffix != "\"" || mstrPrefix == "[" && mstrSuffix != "]")
                throw new ArgumentException("The QuotePrefix and QuoteSuffix properties are not consistent.");
        }

        public override string QuoteIdentifier(string unquotedIdentifier)
        {
            if (unquotedIdentifier == null)
                throw new ArgumentException();
            CheckQuoteConsistency();
            return mstrPrefix + unquotedIdentifier + mstrSuffix;
        }

        public override string UnquoteIdentifier(string quotedIdentifier)
        {
            if (quotedIdentifier == null)
                throw new ArgumentException();
            CheckQuoteConsistency();
            return !quotedIdentifier.StartsWith(mstrPrefix) || !quotedIdentifier.EndsWith(mstrSuffix)
                ? quotedIdentifier
                : quotedIdentifier.Substring(mstrPrefix.Length,
                    quotedIdentifier.Length - (mstrPrefix.Length + mstrSuffix.Length));
        }
    }
}