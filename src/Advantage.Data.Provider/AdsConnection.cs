using AdvantageClientEngine;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Transactions;

namespace Advantage.Data.Provider
{
    [DesignerCategory("Form")]
    [ToolboxBitmap(typeof(AdsConnection), "adsconn.bmp")]
    [Designer("Advantage.Data.Provider.AdsConnectionDesigner, Advantage.Designer")]
    public sealed class AdsConnection : DbConnection, ICloneable, IDbConnection, IDisposable
    {
        private const int restCatalog = 0;
        private const int restSchema = 1;
        private const int restTable = 2;
        private const int restColumn = 3;
        private const int restIndex = 3;
        private const int restIndexCol = 4;
        private const int restProcedure = 2;
        private const int restProcParam = 3;
        private const string strSchemaPH = "::this";
        private ConnectionState mState;
        private AdsInternalConnection mInternalConnection;
        private AdsConnectionPool mPool;
        private string mstrConnString = "";
        private string mstrServerDatabase;
        private string mstrServerVersion;
        private bool mbBusy;
        private bool mbDisposed;
        private static AdsPoolManager mPoolMgr = new AdsPoolManager();
        private ArrayList mProxyHandleList = new ArrayList();
        private bool mbOverrideTableType;
        private ushort musTableTypeOverride;
        public bool mbEntityTablesGenerated;

        public event AdsInfoMessageEventHandler InfoMessage;

        public AdsConnection()
        {
        }

        public AdsConnection(string sConnString) => this.mstrConnString = sConnString;

        protected override DbProviderFactory DbProviderFactory
        {
            get => (DbProviderFactory)AdsFactory.Instance;
        }

        protected override void Dispose(bool bDisposing)
        {
            if (this.mbDisposed)
                return;
            lock (this)
            {
                if (this.mbDisposed)
                    return;
                if (this.State != ConnectionState.Closed && bDisposing)
                    this.Close();
                if (bDisposing && this.mInternalConnection != null)
                    this.mInternalConnection.Dispose(bDisposing);
                base.Dispose(bDisposing);
                this.mbDisposed = true;
            }
        }

        internal void AddProxyHandle(AdsProxyHandle hProxy)
        {
            lock (this.mProxyHandleList.SyncRoot)
                this.mProxyHandleList.Add((object)hProxy);
        }

        internal void RemoveProxyHandle(AdsProxyHandle hProxy)
        {
            lock (this.mProxyHandleList.SyncRoot)
                this.mProxyHandleList.Remove((object)hProxy);
        }

        private void RemoveAllProxyHandles()
        {
            lock (this.mProxyHandleList.SyncRoot)
            {
                foreach (AdsProxyHandle mProxyHandle in this.mProxyHandleList)
                    mProxyHandle.mhHandle = IntPtr.Zero;
                this.mProxyHandleList.Clear();
            }
        }

        private void OnInfoMessage(AdsInfoMessageEventArgs eventArgs)
        {
            AdsInfoMessageEventHandler infoMessage = this.InfoMessage;
            if (infoMessage == null)
                return;
            infoMessage((object)this, eventArgs);
        }

        internal void FireStateChangeEvent(ConnectionState origState, ConnectionState currState)
        {
            if (origState == currState)
                return;
            this.OnStateChange(new StateChangeEventArgs(origState, currState));
        }

        internal void FireWarning(int iWarning, string strMessage)
        {
            this.OnInfoMessage(new AdsInfoMessageEventArgs(iWarning, strMessage));
        }

        public new AdsTransaction BeginTransaction()
        {
            if (this.mState == ConnectionState.Closed)
                throw new InvalidOperationException("Invalid operation. The connection is closed.");
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            return new AdsTransaction(this);
        }

        IDbTransaction IDbConnection.BeginTransaction() => (IDbTransaction)this.BeginTransaction();

        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            return (DbTransaction)this.BeginTransaction(isolationLevel);
        }

        public new AdsTransaction BeginTransaction(System.Data.IsolationLevel level)
        {
            if (this.mState == ConnectionState.Closed)
                throw new InvalidOperationException("Invalid operation. The connection is closed.");
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            if (level == System.Data.IsolationLevel.Unspecified)
                level = System.Data.IsolationLevel.ReadCommitted;
            if (level != System.Data.IsolationLevel.ReadCommitted)
                throw new NotSupportedException("Isolation level must be ReadCommitted.");
            return new AdsTransaction(this);
        }

        IDbTransaction IDbConnection.BeginTransaction(System.Data.IsolationLevel level)
        {
            return (IDbTransaction)this.BeginTransaction(level);
        }

