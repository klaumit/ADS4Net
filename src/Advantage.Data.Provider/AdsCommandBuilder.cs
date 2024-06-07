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

        ~AdsCommandBuilder() => this.Dispose(false);

        public AdsCommandBuilder(AdsDataAdapter adapter) => this.DataAdapter = adapter;

        protected override void Dispose(bool bExplicitDispose)
        {
            if (this.mbDisposed)
                return;
            lock (this)
            {
                if (this.mbDisposed)
                    return;
                if (bExplicitDispose)
                {
                    if (this.mUpdateCmd != null)
                        this.mUpdateCmd.Dispose();
                    if (this.mInsertCmd != null)
                        this.mInsertCmd.Dispose();
                    if (this.mDeleteCmd != null)
                        this.mDeleteCmd.Dispose();
                    this.mUpdateCmd = this.mInsertCmd = this.mDeleteCmd = (AdsCommand)null;
                }

                base.Dispose(bExplicitDispose);
                this.mbDisposed = true;
            }
        }

        private void GetSchema()
        {
            AdsDataReader adsDataReader = this.DataAdapter != null && this.DataAdapter.SelectCommand != null
                ? this.DataAdapter.SelectCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo)
                : throw new InvalidOperationException(
                    "The DataAdapter.SelectCommand property needs to be initialized.");
            DataTable schemaTable = adsDataReader.GetSchemaTable();
            adsDataReader.Close();
            if (adsDataReader.IsStatic)
                throw new InvalidOperationException("Command Builder unable to generate commands for static cursors.");
            if (schemaTable.Rows.Count <= 0)
                throw new InvalidOperationException("Unable to retrieve valid schema.");
            this.mstrTableName = schemaTable.Rows[0]["BaseTableName"].ToString();
            this.mstrCatalogName = schemaTable.Rows[0]["BaseCatalogName"].ToString();
            if (this.mbRequirePK)
            {
                bool flag = false;
                foreach (DataRow row in (InternalDataCollectionBase)schemaTable.Rows)
                {
                    if (row["IsKey"].Equals((object)true) || row["IsUnique"].Equals((object)true))
                        flag = true;
                }

                if (!flag)
                    throw new InvalidOperationException(
                        "Select command must return a primary key or unique column because the RequirePrimaryKey option was selected.");
            }

            if (this.mbUseOnlyRowversionInWhere)
            {
                bool flag = false;
                foreach (DataRow row in (InternalDataCollectionBase)schemaTable.Rows)
                {
                    if (row["IsRowversion"].Equals((object)true))
                        flag = true;
                }

                if (!flag)
                    throw new InvalidOperationException(
                        "Select command must return a RowVersion column because the UseRowversionOnlyInWhereClause option was selected.");
            }

            this.mSchemaTable = schemaTable;
        }

        private AdsCommand GetNewCommand()
        {
            AdsCommand newCommand = new AdsCommand();
            newCommand.Connection = this.DataAdapter.SelectCommand.Connection;
            newCommand.Transaction = this.DataAdapter.SelectCommand.Transaction;
            newCommand.CommandTimeout = this.DataAdapter.SelectCommand.CommandTimeout;
            newCommand.UpdatedRowSource = UpdateRowSource.None;
            return newCommand;
        }

        private string QualifiedTableName()
        {
            if (this.mstrCatalogName == null || this.mstrCatalogName.Length == 0)
                return this.mstrPrefix + this.mstrTableName + this.mstrSuffix;
            return this.mstrPrefix + this.mstrCatalogName + this.mstrSuffix + "." + this.mstrPrefix +
                   this.mstrTableName + this.mstrSuffix;
        }

        private void GetDelete()
        {
            this.mDeleteCmd = this.GetNewCommand();
            string whereClause = this.GetWhereClause(this.mDeleteCmd, 1);
            this.mDeleteCmd.CommandText = "DELETE FROM " + this.QualifiedTableName() + " WHERE " + whereClause;
        }

        private void GetInsert()
        {
            bool flag = false;
            this.mInsertCmd = this.GetNewCommand();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            int num = 1;
            foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
            {
                if (!row["IsAutoIncrement"].Equals((object)true) && !row["IsRowVersion"].Equals((object)true) &&
                    !row["IsReadOnly"].Equals((object)true))
                {
                    flag = true;
                    if (stringBuilder1.Length > 0)
                        stringBuilder1.Append(", ");
                    if (stringBuilder2.Length > 0)
                        stringBuilder2.Append(", ");
                    byte precision = Type.GetTypeCode(row["NumericPrecision"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericPrecision"];
                    byte scale = Type.GetTypeCode(row["NumericScale"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericScale"];
                    AdsParameter adsParameter = new AdsParameter(":p" + num.ToString(),
                        this.GetDbType((int)row["ProviderType"]), (int)row["ColumnSize"], ParameterDirection.Input,
                        (bool)row["AllowDBNull"], precision, scale, row["ColumnName"].ToString(),
                        DataRowVersion.Current, (object)null);
                    this.mInsertCmd.Parameters.Add(adsParameter);
                    stringBuilder1.Append(this.mstrPrefix + row["BaseColumnName"].ToString() + this.mstrSuffix);
                    stringBuilder2.Append(":" + adsParameter.ParameterName);
                    ++num;
                }
            }

            if (!flag)
                throw new InvalidOperationException("There are no columns that can be updated.");
            this.mInsertCmd.CommandText = "INSERT INTO " + this.QualifiedTableName() + " (" +
                                          stringBuilder1.ToString() + ") VALUES (" + (object)stringBuilder2 + ")";
        }

        private void GetUpdate()
        {
            bool flag = false;
            this.mUpdateCmd = this.GetNewCommand();
            StringBuilder stringBuilder = new StringBuilder();
            int iParamIndex = 1;
            foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
            {
                if (!row["IsRowVersion"].Equals((object)true) && !row["IsAutoIncrement"].Equals((object)true) &&
                    !row["IsReadOnly"].Equals((object)true))
                {
                    flag = true;
                    if (stringBuilder.Length > 0)
                        stringBuilder.Append(", ");
                    byte precision = Type.GetTypeCode(row["NumericPrecision"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericPrecision"];
                    byte scale = Type.GetTypeCode(row["NumericScale"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericScale"];
                    AdsParameter adsParameter = new AdsParameter(":p" + iParamIndex.ToString(),
                        this.GetDbType((int)row["ProviderType"]), (int)row["ColumnSize"], ParameterDirection.Input,
                        (bool)row["AllowDBNull"], precision, scale, row["ColumnName"].ToString(),
                        DataRowVersion.Current, (object)null);
                    this.mUpdateCmd.Parameters.Add(adsParameter);
                    stringBuilder.Append(this.mstrPrefix + row["BaseColumnName"].ToString() + this.mstrSuffix + " = :" +
                                         adsParameter.ParameterName);
                    ++iParamIndex;
                }
            }

            if (!flag)
                throw new InvalidOperationException("There are no columns that can be updated.");
            string whereClause = this.GetWhereClause(this.mUpdateCmd, iParamIndex);
            this.mUpdateCmd.CommandText = "UPDATE " + this.QualifiedTableName() + " SET " + (object)stringBuilder +
                                          " WHERE " + whereClause;
        }

        private string GetWhereClause(AdsCommand cmd, int iParamIndex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int num = iParamIndex;
            foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
            {
                if (this.mbUseOnlyRowversionInWhere)
                {
                    if (row["IsRowversion"].Equals((object)false) && !row["ProviderType"].Equals((object)15))
                        continue;
                }
                else if (this.mbPKOnlyInWhere && row["IsKey"].Equals((object)false) &&
                         row["IsUnique"].Equals((object)false))
                    continue;

                if (!row["IsLong"].Equals((object)true))
                {
                    if (stringBuilder.Length > 0)
                        stringBuilder.Append(" AND ");
                    byte precision = Type.GetTypeCode(row["NumericPrecision"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericPrecision"];
                    byte scale = Type.GetTypeCode(row["NumericScale"].GetType()) == TypeCode.DBNull
                        ? (byte)0
                        : (byte)(short)row["NumericScale"];
                    AdsParameter adsParameter = new AdsParameter(":p" + num.ToString(),
                        this.GetDbType((int)row["ProviderType"]), (int)row["ColumnSize"], ParameterDirection.Input,
                        (bool)row["AllowDBNull"], precision, scale, row["ColumnName"].ToString(),
                        DataRowVersion.Original, (object)null);
                    cmd.Parameters.Add(adsParameter);
                    stringBuilder.Append("(" + this.mstrPrefix + row["BaseColumnName"].ToString() + this.mstrSuffix +
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
                    this.GetInsertCommand();
                    args.Command = this.mInsertCmd;
                    break;
                case StatementType.Update:
                    this.GetUpdateCommand();
                    args.Command = this.mUpdateCmd;
                    break;
                case StatementType.Delete:
                    this.GetDeleteCommand();
                    args.Command = this.mDeleteCmd;
                    break;
            }

            DataTableMapping tableMapping = args.TableMapping;
            foreach (AdsParameter parameter in (DbParameterCollection)args.Command.Parameters)
                parameter.Value = args.Row[this.MapColumnName(tableMapping, parameter.SourceColumn),
                    parameter.SourceVersion];
        }

        private string MapColumnName(DataTableMapping dtm, string strName)
        {
            if (dtm == null || dtm.ColumnMappings.Count == 0 || !dtm.ColumnMappings.Contains(strName))
                return strName;
            DataColumnMapping columnMapping = dtm.ColumnMappings[strName];
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
            this.mSchemaTable = (DataTable)null;
            this.mInsertCmd = (AdsCommand)null;
            this.mUpdateCmd = (AdsCommand)null;
            this.mDeleteCmd = (AdsCommand)null;
        }

        public static void DeriveParameters(AdsCommand command)
        {
            if (command.CommandType != CommandType.StoredProcedure)
                throw new InvalidOperationException(string.Format(
                    "DeriveParameters only supports CommandType.StoredProcedure, not CommandType.{0}",
                    (object)command.CommandType));
            command.DeriveParameters();
        }

        protected override void ApplyParameterInfo(
            DbParameter parameter,
            DataRow datarow,
            StatementType statementType,
            bool whereClause)
        {
            AdsParameter adsParameter = (AdsParameter)parameter;
            object iAceType = datarow[SchemaTableColumn.ProviderType];
            adsParameter.DbType = this.GetDbType((int)iAceType);
            object obj1 = datarow[SchemaTableColumn.NumericPrecision];
            if (DBNull.Value != obj1)
            {
                byte num = (byte)(short)obj1;
                adsParameter.Precision = num != byte.MaxValue ? num : (byte)0;
            }

            object obj2 = datarow[SchemaTableColumn.NumericScale];
            if (DBNull.Value == obj2)
                return;
            byte num1 = (byte)(short)obj2;
            if (num1 == byte.MaxValue)
                adsParameter.Scale = (byte)0;
            else
                adsParameter.Scale = num1;
        }

        protected override string GetParameterName(int parameterOrdinal)
        {
            return ":p" + parameterOrdinal.ToString((IFormatProvider)CultureInfo.InvariantCulture);
        }

        protected override string GetParameterName(string parameterName) => ":" + parameterName;

        protected override string GetParameterPlaceholder(int parameterOrdinal)
        {
            return ":p" + parameterOrdinal.ToString((IFormatProvider)CultureInfo.InvariantCulture);
        }

        protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
        {
            if (adapter == base.DataAdapter)
                ((AdsDataAdapter)adapter).RowUpdating -= new AdsRowUpdatingEventHandler(this.OnRowUpdating);
            else
                ((AdsDataAdapter)adapter).RowUpdating += new AdsRowUpdatingEventHandler(this.OnRowUpdating);
        }

        public AdsCommand GetDeleteCommand()
        {
            if (this.mSchemaTable == null)
                this.GetSchema();
            if (this.mDeleteCmd == null)
                this.GetDelete();
            return this.mDeleteCmd;
        }

        public AdsCommand GetInsertCommand()
        {
            if (this.mSchemaTable == null)
                this.GetSchema();
            if (this.mInsertCmd == null)
                this.GetInsert();
            return this.mInsertCmd;
        }

        private string GetRefreshSelect()
        {
            bool flag = true;
            StringBuilder stringBuilder = new StringBuilder();
            if (this.mSchemaTable == null)
                this.GetSchema();
            stringBuilder.Append("SELECT ");
            foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
            {
                if (flag)
                    flag = false;
                else
                    stringBuilder.Append(", ");
                if (row["BaseColumnName"].ToString().ToUpper().Equals(row["ColumnName"].ToString().ToUpper()))
                    stringBuilder.Append(this.mstrPrefix + row["BaseColumnName"].ToString() + this.mstrSuffix);
                else
                    stringBuilder.Append(this.mstrPrefix + row["BaseColumnName"].ToString() + this.mstrSuffix + " " +
                                         this.mstrPrefix + row["ColumnName"].ToString() + this.mstrSuffix);
            }

            stringBuilder.Append(" FROM " + this.QualifiedTableName());
            return stringBuilder.ToString();
        }

        private string GetRefreshWhereClause(AdsCommand cmd)
        {
            bool flag1 = true;
            bool flag2 = false;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
            {
                if ((row["ProviderType"].Equals((object)21) || row["ProviderType"].Equals((object)22)) &&
                    row["IsKey"].Equals((object)true))
                {
                    flag2 = true;
                    break;
                }
            }

            foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
            {
                if (!row["ProviderType"].Equals((object)21) && !row["ProviderType"].Equals((object)22) &&
                    !row["IsLong"].Equals((object)true) && (row["IsKey"].Equals((object)true) && !flag2 ||
                                                            row["IsUnique"].Equals((object)true)))
                {
                    AdsParameter adsParameter = (AdsParameter)null;
                    foreach (AdsParameter parameter in (DbParameterCollection)cmd.Parameters)
                    {
                        if (parameter.SourceColumn.Equals(row["ColumnName"]) &&
                            (parameter.SourceVersion == DataRowVersion.Current ||
                             row["ProviderType"].Equals((object)15)))
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
                        stringBuilder.Append("(" + this.mstrPrefix + row["BaseColumnName"].ToString() +
                                             this.mstrSuffix + " = :" + adsParameter.ParameterName + ")");
                    }
                }
            }

            if (stringBuilder.Length == 0)
            {
                bool flag3 = true;
                foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
                {
                    if (!row["ProviderType"].Equals((object)21) && !row["ProviderType"].Equals((object)22) &&
                        !row["IsLong"].Equals((object)true))
                    {
                        AdsParameter adsParameter = (AdsParameter)null;
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
                            stringBuilder.Append("(" + this.mstrPrefix + row["BaseColumnName"].ToString() +
                                                 this.mstrSuffix + " = :" + adsParameter.ParameterName + ")");
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
            StringBuilder stringBuilder = new StringBuilder();
            this.GetInsertCommand();
            stringBuilder.Append(this.GetRefreshSelect());
            stringBuilder.Append(" WHERE ");
            foreach (DataRow row in (InternalDataCollectionBase)this.mSchemaTable.Rows)
            {
                if (row["ProviderType"].Equals((object)15))
                {
                    stringBuilder.Append("(" + this.mstrPrefix + row["BaseColumnName"].ToString() + this.mstrSuffix +
                                         " = LASTAUTOINC(statement))");
                    return stringBuilder.ToString();
                }
            }

            string refreshWhereClause = this.GetRefreshWhereClause(this.mInsertCmd);
            stringBuilder.Append(refreshWhereClause);
            return stringBuilder.ToString();
        }

        public AdsCommand GetUpdateCommand()
        {
            if (this.mSchemaTable == null)
                this.GetSchema();
            if (this.mUpdateCmd == null)
                this.GetUpdate();
            return this.mUpdateCmd;
        }

        public string GetUpdateRefreshStatement()
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.GetUpdateCommand();
            stringBuilder.Append(this.GetRefreshSelect());
            stringBuilder.Append(" WHERE ");
            string refreshWhereClause = this.GetRefreshWhereClause(this.mUpdateCmd);
            stringBuilder.Append(refreshWhereClause);
            return stringBuilder.ToString();
        }

        [Description("The DataAdapter for which to automatically generate AdsCommands.")]
        public AdsDataAdapter DataAdapter
        {
            get => (AdsDataAdapter)base.DataAdapter;
            set
            {
                this.RefreshSchema();
                this.DataAdapter = (DbDataAdapter)value;
            }
        }

        [Browsable(false)]
        public override string QuotePrefix
        {
            get => this.mstrPrefix;
            set
            {
                if (this.mSchemaTable != null)
                    throw new InvalidOperationException(
                        "QuotePrefix cannot be modified after commands have been generated.");
                this.mstrPrefix = !(value != "[") || !(value != "\"")
                    ? value
                    : throw new ArgumentException(
                        "The acceptable values for the property 'QuotePrefix' are '[' or '\"'.");
            }
        }

        [Browsable(false)]
        public override string QuoteSuffix
        {
            get => this.mstrSuffix;
            set
            {
                if (this.mSchemaTable != null)
                    throw new InvalidOperationException(
                        "QuoteSuffix cannot be modified after commands have been generated.");
                this.mstrSuffix = !(value != "]") || !(value != "\"")
                    ? value
                    : throw new ArgumentException(
                        "The acceptable values for the property 'QuoteSuffix' are ']' or '\"'.");
            }
        }

        [Browsable(false)]
        public bool UsePKOnlyInWhereClause
        {
            get => this.mbPKOnlyInWhere;
            set
            {
                this.mbPKOnlyInWhere = !value || this.mbRequirePK
                    ? value
                    : throw new InvalidOperationException(
                        "The UsePKOnlyInWhereClause property cannot be true when RequirePrimaryKey is set to false.");
            }
        }

        [Browsable(false)]
        public bool RequirePrimaryKey
        {
            get => this.mbRequirePK;
            set
            {
                this.mbRequirePK = value || !this.mbPKOnlyInWhere
                    ? value
                    : throw new InvalidOperationException(
                        "The RequirePrimaryKey property cannot be false when UsePKOnlyInWhereClause is set to true.");
            }
        }

        [Browsable(false)]
        public bool UseRowversionOnlyInWhereClause
        {
            get => this.mbUseOnlyRowversionInWhere;
            set
            {
                this.mbUseOnlyRowversionInWhere = !value || this.mbRequirePK
                    ? value
                    : throw new InvalidOperationException(
                        "The UseOnlyRowversion property cannot be true when RequirePrimaryKey is set to false.");
            }
        }

        protected override DataTable GetSchemaTable(DbCommand srcCommand)
        {
            AdsDataReader adsDataReader =
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
            if (this.mstrPrefix == "\"" && this.mstrSuffix != "\"" || this.mstrPrefix == "[" && this.mstrSuffix != "]")
                throw new ArgumentException("The QuotePrefix and QuoteSuffix properties are not consistent.");
        }

        public override string QuoteIdentifier(string unquotedIdentifier)
        {
            if (unquotedIdentifier == null)
                throw new ArgumentException();
            this.CheckQuoteConsistency();
            return this.mstrPrefix + unquotedIdentifier + this.mstrSuffix;
        }

        public override string UnquoteIdentifier(string quotedIdentifier)
        {
            if (quotedIdentifier == null)
                throw new ArgumentException();
            this.CheckQuoteConsistency();
            return !quotedIdentifier.StartsWith(this.mstrPrefix) || !quotedIdentifier.EndsWith(this.mstrSuffix)
                ? quotedIdentifier
                : quotedIdentifier.Substring(this.mstrPrefix.Length,
                    quotedIdentifier.Length - (this.mstrPrefix.Length + this.mstrSuffix.Length));
        }
    }
}