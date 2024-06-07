using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Transactions;
using AdvantageClientEngine;
using IsolationLevel = System.Data.IsolationLevel;

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

        public AdsConnection(string sConnString) => mstrConnString = sConnString;

        protected override DbProviderFactory DbProviderFactory
        {
            get => AdsFactory.Instance;
        }

        protected override void Dispose(bool bDisposing)
        {
            if (mbDisposed)
                return;
            lock (this)
            {
                if (mbDisposed)
                    return;
                if (State != ConnectionState.Closed && bDisposing)
                    Close();
                if (bDisposing && mInternalConnection != null)
                    mInternalConnection.Dispose(bDisposing);
                base.Dispose(bDisposing);
                mbDisposed = true;
            }
        }

        internal void AddProxyHandle(AdsProxyHandle hProxy)
        {
            lock (mProxyHandleList.SyncRoot)
                mProxyHandleList.Add(hProxy);
        }

        internal void RemoveProxyHandle(AdsProxyHandle hProxy)
        {
            lock (mProxyHandleList.SyncRoot)
                mProxyHandleList.Remove(hProxy);
        }

        private void RemoveAllProxyHandles()
        {
            lock (mProxyHandleList.SyncRoot)
            {
                foreach (AdsProxyHandle mProxyHandle in mProxyHandleList)
                    mProxyHandle.mhHandle = IntPtr.Zero;
                mProxyHandleList.Clear();
            }
        }

        private void OnInfoMessage(AdsInfoMessageEventArgs eventArgs)
        {
            var infoMessage = InfoMessage;
            if (infoMessage == null)
                return;
            infoMessage(this, eventArgs);
        }

        internal void FireStateChangeEvent(ConnectionState origState, ConnectionState currState)
        {
            if (origState == currState)
                return;
            OnStateChange(new StateChangeEventArgs(origState, currState));
        }

        internal void FireWarning(int iWarning, string strMessage)
        {
            OnInfoMessage(new AdsInfoMessageEventArgs(iWarning, strMessage));
        }

        public new AdsTransaction BeginTransaction()
        {
            if (mState == ConnectionState.Closed)
                throw new InvalidOperationException("Invalid operation. The connection is closed.");
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            return new AdsTransaction(this);
        }

        IDbTransaction IDbConnection.BeginTransaction() => BeginTransaction();

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return BeginTransaction(isolationLevel);
        }

        public new AdsTransaction BeginTransaction(IsolationLevel level)
        {
            if (mState == ConnectionState.Closed)
                throw new InvalidOperationException("Invalid operation. The connection is closed.");
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            if (level == IsolationLevel.Unspecified)
                level = IsolationLevel.ReadCommitted;
            if (level != IsolationLevel.ReadCommitted)
                throw new NotSupportedException("Isolation level must be ReadCommitted.");
            return new AdsTransaction(this);
        }

        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel level)
        {
            return BeginTransaction(level);
        }

        public override void ChangeDatabase(string dbName)
        {
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            throw new NotSupportedException("ChangeDatabase is not supported.");
        }

        public override void Open()
        {
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            if (mstrConnString == "")
                throw new InvalidOperationException("The ConnectionString property has not been initialized.");
            if (this.mState != ConnectionState.Closed)
                throw new InvalidOperationException("The Connection is already opened.");
            mPoolMgr.GetConnection(mstrConnString, out mInternalConnection, out mPool);
            if (mInternalConnection == null)
                throw new InvalidOperationException(
                    "Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.");
            if (mInternalConnection.State != ConnectionState.Open)
                mInternalConnection.Connect();
            var mState = this.mState;
            this.mState = mInternalConnection.State;
            if (mInternalConnection.TransScopeEnlist && Transaction.Current != null)
                mInternalConnection.EnlistTransaction(Transaction.Current, this);
            FireStateChangeEvent(mState, this.mState);
        }

        public override void Close()
        {
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            var mState = this.mState;
            this.mState = ConnectionState.Closed;
            RemoveAllProxyHandles();
            if (mInternalConnection != null)
            {
                if (mInternalConnection.CurrentTransaction != null)
                    mInternalConnection.DisposeOnCommit = true;
                else if (mInternalConnection is AdsPooledInternalConnection)
                    AdsPooledInternalConnection.ReturnConnectionToPool(
                        mInternalConnection as AdsPooledInternalConnection);
                else
                    mInternalConnection.Dispose();
            }

            mInternalConnection = null;
            mstrServerDatabase = null;
            FireStateChangeEvent(mState, this.mState);
        }

        IDbCommand IDbConnection.CreateCommand() => new AdsCommand("", this);

        protected override DbCommand CreateDbCommand() => CreateCommand();

        public new AdsCommand CreateCommand() => new AdsCommand("", this);

        public static void FlushConnectionPool() => mPoolMgr.FlushConnections();

        public static void FlushConnectionPool(string sConnString)
        {
            mPoolMgr.FlushConnections(sConnString);
        }

        public object[] GetDDObjects(AdsObjectType type, string strParent)
        {
            if (mState == ConnectionState.Closed)
                throw new InvalidOperationException("The connection is not open.");
            var arrayList = new ArrayList();
            ushort usFindObjectType;
            switch (type)
            {
                case AdsObjectType.TableObject:
                    usFindObjectType = 1;
                    break;
                case AdsObjectType.ViewObject:
                    usFindObjectType = 6;
                    break;
                case AdsObjectType.RelationObject:
                    usFindObjectType = 2;
                    break;
                case AdsObjectType.IndexFileObject:
                    usFindObjectType = 3;
                    break;
                case AdsObjectType.IndexObject:
                    usFindObjectType = 5;
                    break;
                case AdsObjectType.FieldObject:
                    usFindObjectType = 4;
                    break;
                case AdsObjectType.UserObject:
                    usFindObjectType = 8;
                    break;
                case AdsObjectType.ProcedureObject:
                    usFindObjectType = 10;
                    break;
                case AdsObjectType.LinkObject:
                    usFindObjectType = 12;
                    break;
                default:
                    usFindObjectType = 0;
                    break;
            }

            ushort pusObjectNameLen = 201;
            var pucObjectName = new char[201];
            IntPtr phFindHandle;
            uint ulRet;
            for (ulRet = ACE.AdsDDFindFirstObject(mInternalConnection.Handle, usFindObjectType, strParent,
                     pucObjectName, ref pusObjectNameLen, out phFindHandle);
                 ulRet == 0U;
                 ulRet = ACE.AdsDDFindNextObject(mInternalConnection.Handle, phFindHandle, pucObjectName,
                     ref pusObjectNameLen))
            {
                arrayList.Add(new string(pucObjectName, 0, pusObjectNameLen - 1));
                pusObjectNameLen = 201;
            }

            var close = (int)ACE.AdsDDFindClose(mInternalConnection.Handle, phFindHandle);
            if (ulRet != 5137U)
                AdsException.CheckACE(ulRet);
            return arrayList.ToArray();
        }

        public object[] GetTableNames()
        {
            var strMask = "";
            var connectionStringHandler = new AdsConnectionStringHandler();
            connectionStringHandler.ParseConnectionString(mstrConnString);
            var dataSource = connectionStringHandler.DataSource;
            var flag = false;
            if (dataSource.EndsWith(".add") || dataSource.EndsWith(".ADD"))
                flag = true;
            if (!flag)
            {
                var str = dataSource;
                if (!str.EndsWith("\\"))
                    str += "\\";
                strMask = connectionStringHandler.TableType != 3 ? str + "*.dbf" : str + "*.adt";
            }

            return InternalGetTableNames(strMask, true, true);
        }

        public object[] GetTableNames(string strMask)
        {
            return InternalGetTableNames(strMask, false, false);
        }

        public object[] GetTableNames(bool bIncludeLinkNames)
        {
            return InternalGetTableNames(null, bIncludeLinkNames, false);
        }

        private object[] InternalGetTableNames(
            string strMask,
            bool bGetLinkNames,
            bool bStripExtensions)
        {
            if (mState == ConnectionState.Closed)
                throw new InvalidOperationException("The connection is not open.");
            var arrayList = new ArrayList();
            if (mInternalConnection != null)
            {
                ushort pusFileLen = 256;
                var chArray1 = new char[pusFileLen];
                var pusDDLen = pusFileLen;
                var chArray2 = new char[pusDDLen];
                IntPtr plHandle;
                var ulRet = ACE.AdsFindFirstTable62(mInternalConnection.Handle, strMask, chArray2, ref pusDDLen,
                    chArray1, ref pusFileLen, out plHandle);
                var isDictionaryConn = IsDictionaryConn;
                for (;
                     ulRet == 0U;
                     ulRet = ACE.AdsFindNextTable62(mInternalConnection.Handle, plHandle, chArray2, ref pusDDLen,
                         chArray1, ref pusFileLen))
                {
                    if (!isDictionaryConn && bStripExtensions)
                        pusFileLen -= 4;
                    if (bGetLinkNames && pusDDLen > 0)
                    {
                        var str = new string(chArray2, 0, pusDDLen) + "::" +
                                  new string(chArray1, 0, pusFileLen);
                        arrayList.Add(str);
                    }
                    else
                        arrayList.Add(new string(chArray1, 0, pusFileLen));

                    pusFileLen = 256;
                    pusDDLen = pusFileLen;
                }

                var close = (int)ACE.AdsFindClose(mInternalConnection.Handle, plHandle);
                if (ulRet != 5059U)
                    AdsException.CheckACE(ulRet);
            }

            return arrayList.ToArray();
        }

        public void CloseCachedTables()
        {
            AdsException.CheckACE(ACE.AdsCloseCachedTables(mInternalConnection.Handle));
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
            return GetSchema(DbMetaDataCollectionNames.MetaDataCollections, null);
        }

        public override DataTable GetSchema(string collectionName)
        {
            return GetSchema(collectionName, null);
        }

        private string ArrayToString(object[] array)
        {
            if (array == null)
                return "[]";
            var stringBuilder = new StringBuilder("[");
            for (var index = 0; index < array.Length; ++index)
            {
                if (index > 0)
                    stringBuilder.Append(", ");
                if (array[index] == null)
                    stringBuilder.Append("null");
                else
                    stringBuilder.Append(array[index]);
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        public override DataTable GetSchema(string collectionName, string[] restrictions)
        {
            if (collectionName.Equals("DataSourceInformation", StringComparison.InvariantCultureIgnoreCase))
                return GetDataSourceInformation();
            if (collectionName.Equals("Tables", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaTables(restrictions);
            if (collectionName.Equals("Indexes", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaIndexes(restrictions);
            if (collectionName.Equals("IndexColumns", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaIndexColumns(restrictions);
            if (collectionName.Equals("Views", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaViews(restrictions);
            if (collectionName.Equals("Columns", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaColumnsOrig(restrictions);
            if (collectionName.Equals("ReservedWords", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaReservedWords(restrictions);
            if (collectionName.Equals("DataTypes", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaDataTypes(restrictions);
            if (collectionName.Equals("StoredProcedures", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaProcedures(restrictions);
            if (collectionName.Equals("StoredProcedureParameters", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaProcedureParameters(1, restrictions);
            if (collectionName.Equals("StoredProcedureColumns", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaProcedureParameters(3, restrictions);
            if (collectionName.Equals("ForeignKeys", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaForeignKeys(false, restrictions);
            if (collectionName.Equals("ForeignKeyColumns", StringComparison.InvariantCultureIgnoreCase))
                return GetSchemaForeignKeys(true, restrictions);
            throw new NotImplementedException(string.Format("GetSchema( {0} )", collectionName));
        }

        private DataTable GetSchemaIndexes(string[] restrictions)
        {
            if (restrictions == null || restrictions.Length != 4 || restrictions[2] == null)
                throw new NotImplementedException("Cannot enumerate all indexes in all tables.");
            var schemaIndexes = new DataTable("Indexes");
            using (var manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.IndexSchema.xml"))
                schemaIndexes.ReadXmlSchema(manifestResourceStream);
            DataTable dataTable1;
            try
            {
                if (RestrictionsHasLink(restrictions))
                    dataTable1 = ExecuteDataTable("execute procedure sp_GetPrimaryKeys( ?, null, ? )",
                        restrictions[1], restrictions[2]);
                else
                    dataTable1 = ExecuteDataTable("execute procedure sp_GetPrimaryKeys( null, null, ? )",
                        restrictions[2]);
            }
            catch
            {
                return schemaIndexes;
            }

            var str1 = "";
            if (dataTable1.Rows.Count > 0)
                str1 = (string)dataTable1.Rows[0]["PK_NAME"];
            DataTable dataTable2;
            if (RestrictionsHasLink(restrictions))
                dataTable2 = ExecuteDataTable("execute procedure sp_GetIndexInfo( ?, null, ?, false, false )",
                    restrictions[1], restrictions[2]);
            else
                dataTable2 = ExecuteDataTable("execute procedure sp_GetIndexInfo( null, null, ?, false, false )",
                    restrictions[2]);
            var str2 = "";
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable2.Rows)
            {
                if (row1["COLUMN_NAME"] != DBNull.Value && !((string)row1["COLUMN_NAME"] == "") &&
                    str2 != (string)row1["INDEX_NAME"])
                {
                    str2 = (string)row1["INDEX_NAME"];
                    var row2 = schemaIndexes.NewRow();
                    row2["catalog"] = restrictions[0];
                    row2["schema"] = restrictions[1];
                    row2["table"] = row1["TABLE_NAME"];
                    row2["name"] = row1["INDEX_NAME"];
                    row2["isunique"] = (bool)row1["NON_UNIQUE"] ? false : (object)true;
                    row2["isprimary"] = !(str1 == (string)row1["INDEX_NAME"]) ? false : (object)true;
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
            var schemaIndexColumns = new DataTable("IndexColumns");
            using (var manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.IndexColumnsSchema.xml"))
                schemaIndexColumns.ReadXmlSchema(manifestResourceStream);
            DataTable dataTable;
            if (RestrictionsHasLink(restrictions))
                dataTable = ExecuteDataTable("execute procedure sp_GetIndexInfo( ?, null, ?, false, false )",
                    restrictions[1], restrictions[2]);
            else
                dataTable = ExecuteDataTable("execute procedure sp_GetIndexInfo( null, null, ?, false, false )",
                    restrictions[2]);
            foreach (DataRow row1 in (InternalDataCollectionBase)dataTable.Rows)
            {
                if (restrictions[3].Equals((string)row1["INDEX_NAME"], StringComparison.InvariantCultureIgnoreCase))
                {
                    var row2 = schemaIndexColumns.NewRow();
                    row2["catalog"] = restrictions[0];
                    row2["schema"] = restrictions[1];
                    row2["table"] = restrictions[2];
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
            var schemaDataTypes = new DataTable("DataTypes");
            using (var manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.DataTypes.xml"))
            {
                var num = (int)schemaDataTypes.ReadXml(manifestResourceStream);
            }

            return schemaDataTypes;
        }

        private DataTable GetSchemaReservedWords(string[] restrictions)
        {
            var schemaReservedWords = ExecuteDataTable("execute procedure sp_getSQLKeywords()");
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
            string str1 = null;
            string str2 = null;
            string str3 = null;
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
                str2 = null;
            var schemaProcedures = ExecuteDataTable("execute procedure sp_GetProcedures(?, ?, ?)",
                str1, str2, str3);
            schemaProcedures.Columns["PROCEDURE_SCHEM"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)schemaProcedures.Rows)
                row["PROCEDURE_SCHEM"] = "::this";
            schemaProcedures.AcceptChanges();
            return schemaProcedures;
        }

        private DataTable GetSchemaProcedureParameters(short iColType, string[] restrictions)
        {
            string str1 = null;
            string str2 = null;
            string str3 = null;
            string str4 = null;
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
                str2 = null;
            var procedureParameters = ExecuteDataTable(
                "execute procedure sp_GetProcedureColumns(?, ?, ?, ?)", str1, str2, str3,
                str4);
            procedureParameters.Columns.Add("Column_Ordinal", Type.GetType("System.Int32"));
            var num = 0;
            procedureParameters.Columns.Add("IsOutputParameter", Type.GetType("System.Boolean"));
            procedureParameters.Columns.Add("Direction", Type.GetType("System.String"));
            procedureParameters.Columns["PROCEDURE_SCHEM"].ReadOnly = false;
            procedureParameters.Columns["TYPE_NAME"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)procedureParameters.Rows)
            {
                if ((short)row["Column_Type"] != iColType)
                {
                    row.Delete();
                }
                else
                {
                    if (restrictions.Length > 1)
                        row["Procedure_Schem"] = restrictions[1];
                    row["Column_Ordinal"] = num++;
                    var type = AdsDataReader.ConvertACETypeNameToType((string)row["Type_Name"]);
                    row["Type_Name"] = AdsDataReader.ConvertACETypeToName(type);
                    if (iColType == 1)
                    {
                        row["IsOutputParameter"] = false;
                        row["Direction"] = "IN";
                    }
                    else
                    {
                        row["IsOutputParameter"] = true;
                        row["Direction"] = "OUT";
                    }
                }
            }

            procedureParameters.AcceptChanges();
            return procedureParameters;
        }

        private DataTable GetSchemaForeignKeys(bool bIncludeAllColumns, string[] restrictions)
        {
            string str1 = null;
            string str2 = null;
            string str3 = null;
            string str4 = null;
            string str5 = null;
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
                str2 = null;
            var schemaForeignKeys = ExecuteDataTable(
                "EXECUTE PROCEDURE sp_GetForeignKeyColumns( ?, ?, ?, ? )", str1, str2, str3,
                str4);
            schemaForeignKeys.Columns["PKTABLE_SCHEM"].ReadOnly = false;
            schemaForeignKeys.Columns["FKTABLE_SCHEM"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)schemaForeignKeys.Rows)
            {
                var num = (short)row["KEY_SEQ"];
                if (!bIncludeAllColumns && num != 0)
                    row.Delete();
                else if (str5 != null && !restrictions[4].Equals((string)row["FKCOLUMN_NAME"],
                             StringComparison.InvariantCultureIgnoreCase))
                {
                    row.Delete();
                }
                else
                {
                    row["PKTABLE_SCHEM"] = "::this";
                    row["FKTABLE_SCHEM"] = "::this";
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
            string str = null;
            if (restrictions == null || restrictions.Length < 3 || restrictions[2] == null)
                throw new NotImplementedException("Cannot enumerate all columns in all tables.");
            if (restrictions.Length >= 4)
                str = restrictions[3];
            var adsCommand = new AdsCommand();
            adsCommand.Connection = this;
            if (RestrictionsHasLink(restrictions))
                adsCommand.CommandText = ":" + restrictions[1] + "::" + restrictions[2];
            else
                adsCommand.CommandText = restrictions[2];
            adsCommand.CommandType = CommandType.TableDirect;
            var adsDataReader =
                adsCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
            var schemaTable = adsDataReader.GetSchemaTable();
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
                        row["BaseCatalogName"] = restrictions[0];
                        row["BaseSchemaName"] = restrictions[1];
                        row["BaseTableName"] = restrictions[2];
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
                var adsCommand = new AdsCommand(strQuery, this);
                for (var index = 0; index < varargs.Length; ++index)
                    adsCommand.Parameters.Add(index + 1, varargs[index]);
                var reader = adsCommand.ExecuteReader();
                dataTable = new DataTable();
                var trimTrailingSpaces = TrimTrailingSpaces;
                mInternalConnection.TrimTrailingSpaces = true;
                dataTable.Load(reader);
                mInternalConnection.TrimTrailingSpaces = trimTrailingSpaces;
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
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            foreach (DataColumn column in Table.Columns)
            {
                stringBuilder.Append(column.ColumnName);
                stringBuilder.Append(", ");
            }

            stringBuilder.Append("]\n");
            foreach (DataRow row in (InternalDataCollectionBase)Table.Rows)
            {
                stringBuilder.Append("[");
                for (var index = 0; index < row.ItemArray.Length; ++index)
                {
                    if (index > 0)
                        stringBuilder.Append(", ");
                    if (row.ItemArray[index] == null)
                        stringBuilder.Append("null");
                    else
                        stringBuilder.Append(row.ItemArray[index]);
                }

                stringBuilder.Append("]\n");
            }

            return stringBuilder.ToString();
        }

        private DataTable GetSchemaTablesOrViews(string[] restrictions, string strType)
        {
            string str1 = null;
            string str2 = null;
            string str3 = null;
            var strQuery = "execute procedure sp_gettables( ?, ?, ?, ? )";
            var database = Database;
            if (restrictions != null)
            {
                if (restrictions.Length >= 1)
                    str1 = restrictions[0];
                if (restrictions.Length >= 2)
                    str2 = restrictions[1];
                if (restrictions.Length >= 3)
                    str3 = restrictions[2];
            }

            if (str1 != null && !str1.Equals(Database, StringComparison.InvariantCultureIgnoreCase))
            {
                str1 = "empty;,result";
            }
            else
            {
                if (str1 != null && str1.Equals(Database, StringComparison.InvariantCultureIgnoreCase))
                    str1 = null;
                if (str2 == "::this")
                    str2 = null;
                if (str2 != null)
                {
                    str1 = str2;
                    str2 = null;
                }
            }

            var schemaTablesOrViews =
                ExecuteDataTable(strQuery, str1, str2, str3, strType);
            schemaTablesOrViews.Columns["TABLE_CAT"].ReadOnly = false;
            schemaTablesOrViews.Columns["TABLE_NAME"].ReadOnly = false;
            schemaTablesOrViews.Columns["TABLE_SCHEM"].ReadOnly = false;
            foreach (DataRow row in (InternalDataCollectionBase)schemaTablesOrViews.Rows)
            {
                if (!IsDictionaryConn ||
                    database.Equals((string)row["TABLE_CAT"], StringComparison.InvariantCultureIgnoreCase))
                {
                    row["TABLE_SCHEM"] = "::this";
                }
                else
                {
                    row["TABLE_SCHEM"] = row["TABLE_CAT"];
                    row["TABLE_CAT"] = database;
                }

                var str4 = (string)row["TABLE_NAME"];
                if (str4.EndsWith(".adt", true, CultureInfo.InvariantCulture) ||
                    str4.EndsWith(".dbf", true, CultureInfo.InvariantCulture))
                {
                    var str5 = str4.Substring(0, str4.Length - 4);
                    row["TABLE_NAME"] = str5;
                }
            }

            schemaTablesOrViews.AcceptChanges();
            return schemaTablesOrViews;
        }

        private DataTable GetSchemaTables(string[] restrictions)
        {
            return GetSchemaTablesOrViews(restrictions, "TABLE");
        }

        private DataTable GetSchemaViews(string[] restrictions)
        {
            return GetSchemaTablesOrViews(restrictions, "VIEW");
        }

        private DataTable GetDataSourceInformation()
        {
            var sourceInformation = new DataTable("DataSourceInformation");
            using (var manifestResourceStream = Assembly.GetExecutingAssembly()
                       .GetManifestResourceStream("Advantage.Data.Provider.DataSourceInformation.xml"))
            {
                var num = (int)sourceInformation.ReadXml(manifestResourceStream);
            }

            if (mState == ConnectionState.Open)
            {
                sourceInformation.Rows[0][DbMetaDataColumnNames.DataSourceProductVersion] = ServerVersion;
                sourceInformation.Rows[0][DbMetaDataColumnNames.DataSourceProductVersionNormalized] =
                    ServerVersion;
            }

            if (TableType != 3)
                sourceInformation.Rows[0][DbMetaDataColumnNames.IdentifierPattern] =
                    "(^[a-zA-Z_][a-zA-Z0-9_]*$)|(^\\[[a-zA-Z_][a-zA-Z0-9_]*\\]$)|(^\\\"[a-zA-Z_][a-zA-Z0-9_]*\\\"$)";
            sourceInformation.Rows[0][DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern] =
                "(?<!\\[[^\\]]+)\\.";
            sourceInformation.AcceptChanges();
            return sourceInformation;
        }

        object ICloneable.Clone() => new AdsConnection(ConnectionString);

        public string ServerName
        {
            get
            {
                if (mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                var serverName = "";
                if (mInternalConnection != null)
                {
                    ushort pusLen = 260;
                    var pucName = new char[pusLen];
                    AdsException.CheckACE(ACE.AdsGetServerName(mInternalConnection.Handle, pucName, ref pusLen));
                    serverName = new string(pucName, 0, pusLen);
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
                var chArray = new char[pusDateBufLen];
                var pucTimeBuf = new char[pusTimeBufLen];
                int plTime;
                AdsException.CheckACE(ACE.AdsGetServerTime(mInternalConnection.Handle, chArray, ref pusDateBufLen,
                    out plTime, pucTimeBuf, ref pusTimeBufLen));
                double pdJulian;
                AdsException.CheckACE(ACEUNPUB.AdsConvertDateToJulian(mInternalConnection.Handle,
                    new string(chArray, 0, pusDateBufLen), pusDateBufLen, out pdJulian));
                ushort pusLen = 12;
                AdsException.CheckACE(ACEUNPUB.AdsConvertJulianToString(pdJulian, chArray, ref pusLen));
                var str1 = new string(chArray, 0, 4);
                var str2 = new string(chArray, 4, 2);
                var str3 = new string(chArray, 6, 2);
                var serverTime = new DateTime(Convert.ToInt32(str1, 10), Convert.ToInt32(str2, 10),
                    Convert.ToInt32(str3, 10));
                serverTime = serverTime.AddMilliseconds(plTime);
                return serverTime;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public string DateFormat
        {
            get
            {
                if (mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                ushort pusLen = 12;
                var pucFormat = new char[pusLen];
                AdsException.CheckACE(ACE.AdsGetDateFormat60(mInternalConnection.Handle, pucFormat, ref pusLen));
                return new string(pucFormat, 0, pusLen);
            }
            set
            {
                if (mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                AdsException.CheckACE(ACE.AdsSetDateFormat60(mInternalConnection.Handle, value));
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
                if (mState == ConnectionState.Closed)
                {
                    var connectionStringHandler = new AdsConnectionStringHandler();
                    connectionStringHandler.ParseConnectionString(mstrConnString);
                    return connectionStringHandler.ServerType == null ? "REMOTE" : connectionStringHandler.ServerType;
                }

                ushort pusConnectType;
                AdsException.CheckACE(ACE.AdsGetConnectionType(mInternalConnection.Handle, out pusConnectType));
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
            get => DDGetDatabaseVersion(111);
            set
            {
                if (value < 0)
                    throw new ArgumentException("DDVersionMajor value cannot be negative.");
                DDSetDatabaseVersion(111, (uint)value);
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int DDVersionMinor
        {
            get => DDGetDatabaseVersion(112);
            set
            {
                if (value < 0)
                    throw new ArgumentException("DDVersionMinor value cannot be negative.");
                DDSetDatabaseVersion(112, (uint)value);
            }
        }

        private int DDGetDatabaseVersion(ushort usProperty)
        {
            if (!IsDictionaryConn)
                throw new InvalidOperationException(
                    "Invalid access to DDMajorVersion. Connection is not a Data Dictionary.");
            ushort pusPropertyLen = 2;
            ushort pusProperty = 0;
            AdsException.CheckACE(ACE.AdsDDGetDatabaseProperty(mInternalConnection.Handle, usProperty,
                ref pusProperty, ref pusPropertyLen));
            return pusProperty;
        }

        private void DDSetDatabaseVersion(ushort usProperty, uint uiVer)
        {
            if (mState == ConnectionState.Closed)
                throw new InvalidOperationException("The connection is not open.");
            var pusProperty = (ushort)uiVer;
            AdsException.CheckACE(ACE.AdsDDSetDatabaseProperty(mInternalConnection.Handle, usProperty,
                ref pusProperty, 2));
        }

        public bool IsDictionaryConn
        {
            get
            {
                if (mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                return mInternalConnection.HandleType == 6;
            }
        }

        public bool IsConnectionAlive
        {
            get
            {
                if (mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                ushort pbConnectionIsAlive;
                return ACE.AdsIsConnectionAlive(mInternalConnection.Handle, out pbConnectionIsAlive) == 0U &&
                       pbConnectionIsAlive != 0;
            }
        }

        [Category("Data")]
        [Description("Information used to connect to a Data Source.")]
        [DefaultValue("")]
        public override string ConnectionString
        {
            get => mstrConnString;
            set
            {
                if (mstrConnString == value)
                    return;
                if (mInternalConnection != null)
                {
                    if (mInternalConnection.State != ConnectionState.Closed)
                        throw new NotSupportedException();
                    Close();
                }

                mstrConnString = value;
            }
        }

        [Description("Current connection timeout value.")]
        public override int ConnectionTimeout => 0;

        [Description("Initial data source catalog value.")]
        public override string Database
        {
            get
            {
                if (mstrServerDatabase == null && State == ConnectionState.Open)
                    mstrServerDatabase = GetServerConnectionPath();
                if (mstrServerDatabase != null)
                    return mstrServerDatabase;
                if (mInternalConnection == null)
                    return "";
                return mInternalConnection.InitialCatalog != null
                    ? mInternalConnection.InitialCatalog
                    : mInternalConnection.ConnectionPath;
            }
        }

        private string GetServerConnectionPath()
        {
            try
            {
                var adsCommand = new AdsCommand("execute procedure sp_GetCatalogs()", this);
                var adsDataReader = adsCommand.ExecuteReader();
                string serverConnectionPath = null;
                if (adsDataReader.Read())
                    serverConnectionPath = ((string)adsDataReader["TABLE_CAT"]).TrimEnd();
                adsDataReader.Close();
                adsCommand.Dispose();
                return serverConnectionPath;
            }
            catch
            {
                return null;
            }
        }

        [Browsable(false)]
        public override ConnectionState State
        {
            get => mInternalConnection != null ? mInternalConnection.State : mState;
        }

        public override string DataSource
        {
            get
            {
                if (mInternalConnection != null)
                    return mInternalConnection.ConnectionPath;
                if (mstrConnString == null)
                    return "";
                var connectionStringHandler = new AdsConnectionStringHandler();
                connectionStringHandler.ParseConnectionString(mstrConnString);
                return connectionStringHandler.DataSource;
            }
        }

        public override string ServerVersion
        {
            get
            {
                if (mState == ConnectionState.Closed)
                    throw new InvalidOperationException("Invalid operation. The connection is closed.");
                if (mstrServerVersion != null)
                    return mstrServerVersion;
                var adsCommand = new AdsCommand("execute procedure sp_mgGetInstallInfo()", this);
                var adsDataReader = adsCommand.ExecuteReader();
                adsDataReader.Read();
                var serverVersion = adsDataReader.GetString(adsDataReader.GetOrdinal("Version"), true);
                adsDataReader.Close();
                adsDataReader.Dispose();
                adsCommand.Dispose();
                mstrServerVersion = serverVersion;
                return serverVersion;
            }
        }

        [Browsable(false)]
        public IntPtr ConnectionHandle
        {
            get
            {
                if (mState == ConnectionState.Closed)
                    throw new InvalidOperationException("The connection is not open.");
                return mInternalConnection.Handle;
            }
        }

        internal bool TrimTrailingSpaces => mInternalConnection.TrimTrailingSpaces;

        internal AdsInternalConnection InternalConnection => mInternalConnection;

        internal ushort TableType
        {
            get { return mbOverrideTableType ? musTableTypeOverride : mInternalConnection.TableType; }
            set
            {
                mbOverrideTableType = true;
                musTableTypeOverride = value;
            }
        }

        internal ushort LockType => mInternalConnection.LockType;

        internal ushort RightsChecking => mInternalConnection.RightsChecking;

        internal ushort CharType
        {
            get
            {
                ushort charType;
                try
                {
                    charType = !(mInternalConnection.CharType.ToUpper(CultureInfo.InvariantCulture) == "ANSI")
                        ? (!(mInternalConnection.CharType.ToUpper(CultureInfo.InvariantCulture) == "OEM")
                            ? (ushort)Enum.Parse(typeof(ACE.AdsCharTypes),
                                mInternalConnection.CharType.ToUpper(CultureInfo.InvariantCulture))
                            : (ushort)2)
                        : (ushort)1;
                }
                catch
                {
                    charType = 1;
                }

                return charType;
            }
        }

        internal string UnicodeCollation => mInternalConnection.UnicodeCollation;

        internal string Collation
        {
            get
            {
                return mInternalConnection.UnicodeCollation.Length > 0
                    ? mInternalConnection.CharType + ":" + mInternalConnection.UnicodeCollation
                    : mInternalConnection.CharType;
            }
        }

        internal uint TableOpenOptions => mInternalConnection.TableOpenOptions;

        internal IntPtr Handle => mInternalConnection.Handle;

        internal bool Busy
        {
            get => mbBusy;
            set => mbBusy = value;
        }

        internal ushort HandleType => mInternalConnection.HandleType;

        internal string ConnectionPath => mInternalConnection.ConnectionPath;

        internal bool DbfsUseNulls => mInternalConnection.DbfsUseNulls;

        internal string EncryptionPassword => mInternalConnection.EncryptionPassword;

        public override void EnlistTransaction(Transaction transaction)
        {
            mInternalConnection.EnlistTransaction(transaction, this);
            GC.KeepAlive(this);
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