        public override void ChangeDatabase(string dbName)
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            throw new NotSupportedException("ChangeDatabase is not supported.");
        }

        public override void Open()
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            if (this.mstrConnString == "")
                throw new InvalidOperationException("The ConnectionString property has not been initialized.");
            if (this.mState != ConnectionState.Closed)
                throw new InvalidOperationException("The Connection is already opened.");
            AdsConnection.mPoolMgr.GetConnection(this.mstrConnString, out this.mInternalConnection, out this.mPool);
            if (this.mInternalConnection == null)
                throw new InvalidOperationException(
                    "Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.");
            if (this.mInternalConnection.State != ConnectionState.Open)
                this.mInternalConnection.Connect();
            ConnectionState mState = this.mState;
            this.mState = this.mInternalConnection.State;
            if (this.mInternalConnection.TransScopeEnlist && Transaction.Current != (Transaction)null)
                this.mInternalConnection.EnlistTransaction(Transaction.Current, this);
            this.FireStateChangeEvent(mState, this.mState);
        }

        public override void Close()
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            ConnectionState mState = this.mState;
            this.mState = ConnectionState.Closed;
            this.RemoveAllProxyHandles();
            if (this.mInternalConnection != null)
            {
                if (this.mInternalConnection.CurrentTransaction != null)
                    this.mInternalConnection.DisposeOnCommit = true;
                else if (this.mInternalConnection is AdsPooledInternalConnection)
                    AdsPooledInternalConnection.ReturnConnectionToPool(
                        this.mInternalConnection as AdsPooledInternalConnection);
                else
                    this.mInternalConnection.Dispose();
            }

            this.mInternalConnection = (AdsInternalConnection)null;
            this.mstrServerDatabase = (string)null;
            this.FireStateChangeEvent(mState, this.mState);
        }

        IDbCommand IDbConnection.CreateCommand() => (IDbCommand)new AdsCommand("", this);

        protected override DbCommand CreateDbCommand() => (DbCommand)this.CreateCommand();

        public new AdsCommand CreateCommand() => new AdsCommand("", this);

        public static void FlushConnectionPool() => AdsConnection.mPoolMgr.FlushConnections();

        public static void FlushConnectionPool(string sConnString)
        {
            AdsConnection.mPoolMgr.FlushConnections(sConnString);
        }

        public object[] GetDDObjects(AdsConnection.AdsObjectType type, string strParent)
        {
            if (this.mState == ConnectionState.Closed)
                throw new InvalidOperationException("The connection is not open.");
            ArrayList arrayList = new ArrayList();
            ushort usFindObjectType;
            switch (type)
            {
                case AdsConnection.AdsObjectType.TableObject:
                    usFindObjectType = (ushort)1;
                    break;
                case AdsConnection.AdsObjectType.ViewObject:
                    usFindObjectType = (ushort)6;
                    break;
                case AdsConnection.AdsObjectType.RelationObject:
                    usFindObjectType = (ushort)2;
                    break;
                case AdsConnection.AdsObjectType.IndexFileObject:
                    usFindObjectType = (ushort)3;
                    break;
                case AdsConnection.AdsObjectType.IndexObject:
                    usFindObjectType = (ushort)5;
                    break;
                case AdsConnection.AdsObjectType.FieldObject:
                    usFindObjectType = (ushort)4;
                    break;
                case AdsConnection.AdsObjectType.UserObject:
                    usFindObjectType = (ushort)8;
                    break;
                case AdsConnection.AdsObjectType.ProcedureObject:
                    usFindObjectType = (ushort)10;
                    break;
                case AdsConnection.AdsObjectType.LinkObject:
                    usFindObjectType = (ushort)12;
                    break;
                default:
                    usFindObjectType = (ushort)0;
                    break;
            }

            ushort pusObjectNameLen = 201;
            char[] pucObjectName = new char[201];
            IntPtr phFindHandle;
            uint ulRet;
            for (ulRet = ACE.AdsDDFindFirstObject(this.mInternalConnection.Handle, usFindObjectType, strParent,
                     pucObjectName, ref pusObjectNameLen, out phFindHandle);
                 ulRet == 0U;
                 ulRet = ACE.AdsDDFindNextObject(this.mInternalConnection.Handle, phFindHandle, pucObjectName,
                     ref pusObjectNameLen))
            {
                arrayList.Add((object)new string(pucObjectName, 0, (int)pusObjectNameLen - 1));
                pusObjectNameLen = (ushort)201;
            }

            int close = (int)ACE.AdsDDFindClose(this.mInternalConnection.Handle, phFindHandle);
            if (ulRet != 5137U)
                AdsException.CheckACE(ulRet);
            return arrayList.ToArray();
        }

        public object[] GetTableNames()
        {
            string strMask = "";
            AdsConnectionStringHandler connectionStringHandler = new AdsConnectionStringHandler();
            connectionStringHandler.ParseConnectionString(this.mstrConnString);
            string dataSource = connectionStringHandler.DataSource;
            bool flag = false;
            if (dataSource.EndsWith(".add") || dataSource.EndsWith(".ADD"))
                flag = true;
            if (!flag)
            {
                string str = dataSource;
                if (!str.EndsWith("\\"))
                    str += "\\";
                strMask = connectionStringHandler.TableType != (ushort)3 ? str + "*.dbf" : str + "*.adt";
            }

            return this.InternalGetTableNames(strMask, true, true);
        }

        public object[] GetTableNames(string strMask)
        {
            return this.InternalGetTableNames(strMask, false, false);
        }

        public object[] GetTableNames(bool bIncludeLinkNames)
        {
            return this.InternalGetTableNames((string)null, bIncludeLinkNames, false);
        }

        private object[] InternalGetTableNames(
            string strMask,
            bool bGetLinkNames,
            bool bStripExtensions)
        {
            if (this.mState == ConnectionState.Closed)
                throw new InvalidOperationException("The connection is not open.");
            ArrayList arrayList = new ArrayList();
            if (this.mInternalConnection != null)
            {
                ushort pusFileLen = 256;
                char[] chArray1 = new char[(int)pusFileLen];
                ushort pusDDLen = pusFileLen;
                char[] chArray2 = new char[(int)pusDDLen];
                IntPtr plHandle;
                uint ulRet = ACE.AdsFindFirstTable62(this.mInternalConnection.Handle, strMask, chArray2, ref pusDDLen,
                    chArray1, ref pusFileLen, out plHandle);
                bool isDictionaryConn = this.IsDictionaryConn;
                for (;
                     ulRet == 0U;
                     ulRet = ACE.AdsFindNextTable62(this.mInternalConnection.Handle, plHandle, chArray2, ref pusDDLen,
                         chArray1, ref pusFileLen))
                {
                    if (!isDictionaryConn && bStripExtensions)
                        pusFileLen -= (ushort)4;
                    if (bGetLinkNames && pusDDLen > (ushort)0)
                    {
                        string str = new string(chArray2, 0, (int)pusDDLen) + "::" +
                                     new string(chArray1, 0, (int)pusFileLen);
                        arrayList.Add((object)str);
                    }
                    else
                        arrayList.Add((object)new string(chArray1, 0, (int)pusFileLen));

                    pusFileLen = (ushort)256;
                    pusDDLen = pusFileLen;
                }

                int close = (int)ACE.AdsFindClose(this.mInternalConnection.Handle, plHandle);
                if (ulRet != 5059U)
                    AdsException.CheckACE(ulRet);
            }

            return arrayList.ToArray();
        }

        public void CloseCachedTables()
        {
            AdsException.CheckACE(ACE.AdsCloseCachedTables(this.mInternalConnection.Handle));
        }

        public static void SetTableCache(int iNumTables)
        {
            if (iNumTables < 0)
                throw new ArgumentException("TableCache value cannot be negative.");
            AdsException.CheckACE(ACE.AdsCacheOpenTables((ushort)iNumTables));
        }

        public static void SetCursorCache(int iNumCursors)
        {
            if (iNumCursors < 0)
                throw new ArgumentException("CursorCache value cannot be negative.");
            AdsException.CheckACE(ACE.AdsCacheOpenCursors((ushort)iNumCursors));
        }

        public override DataTable GetSchema()
        {
            return this.GetSchema(DbMetaDataCollectionNames.MetaDataCollections, (string[])null);
        }

        public override DataTable GetSchema(string collectionName)
        {
            return this.GetSchema(collectionName, (string[])null);
        }

        private string ArrayToString(object[] array)
        {
            if (array == null)
                return "[]";
            StringBuilder stringBuilder = new StringBuilder("[");
            for (int index = 0; index < array.Length; ++index)
            {
                if (index > 0)
                    stringBuilder.Append(", ");
                if (array[index] == null)
                    stringBuilder.Append("null");
                else
                    stringBuilder.Append(array[index].ToString());
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        public override DataTable GetSchema(string collectionName, string[] restrictions)
        {
            if (collectionName.Equals("DataSourceInformation", StringComparison.InvariantCultureIgnoreCase))
                return this.GetDataSourceInformation();
            if (collectionName.Equals("Tables", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaTables(restrictions);
            if (collectionName.Equals("Indexes", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaIndexes(restrictions);
            if (collectionName.Equals("IndexColumns", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaIndexColumns(restrictions);
            if (collectionName.Equals("Views", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaViews(restrictions);
            if (collectionName.Equals("Columns", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaColumnsOrig(restrictions);
            if (collectionName.Equals("ReservedWords", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaReservedWords(restrictions);
            if (collectionName.Equals("DataTypes", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaDataTypes(restrictions);
            if (collectionName.Equals("StoredProcedures", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaProcedures(restrictions);
            if (collectionName.Equals("StoredProcedureParameters", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaProcedureParameters((short)1, restrictions);
            if (collectionName.Equals("StoredProcedureColumns", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaProcedureParameters((short)3, restrictions);
            if (collectionName.Equals("ForeignKeys", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaForeignKeys(false, restrictions);
            if (collectionName.Equals("ForeignKeyColumns", StringComparison.InvariantCultureIgnoreCase))
                return this.GetSchemaForeignKeys(true, restrictions);
            throw new NotImplementedException(string.Format("GetSchema( {0} )", (object)collectionName));
        }

        private DataTable GetSchemaIndexes(string[] restrictions)
        {
            if (restrictions == null || restrictions.Length != 4 || restrictions[2] == null)
                throw new NotImplementedException("Cannot enumerate all indexes in all tables.");
            DataTable schemaIndexes = new DataTable("Indexes");
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.IndexSchema.xml"))
                schemaIndexes.ReadXmlSchema(manifestResourceStream);
            DataTable dataTable1;
            try
            {
                if (this.RestrictionsHasLink(restrictions))
                    dataTable1 = this.ExecuteDataTable("execute procedure sp_GetPrimaryKeys( ?, null, ? )",
                        (object)restrictions[1], (object)restrictions[2]);
                else
                    dataTable1 = this.ExecuteDataTable("execute procedure sp_GetPrimaryKeys( null, null, ? )",
                        (object)restrictions[2]);
            }
            catch
            {
                return schemaIndexes;
            }

            string str1 = "";
            if (dataTable1.Rows.Count > 0)
                str1 = (string)dataTable1.Rows[0]["PK_NAME"];
            DataTable dataTable2;
            if (this.RestrictionsHasLink(restrictions))
                dataTable2 = this.ExecuteDataTable("execute procedure sp_GetIndexInfo( ?, null, ?, false, false )",
                    (object)restrictions[1], (object)restrictions[2]);
            else
                dataTable2 = this.ExecuteDataTable("execute procedure sp_GetIndexInfo( null, null, ?, false, false )",
                    (object)restrictions[2]);
            string str2 = "";
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable2.Rows)
            {
                if (row1["COLUMN_NAME"] != DBNull.Value && !((string)row1["COLUMN_NAME"] == "") &&
                    str2 != (string)row1["INDEX_NAME"])
                {
                    str2 = (string)row1["INDEX_NAME"];
                    DataRow row2 = schemaIndexes.NewRow();
                    row2["catalog"] = (object)restrictions[0];
                    row2["schema"] = (object)restrictions[1];
                    row2["table"] = row1["TABLE_NAME"];
                    row2["name"] = row1["INDEX_NAME"];
                    row2["isunique"] = (bool)row1["NON_UNIQUE"] ? (object)false : (object)true;
                    row2["isprimary"] = !(str1 == (string)row1["INDEX_NAME"]) ? (object)false : (object)true;
                    schemaIndexes.Rows.Add(row2);
                }
            }

            schemaIndexes.AcceptChanges();
            return schemaIndexes;
        }

        private DataTable GetSchemaIndexColumns(string[] restrictions)
        {
            if (restrictions == null || restrictions.Length < 4 || restrictions[2] == null || restrictions[3] == null)
                throw new NotImplementedException("Cannot enumerate index columns in multiple tables or indexes.");
            DataTable schemaIndexColumns = new DataTable("IndexColumns");
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.IndexColumnsSchema.xml"))
                schemaIndexColumns.ReadXmlSchema(manifestResourceStream);
            DataTable dataTable;
            if (this.RestrictionsHasLink(restrictions))
                dataTable = this.ExecuteDataTable("execute procedure sp_GetIndexInfo( ?, null, ?, false, false )",
                    (object)restrictions[1], (object)restrictions[2]);
            else
                dataTable = this.ExecuteDataTable("execute procedure sp_GetIndexInfo( null, null, ?, false, false )",
                    (object)restrictions[2]);
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (restrictions[3].Equals((string)row1["INDEX_NAME"], StringComparison.InvariantCultureIgnoreCase))
                {
                    DataRow row2 = schemaIndexColumns.NewRow();
                    row2["catalog"] = (object)restrictions[0];
                    row2["schema"] = (object)restrictions[1];
                    row2["table"] = (object)restrictions[2];
                    row2["name"] = row1["COLUMN_NAME"];
                    row2["ordinal"] = row1["ORDINAL_POSITION"];
                    row2["index"] = row1["INDEX_NAME"];
                    schemaIndexColumns.Rows.Add(row2);
                }
            }

            schemaIndexColumns.AcceptChanges();
            return schemaIndexColumns;
        }

        private bool IsSearchableColumn(int iACEType)
        {
            switch (iACEType)
            {
                case 5:
                case 6:
                case 7:
                case 8:
                case 16:
                case 24:
                case 28:
                    return false;
                default:
                    return true;
            }
        }

        private DataTable GetSchemaDataTypes(string[] restrictions)
        {
            DataTable schemaDataTypes = new DataTable("DataTypes");
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.DataTypes.xml"))
            {
                int num = (int)schemaDataTypes.ReadXml(manifestResourceStream);
            }

            return schemaDataTypes;
        }

        private DataTable GetSchemaReservedWords(string[] restrictions)
        {
            DataTable schemaReservedWords = this.ExecuteDataTable("execute procedure sp_getSQLKeywords()");
            schemaReservedWords.Columns[0].ColumnName = "ReservedWord";
            return schemaReservedWords;
        }

        private bool RestrictionsHasLink(string[] restrictions)
        {
            return restrictions.Length > 1 && restrictions[1] != null &&
                   !restrictions[1].Equals("::this", StringComparison.InvariantCultureIgnoreCase);
        }

        private DataTable GetSchemaProcedures(string[] restrictions)
        {
            string str1 = (string)null;
            string str2 = (string)null;
            string str3 = (string)null;
            if (restrictions != null)
            {
                if (restrictions.Length >= 1)
                    str1 = restrictions[0];
                if (restrictions.Length >= 2)
                    str2 = restrictions[1];
                if (restrictions.Length >= 3)
                    str3 = restrictions[2];
            }

            if (str2 != null && str2.Equals("::this", StringComparison.InvariantCultureIgnoreCase))
                str2 = (string)null;
            DataTable schemaProcedures = this.ExecuteDataTable("execute procedure sp_GetProcedures(?, ?, ?)",
                (object)str1, (object)str2, (object)str3);
            schemaProcedures.Columns["PROCEDURE_SCHEM"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)schemaProcedures.Rows)
                row["PROCEDURE_SCHEM"] = (object)"::this";
            schemaProcedures.AcceptChanges();
            return schemaProcedures;
        }

        private DataTable GetSchemaProcedureParameters(short iColType, string[] restrictions)
        {
            string str1 = (string)null;
            string str2 = (string)null;
            string str3 = (string)null;
            string str4 = (string)null;
            if (restrictions != null)
            {
                if (restrictions.Length >= 1)
                    str1 = restrictions[0];
                if (restrictions.Length >= 2)
                    str2 = restrictions[1];
                if (restrictions.Length >= 3)
                    str3 = restrictions[2];
                if (restrictions.Length >= 4)
                    str4 = restrictions[3];
            }

            if (str2 != null && str2.Equals("::this", StringComparison.InvariantCultureIgnoreCase))
                str2 = (string)null;
            DataTable procedureParameters = this.ExecuteDataTable(
                "execute procedure sp_GetProcedureColumns(?, ?, ?, ?)", (object)str1, (object)str2, (object)str3,
                (object)str4);
            procedureParameters.Columns.Add("Column_Ordinal", Type.GetType("System.Int32"));
            int num = 0;
            procedureParameters.Columns.Add("IsOutputParameter", Type.GetType("System.Boolean"));
            procedureParameters.Columns.Add("Direction", Type.GetType("System.String"));
            procedureParameters.Columns["PROCEDURE_SCHEM"].ReadOnly = false;
            procedureParameters.Columns["TYPE_NAME"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)procedureParameters.Rows)
            {
                if ((int)(short)row["Column_Type"] != (int)iColType)
                {
                    row.Delete();
                }
                else
                {
                    if (restrictions.Length > 1)
                        row["Procedure_Schem"] = (object)restrictions[1];
                    row["Column_Ordinal"] = (object)num++;
                    ushort type = AdsDataReader.ConvertACETypeNameToType((string)row["Type_Name"]);
                    row["Type_Name"] = (object)AdsDataReader.ConvertACETypeToName(type);
                    if (iColType == (short)1)
                    {
                        row["IsOutputParameter"] = (object)false;
                        row["Direction"] = (object)"IN";
                    }
                    else
                    {
                        row["IsOutputParameter"] = (object)true;
                        row["Direction"] = (object)"OUT";
                    }
                }
            }

            procedureParameters.AcceptChanges();
            return procedureParameters;
        }

        private DataTable GetSchemaForeignKeys(bool bIncludeAllColumns, string[] restrictions)
        {
            string str1 = (string)null;
            string str2 = (string)null;
            string str3 = (string)null;
            string str4 = (string)null;
            string str5 = (string)null;
            if (restrictions != null)
            {
                if (restrictions.Length >= 1)
                    str1 = restrictions[0];
                if (restrictions.Length >= 2)
                    str2 = restrictions[1];
                if (restrictions.Length >= 3)
                    str3 = restrictions[2];
                if (restrictions.Length >= 4)
                    str4 = restrictions[3];
                if (restrictions.Length >= 5)
                    str5 = restrictions[4];
            }

            if (str2 != null && str2.Equals("::this", StringComparison.InvariantCultureIgnoreCase))
                str2 = (string)null;
            DataTable schemaForeignKeys = this.ExecuteDataTable(
                "EXECUTE PROCEDURE sp_GetForeignKeyColumns( ?, ?, ?, ? )", (object)str1, (object)str2, (object)str3,
                (object)str4);
            schemaForeignKeys.Columns["PKTABLE_SCHEM"].ReadOnly = false;
            schemaForeignKeys.Columns["FKTABLE_SCHEM"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)schemaForeignKeys.Rows)
            {
                short num = (short)row["KEY_SEQ"];
                if (!bIncludeAllColumns && num != (short)0)
                    row.Delete();
                else if (str5 != null && !restrictions[4].Equals((string)row["FKCOLUMN_NAME"],
                             StringComparison.InvariantCultureIgnoreCase))
                {
                    row.Delete();
                }
                else
                {
                    row["PKTABLE_SCHEM"] = (object)"::this";
                    row["FKTABLE_SCHEM"] = (object)"::this";
                }
            }

            schemaForeignKeys.Columns["FKTABLE_CAT"].ColumnName = "Database";
            schemaForeignKeys.Columns["FKTABLE_SCHEM"].ColumnName = "Schema";
            schemaForeignKeys.Columns["FKTABLE_NAME"].ColumnName = "Table";
            schemaForeignKeys.Columns["PKTABLE_SCHEM"].ColumnName = "ReferencedTableSchema";
            schemaForeignKeys.Columns["PKTABLE_NAME"].ColumnName = "ReferencedTableName";
            schemaForeignKeys.Columns["UPDATE_RULE"].ColumnName = "UpdateAction";
            schemaForeignKeys.Columns["DELETE_RULE"].ColumnName = "DeleteAction";
            if (bIncludeAllColumns)
            {
                schemaForeignKeys.Columns["NAME"].ColumnName = "ForeignKey";
                schemaForeignKeys.Columns["FKCOLUMN_NAME"].ColumnName = "Name";
            }
            else
                schemaForeignKeys.Columns["NAME"].ColumnName = "Name";

            schemaForeignKeys.Columns["KEY_SEQ"].ColumnName = "Ordinal";
            schemaForeignKeys.Columns["PKCOLUMN_NAME"].ColumnName = "ReferencedColumnName";
            schemaForeignKeys.AcceptChanges();
            return schemaForeignKeys;
        }

        private DataTable GetSchemaColumnsOrig(string[] restrictions)
        {
            string str = (string)null;
            if (restrictions == null || restrictions.Length < 3 || restrictions[2] == null)
                throw new NotImplementedException("Cannot enumerate all columns in all tables.");
            if (restrictions.Length >= 4)
                str = restrictions[3];
            AdsCommand adsCommand = new AdsCommand();
            adsCommand.Connection = this;
            if (this.RestrictionsHasLink(restrictions))
                adsCommand.CommandText = ":" + restrictions[1] + "::" + restrictions[2];
            else
                adsCommand.CommandText = restrictions[2];
            adsCommand.CommandType = CommandType.TableDirect;
            AdsDataReader adsDataReader =
                adsCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
            DataTable schemaTable = adsDataReader.GetSchemaTable();
            adsDataReader.Close();
            adsCommand.Dispose();
            if (restrictions.Length > 0)
            {
                foreach (DataRow row in (InternalDataCollectionBase)schemaTable.Rows)
                {
                    if (str != null && !str.Equals((string)row["ColumnName"],
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        row.Delete();
                    }
                    else
                    {
                        row["BaseCatalogName"] = (object)restrictions[0];
                        row["BaseSchemaName"] = (object)restrictions[1];
                        row["BaseTableName"] = (object)restrictions[2];
                    }
                }
            }

            schemaTable.AcceptChanges();
            return schemaTable;
        }

        internal DataTable ExecuteDataTable(string strQuery, params object[] varargs)
        {
            DataTable dataTable;
            try
            {
                AdsCommand adsCommand = new AdsCommand(strQuery, this);
                for (int index = 0; index < varargs.Length; ++index)
                    adsCommand.Parameters.Add(index + 1, varargs[index]);
                AdsDataReader reader = adsCommand.ExecuteReader();
                dataTable = new DataTable();
                bool trimTrailingSpaces = this.TrimTrailingSpaces;
                this.mInternalConnection.TrimTrailingSpaces = true;
                dataTable.Load((IDataReader)reader);
                this.mInternalConnection.TrimTrailingSpaces = trimTrailingSpaces;
                reader.Close();
                adsCommand.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
        }

        private string DataTableToString(DataTable Table)
        {
            if (Table == null)
                return "[]";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            foreach (DataColumn column in (InternalDataCollectionBase)Table.Columns)
            {
                stringBuilder.Append(column.ColumnName);
                stringBuilder.Append(", ");
            }

            stringBuilder.Append("]\n");
            foreach (DataRow row in (InternalDataCollectionBase)Table.Rows)
            {
                stringBuilder.Append("[");
                for (int index = 0; index < row.ItemArray.Length; ++index)
                {
                    if (index > 0)
                        stringBuilder.Append(", ");
                    if (row.ItemArray[index] == null)
                        stringBuilder.Append("null");
                    else
                        stringBuilder.Append(row.ItemArray[index].ToString());
                }

                stringBuilder.Append("]\n");
            }

            return stringBuilder.ToString();
        }

        private DataTable GetSchemaTablesOrViews(string[] restrictions, string strType)
        {
            string str1 = (string)null;
            string str2 = (string)null;
            string str3 = (string)null;
            string strQuery = "execute procedure sp_gettables( ?, ?, ?, ? )";
            string database = this.Database;
            if (restrictions != null)
            {
                if (restrictions.Length >= 1)
                    str1 = restrictions[0];
                if (restrictions.Length >= 2)
                    str2 = restrictions[1];
                if (restrictions.Length >= 3)
                    str3 = restrictions[2];
            }

            if (str1 != null && !str1.Equals(this.Database, StringComparison.InvariantCultureIgnoreCase))
            {
                str1 = "empty;,result";
            }
            else
            {
                if (str1 != null && str1.Equals(this.Database, StringComparison.InvariantCultureIgnoreCase))
                    str1 = (string)null;
                if (str2 == "::this")
                    str2 = (string)null;
                if (str2 != null)
                {
                    str1 = str2;
                    str2 = (string)null;
                }
            }

            DataTable schemaTablesOrViews =
                this.ExecuteDataTable(strQuery, (object)str1, (object)str2, (object)str3, (object)strType);
            schemaTablesOrViews.Columns["TABLE_CAT"].ReadOnly = false;
            schemaTablesOrViews.Columns["TABLE_NAME"].ReadOnly = false;
            schemaTablesOrViews.Columns["TABLE_SCHEM"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)schemaTablesOrViews.Rows)
            {
                if (!this.IsDictionaryConn ||
                    database.Equals((string)row["TABLE_CAT"], StringComparison.InvariantCultureIgnoreCase))
                {
                    row["TABLE_SCHEM"] = (object)"::this";
                }
                else
                {
                    row["TABLE_SCHEM"] = row["TABLE_CAT"];
                    row["TABLE_CAT"] = (object)database;
                }

                string str4 = (string)row["TABLE_NAME"];
                if (str4.EndsWith(".adt", true, CultureInfo.InvariantCulture) ||
                    str4.EndsWith(".dbf", true, CultureInfo.InvariantCulture))
                {
                    string str5 = str4.Substring(0, str4.Length - 4);
                    row["TABLE_NAME"] = (object)str5;
                }
            }

            schemaTablesOrViews.AcceptChanges();
            return schemaTablesOrViews;
        }

        private DataTable GetSchemaTables(string[] restrictions)
        {
            return this.GetSchemaTablesOrViews(restrictions, "TABLE");
        }

        private DataTable GetSchemaViews(string[] restrictions)
        {
            return this.GetSchemaTablesOrViews(restrictions, "VIEW");
        }

        private DataTable GetDataSourceInformation()
        {
            DataTable sourceInformation = new DataTable("DataSourceInformation");
            using (Stream manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.DataSourceInformation.xml"))
            {
                int num = (int)sourceInformation.ReadXml(manifestResourceStream);
            }

            if (this.mState == ConnectionState.Open)
            {
                sourceInformation.Rows[0][DbMetaDataColumnNames.DataSourceProductVersion] = (object)this.ServerVersion;
                sourceInformation.Rows[0][DbMetaDataColumnNames.DataSourceProductVersionNormalized] =
                    (object)this.ServerVersion;
            }

            if (this.TableType != (ushort)3)
                sourceInformation.Rows[0][DbMetaDataColumnNames.IdentifierPattern] =
                    (object)
                    "(^[a-zA-Z_][a-zA-Z0-9_]*$)|(^\\[[a-zA-Z_][a-zA-Z0-9_]*\\]$)|(^\\\"[a-zA-Z_][a-zA-Z0-9_]*\\\"$)";
            sourceInformation.Rows[0][DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern] =
                (object)"(?<!\\[[^\\]]+)\\.";
            sourceInformation.AcceptChanges();
            return sourceInformation;
        }

        object ICloneable.Clone() => (object)new AdsConnection(this.ConnectionString);

        public string ServerName
        {
            get
            {
                if (this.mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                string serverName = "";
                if (this.mInternalConnection != null)
                {
                    ushort pusLen = 260;
                    char[] pucName = new char[(int)pusLen];
                    AdsException.CheckACE(ACE.AdsGetServerName(this.mInternalConnection.Handle, pucName, ref pusLen));
                    serverName = new string(pucName, 0, (int)pusLen);
                }

                return serverName;
            }
        }

        public DateTime ServerTime
        {
            get
            {
                ushort pusDateBufLen = 12;
                ushort pusTimeBufLen = 12;
                char[] chArray = new char[(int)pusDateBufLen];
                char[] pucTimeBuf = new char[(int)pusTimeBufLen];
                int plTime;
                AdsException.CheckACE(ACE.AdsGetServerTime(this.mInternalConnection.Handle, chArray, ref pusDateBufLen,
                    out plTime, pucTimeBuf, ref pusTimeBufLen));
                double pdJulian;
                AdsException.CheckACE(ACEUNPUB.AdsConvertDateToJulian(this.mInternalConnection.Handle,
                    new string(chArray, 0, (int)pusDateBufLen), pusDateBufLen, out pdJulian));
                ushort pusLen = 12;
                AdsException.CheckACE(ACEUNPUB.AdsConvertJulianToString(pdJulian, chArray, ref pusLen));
                string str1 = new string(chArray, 0, 4);
                string str2 = new string(chArray, 4, 2);
                string str3 = new string(chArray, 6, 2);
                DateTime serverTime = new DateTime(Convert.ToInt32(str1, 10), Convert.ToInt32(str2, 10),
                    Convert.ToInt32(str3, 10));
                serverTime = serverTime.AddMilliseconds((double)plTime);
                return serverTime;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public string DateFormat
        {
            get
            {
                if (this.mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                ushort pusLen = 12;
                char[] pucFormat = new char[(int)pusLen];
                AdsException.CheckACE(ACE.AdsGetDateFormat60(this.mInternalConnection.Handle, pucFormat, ref pusLen));
                return new string(pucFormat, 0, (int)pusLen);
            }
            set
            {
                if (this.mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                AdsException.CheckACE(ACE.AdsSetDateFormat60(this.mInternalConnection.Handle, value));
            }
        }

        public static short Epoch
        {
            get
            {
                ushort pusCentury;
                AdsException.CheckACE(ACE.AdsGetEpoch(out pusCentury));
                return (short)pusCentury;
            }
            set => AdsException.CheckACE(ACE.AdsSetEpoch((ushort)value));
        }

        public string ServerType
        {
            get
            {
                if (this.mState == ConnectionState.Closed)
                {
                    AdsConnectionStringHandler connectionStringHandler = new AdsConnectionStringHandler();
                    connectionStringHandler.ParseConnectionString(this.mstrConnString);
                    return connectionStringHandler.ServerType == null ? "REMOTE" : connectionStringHandler.ServerType;
                }

                ushort pusConnectType;
                AdsException.CheckACE(ACE.AdsGetConnectionType(this.mInternalConnection.Handle, out pusConnectType));
                switch (pusConnectType)
                {
                    case 1:
                        return "LOCAL";
                    case 2:
                        return "REMOTE";
                    case 4:
                        return "AIS";
                    default:
                        return "UNKNOWN";
                }
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int DDVersionMajor
        {
            get => this.DDGetDatabaseVersion((ushort)111);
            set
            {
                if (value < 0)
                    throw new ArgumentException("DDVersionMajor value cannot be negative.");
                this.DDSetDatabaseVersion((ushort)111, (uint)value);
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int DDVersionMinor
        {
            get => this.DDGetDatabaseVersion((ushort)112);
            set
            {
                if (value < 0)
                    throw new ArgumentException("DDVersionMinor value cannot be negative.");
                this.DDSetDatabaseVersion((ushort)112, (uint)value);
            }
        }

        private int DDGetDatabaseVersion(ushort usProperty)
        {
            if (!this.IsDictionaryConn)
                throw new InvalidOperationException(
                    "Invalid access to DDMajorVersion. Connection is not a Data Dictionary.");
            ushort pusPropertyLen = 2;
            ushort pusProperty = 0;
            AdsException.CheckACE(ACE.AdsDDGetDatabaseProperty(this.mInternalConnection.Handle, usProperty,
                ref pusProperty, ref pusPropertyLen));
            return (int)pusProperty;
        }

        private void DDSetDatabaseVersion(ushort usProperty, uint uiVer)
        {
            if (this.mState == ConnectionState.Closed)
                throw new InvalidOperationException("The connection is not open.");
            ushort pusProperty = (ushort)uiVer;
            AdsException.CheckACE(ACE.AdsDDSetDatabaseProperty(this.mInternalConnection.Handle, usProperty,
                ref pusProperty, (ushort)2));
        }

        public bool IsDictionaryConn
        {
            get
            {
                if (this.mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                return this.mInternalConnection.HandleType == (ushort)6;
            }
        }

        public bool IsConnectionAlive
        {
            get
            {
                if (this.mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                ushort pbConnectionIsAlive;
                return ACE.AdsIsConnectionAlive(this.mInternalConnection.Handle, out pbConnectionIsAlive) == 0U &&
                       pbConnectionIsAlive != (ushort)0;
            }
        }

        [Category("Data")]
        [Description("Information used to connect to a Data Source.")]
        [DefaultValue("")]
        public override string ConnectionString
        {
            get => this.mstrConnString;
            set
            {
                if (this.mstrConnString == value)
                    return;
                if (this.mInternalConnection != null)
                {
                    if (this.mInternalConnection.State != ConnectionState.Closed)
                        throw new NotSupportedException();
                    this.Close();
                }

                this.mstrConnString = value;
            }
        }

        [Description("Current connection timeout value.")]
        public override int ConnectionTimeout => 0;

        [Description("Initial data source catalog value.")]
        public override string Database
        {
            get
            {
                if (this.mstrServerDatabase == null && this.State == ConnectionState.Open)
                    this.mstrServerDatabase = this.GetServerConnectionPath();
                if (this.mstrServerDatabase != null)
                    return this.mstrServerDatabase;
                if (this.mInternalConnection == null)
                    return "";
                return this.mInternalConnection.InitialCatalog != null
                    ? this.mInternalConnection.InitialCatalog
                    : this.mInternalConnection.ConnectionPath;
            }
        }

        private string GetServerConnectionPath()
        {
            try
            {
                AdsCommand adsCommand = new AdsCommand("execute procedure sp_GetCatalogs()", this);
                AdsDataReader adsDataReader = adsCommand.ExecuteReader();
                string serverConnectionPath = (string)null;
                if (adsDataReader.Read())
                    serverConnectionPath = ((string)adsDataReader["TABLE_CAT"]).TrimEnd();
                adsDataReader.Close();
                adsCommand.Dispose();
                return serverConnectionPath;
            }
            catch
            {
                return (string)null;
            }
        }

        [Browsable(false)]
        public override ConnectionState State
        {
            get => this.mInternalConnection != null ? this.mInternalConnection.State : this.mState;
        }

        public override string DataSource
        {
            get
            {
                if (this.mInternalConnection != null)
                    return this.mInternalConnection.ConnectionPath;
                if (this.mstrConnString == null)
                    return "";
                AdsConnectionStringHandler connectionStringHandler = new AdsConnectionStringHandler();
                connectionStringHandler.ParseConnectionString(this.mstrConnString);
                return connectionStringHandler.DataSource;
            }
        }

        public override string ServerVersion
        {
            get
            {
                if (this.mState == ConnectionState.Closed)
                    throw new InvalidOperationException("Invalid operation. The connection is closed.");
                if (this.mstrServerVersion != null)
                    return this.mstrServerVersion;
                AdsCommand adsCommand = new AdsCommand("execute procedure sp_mgGetInstallInfo()", this);
                AdsDataReader adsDataReader = adsCommand.ExecuteReader();
                adsDataReader.Read();
                string serverVersion = adsDataReader.GetString(adsDataReader.GetOrdinal("Version"), true);
                adsDataReader.Close();
                adsDataReader.Dispose();
                adsCommand.Dispose();
                this.mstrServerVersion = serverVersion;
                return serverVersion;
            }
        }

        [Browsable(false)]
        public IntPtr ConnectionHandle
        {
            get
            {
                if (this.mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                return this.mInternalConnection.Handle;
            }
        }

        internal bool TrimTrailingSpaces => this.mInternalConnection.TrimTrailingSpaces;

        internal AdsInternalConnection InternalConnection => this.mInternalConnection;

        internal ushort TableType
        {
            get { return this.mbOverrideTableType ? this.musTableTypeOverride : this.mInternalConnection.TableType; }
            set
            {
                this.mbOverrideTableType = true;
                this.musTableTypeOverride = value;
            }
        }

        internal ushort LockType => this.mInternalConnection.LockType;

        internal ushort RightsChecking => this.mInternalConnection.RightsChecking;

        internal ushort CharType
        {
            get
            {
                ushort charType;
                try
                {
                    charType = !(this.mInternalConnection.CharType.ToUpper(CultureInfo.InvariantCulture) == "ANSI")
                        ? (!(this.mInternalConnection.CharType.ToUpper(CultureInfo.InvariantCulture) == "OEM")
                            ? (ushort)Enum.Parse(typeof(ACE.AdsCharTypes),
                                this.mInternalConnection.CharType.ToUpper(CultureInfo.InvariantCulture))
                            : (ushort)2)
                        : (ushort)1;
                }
                catch
                {
                    charType = (ushort)1;
                }

                return charType;
            }
        }

        internal string UnicodeCollation => this.mInternalConnection.UnicodeCollation;

        internal string Collation
        {
            get
            {
                return this.mInternalConnection.UnicodeCollation.Length > 0
                    ? this.mInternalConnection.CharType + ":" + this.mInternalConnection.UnicodeCollation
                    : this.mInternalConnection.CharType;
            }
        }

        internal uint TableOpenOptions => this.mInternalConnection.TableOpenOptions;

        internal IntPtr Handle => this.mInternalConnection.Handle;

        internal bool Busy
        {
            get => this.mbBusy;
            set => this.mbBusy = value;
        }

        internal ushort HandleType => this.mInternalConnection.HandleType;

        internal string ConnectionPath => this.mInternalConnection.ConnectionPath;

        internal bool DbfsUseNulls => this.mInternalConnection.DbfsUseNulls;

        internal string EncryptionPassword => this.mInternalConnection.EncryptionPassword;

        public override void EnlistTransaction(Transaction transaction)
        {
            this.mInternalConnection.EnlistTransaction(transaction, this);
            GC.KeepAlive((object)this);
        }

        public enum AdsObjectType
        {
            TableObject = 1,
            ViewObject = 2,
            RelationObject = 3,
            IndexFileObject = 4,
            IndexObject = 5,
            FieldObject = 6,
            UserObject = 7,
            ProcedureObject = 8,
            LinkObject = 9,
        }
    }
}