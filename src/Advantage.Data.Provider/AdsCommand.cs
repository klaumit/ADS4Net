using AdvantageClientEngine;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Advantage.Data.Provider
{
    [DesignerCategory("Form")]
    [Designer("Advantage.Data.Provider.AdsCommandDesigner, Advantage.Designer")]
    [ToolboxBitmap(typeof(AdsCommand), "adscmd.bmp")]
    public sealed class AdsCommand : DbCommand, ICloneable, IDbCommand, IDisposable
    {
        private bool mbDisposed;
        private AdsConnection mConnection;
        private AdsTransaction mTxn;
        private string mstrCmdText;
        private string mstrProcCall;
        private UpdateRowSource mUpdatedRowSource = UpdateRowSource.Both;
        private AdsParameterCollection mParameters = new AdsParameterCollection();
        private AdsProxyHandle mStmt = new AdsProxyHandle();
        private bool mbPrepared;
        private int miTimeout = 30;
        private bool mbAttemptTimeout;
        private bool mbCmdTimedOut;
        private DateTime mCmdStartTime;
        private CommandType mCommandType = CommandType.Text;
        private bool mbAttemptCancel;
        private bool mbCancelled;
        private int miProgress;
        private bool mbVisible = true;
        private ACE.CallbackFn mCmdCallback;
        private string mstrEnityTablePrefix = "#NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_";

        public event AdsInfoMessageEventHandler ProgressMessage;

        public AdsCommand()
        {
        }

        ~AdsCommand() => this.Dispose(false);

        public AdsCommand(string strCmdText) => this.mstrCmdText = strCmdText;

        public AdsCommand(string strCmdText, AdsConnection connection)
        {
            this.mstrCmdText = strCmdText;
            this.mConnection = connection;
        }

        public AdsCommand(string strCmdText, AdsConnection connection, AdsTransaction txn)
        {
            this.mstrCmdText = strCmdText;
            this.mConnection = connection;
            this.mTxn = txn;
        }

        public AdsCommand(AdsCommand cmdCopy)
        {
            this.Connection = cmdCopy.Connection;
            this.CommandText = cmdCopy.CommandText;
            this.UpdatedRowSource = cmdCopy.UpdatedRowSource;
            this.CommandTimeout = cmdCopy.CommandTimeout;
            this.CommandType = cmdCopy.CommandType;
            this.DesignTimeVisible = cmdCopy.DesignTimeVisible;
            this.Transaction = cmdCopy.Transaction;
            foreach (ICloneable parameter in (DbParameterCollection)cmdCopy.Parameters)
                this.Parameters.Add(parameter.Clone());
        }

        public AdsCommand Clone() => new AdsCommand(this);

        object ICloneable.Clone() => (object)this.Clone();

        protected override void Dispose(bool bExplicitDispose)
        {
            if (this.mbDisposed)
                return;
            lock (this)
            {
                if (this.mbDisposed)
                    return;
                int num1 = bExplicitDispose ? 1 : 0;
                if (this.mStmt.mhHandle != IntPtr.Zero)
                {
                    if (this.mConnection != null && this.mConnection.State == ConnectionState.Open)
                    {
                        int num2 = (int)ACE.AdsCloseSQLStatement(this.mStmt.mhHandle);
                        this.mConnection.RemoveProxyHandle(this.mStmt);
                    }

                    this.mStmt.mhHandle = IntPtr.Zero;
                }

                base.Dispose(bExplicitDispose);
                this.mbDisposed = true;
            }
        }

        [Description("Command text to execute.")]
        [DefaultValue(null)]
        [Category("Data")]
        public override string CommandText
        {
            get => this.mstrCmdText;
            set
            {
                this.mstrCmdText = value;
                this.mbPrepared = false;
            }
        }

        [Description("Time to wait for the command to execute.")]
        [DefaultValue(30)]
        public override int CommandTimeout
        {
            get => this.miTimeout;
            set
            {
                this.miTimeout =
                    value >= 0 ? value : throw new ArgumentException("CommandTimeout cannot be less than 0.");
            }
        }

        [DefaultValue(CommandType.Text)]
        [Description("How to interpret the CommandText.")]
        [Category("Data")]
        public override CommandType CommandType
        {
            get => this.mCommandType;
            set
            {
                switch (value)
                {
                    case CommandType.Text:
                    case CommandType.StoredProcedure:
                    case CommandType.TableDirect:
                        this.mCommandType = value;
                        break;
                    default:
                        throw new ArgumentException("The CommandType value " + (object)value + " is not valid.");
                }
            }
        }

        IDbConnection IDbCommand.Connection
        {
            get => (IDbConnection)this.Connection;
            set => this.Connection = (AdsConnection)value;
        }

        protected override DbConnection DbConnection
        {
            get => (DbConnection)this.Connection;
            set => this.Connection = (AdsConnection)value;
        }

        [Description("Connection used by the command.")]
        [Category("Behavior")]
        [DefaultValue(null)]
        public AdsConnection Connection
        {
            get => this.mConnection;
            set
            {
                if (this.mConnection != value)
                {
                    if (this.mConnection != null && this.mConnection.Busy)
                        throw new InvalidOperationException(
                            "The connection has an open DataReader.  It must be closed first.");
                    if (this.mStmt.mhHandle != IntPtr.Zero)
                    {
                        if (this.mConnection != null && this.mConnection.State == ConnectionState.Open)
                        {
                            AdsException.CheckACE(ACE.AdsCloseSQLStatement(this.mStmt.mhHandle));
                            this.mConnection.RemoveProxyHandle(this.mStmt);
                        }

                        this.mStmt.mhHandle = IntPtr.Zero;
                        this.mbPrepared = false;
                    }

                    this.Transaction = (AdsTransaction)null;
                }

                this.mConnection = value;
            }
        }

        IDataParameterCollection IDbCommand.Parameters => (IDataParameterCollection)this.Parameters;

        protected override DbParameterCollection DbParameterCollection
        {
            get => (DbParameterCollection)this.Parameters;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Data")]
        public AdsParameterCollection Parameters => this.mParameters;

        IDbTransaction IDbCommand.Transaction
        {
            get => (IDbTransaction)this.Transaction;
            set => this.Transaction = (AdsTransaction)value;
        }

        protected override DbTransaction DbTransaction
        {
            get => (DbTransaction)this.Transaction;
            set => this.Transaction = (AdsTransaction)value;
        }

        [DefaultValue(null)]
        [Browsable(false)]
        public AdsTransaction Transaction
        {
            get => this.mTxn;
            set => this.mTxn = value;
        }

        [DefaultValue(UpdateRowSource.Both)]
        [Description("When used by a DataAdapter.Update, how command results are applied to the current DataRow.")]
        [Category("Behavior")]
        public override UpdateRowSource UpdatedRowSource
        {
            get => this.mUpdatedRowSource;
            set => this.mUpdatedRowSource = value;
        }

        public override void Cancel() => this.mbAttemptCancel = true;

        [Browsable(false)] public int Progress => this.miProgress;

        [Browsable(false)] public bool TimedOut => this.mbCmdTimedOut;

        [Browsable(false)] public bool Cancelled => this.mbCancelled;

        public int RecordsAffected
        {
            get
            {
                uint pulCount;
                AdsException.CheckACE(ACE.AdsGetRecordCount(this.mStmt.mhHandle, (ushort)2, out pulCount));
                return (int)pulCount;
            }
        }

        private void OnProgressMessage(AdsInfoMessageEventArgs eventArgs)
        {
            AdsInfoMessageEventHandler progressMessage = this.ProgressMessage;
            if (progressMessage == null)
                return;
            progressMessage((object)this, eventArgs);
        }

        internal void FireProgress(int iProgress)
        {
            this.OnProgressMessage(new AdsInfoMessageEventArgs(iProgress, "Progress"));
        }

        private uint ProgressCallback(ushort usPercentDone, uint ulCallbackID)
        {
            this.miProgress = usPercentDone <= (ushort)100 ? (int)usPercentDone : 100;
            this.FireProgress(this.miProgress);
            if (this.miTimeout > 0 && DateTime.Now - this.mCmdStartTime >= new TimeSpan(0, 0, 0, this.miTimeout))
            {
                this.mbAttemptTimeout = true;
                return 1;
            }

            return this.mbAttemptCancel ? 1U : 0U;
        }

        [Browsable(false)]
        [DefaultValue(true)]
        [DesignOnly(true)]
        public override bool DesignTimeVisible
        {
            get => this.mbVisible;
            set
            {
                this.mbVisible = value;
                TypeDescriptor.Refresh((object)this);
            }
        }

        IDbDataParameter IDbCommand.CreateParameter() => (IDbDataParameter)new AdsParameter();

        protected override DbParameter CreateDbParameter() => (DbParameter)this.CreateParameter();

        public AdsParameter CreateParameter() => new AdsParameter();

        public override int ExecuteNonQuery()
        {
            int num = -1;
            IntPtr hCursor = IntPtr.Zero;
            this.ExecuteStatement(out hCursor, out string _);
            if (this.mCommandType != CommandType.TableDirect)
            {
                uint pulCount;
                uint recordCount = ACE.AdsGetRecordCount(this.mStmt.mhHandle, (ushort)2, out pulCount);
                if (recordCount == 5117U)
                {
                    num = -1;
                }
                else
                {
                    AdsException.CheckACE(recordCount);
                    num = (int)pulCount;
                }
            }

            if (hCursor != IntPtr.Zero)
            {
                if (this.mCommandType == CommandType.StoredProcedure)
                {
                    AdsDataReader reader =
                        new AdsDataReader(hCursor, -1, this, this.mConnection, CommandBehavior.Default);
                    this.StoreSPValues(reader);
                    reader.Close();
                }
                else
                {
                    uint ulRet = ACE.AdsCloseTable(hCursor);
                    IntPtr zero = IntPtr.Zero;
                    AdsException.CheckACE(ulRet);
                }
            }

            return num;
        }

        IDataReader IDbCommand.ExecuteReader() => (IDataReader)this.ExecuteReader();

        public AdsDataReader ExecuteReader() => this.ExecuteReader(CommandBehavior.Default);

        public AdsExtendedReader ExecuteExtendedReader()
        {
            return this.ExecuteExtendedReader(CommandBehavior.Default);
        }

        IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
        {
            return (IDataReader)this.ExecuteReader(behavior);
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return (DbDataReader)this.ExecuteReader(behavior);
        }

        public AdsDataReader ExecuteReader(CommandBehavior behavior)
        {
            return this.ExtecuteReaderInternal(behavior, false);
        }

        public AdsExtendedReader ExecuteExtendedReader(CommandBehavior behavior)
        {
            return this.ExtecuteReaderInternal(behavior, true) as AdsExtendedReader;
        }

        private AdsDataReader ExtecuteReaderInternal(CommandBehavior behavior, bool bExtendedReader)
        {
            int iRecsAffected = -1;
            IntPtr hCursor = IntPtr.Zero;
            string strIndex;
            this.ExecuteStatement(out hCursor, out strIndex);
            if (this.mCommandType != CommandType.TableDirect)
            {
                uint pulCount;
                uint recordCount = ACE.AdsGetRecordCount(this.mStmt.mhHandle, (ushort)2, out pulCount);
                if (recordCount == 5117U)
                {
                    iRecsAffected = -1;
                }
                else
                {
                    AdsException.CheckACE(recordCount);
                    iRecsAffected = (int)pulCount;
                }
            }

            AdsDataReader reader;
            if (bExtendedReader || strIndex != null)
            {
                reader = (AdsDataReader)new AdsExtendedReader(hCursor, iRecsAffected, this, this.mConnection, behavior);
                if (strIndex != null)
                {
                    ((AdsExtendedReader)reader).ActiveIndex = strIndex;
                    reader.SetBOF();
                }
            }
            else
                reader = new AdsDataReader(hCursor, iRecsAffected, this, this.mConnection, behavior);

            if (this.mCommandType == CommandType.StoredProcedure)
                this.StoreSPValues(reader);
            return reader;
        }

        private void StoreSPValues(AdsDataReader reader)
        {
            bool flag = false;
            if (this.mCommandType != CommandType.StoredProcedure)
                return;
            foreach (DbParameter mParameter in (DbParameterCollection)this.mParameters)
            {
                if (mParameter.Direction == ParameterDirection.Output)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag || !reader.Read())
                return;
            foreach (AdsParameter mParameter in (DbParameterCollection)this.mParameters)
            {
                if (mParameter.Direction == ParameterDirection.Output)
                {
                    try
                    {
                        mParameter.Value = reader[mParameter.ParameterName];
                    }
                    catch (AdsException ex)
                    {
                        this.mConnection.FireWarning(ex.Number, ex.Message);
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        this.mConnection.FireWarning(5113, "Column " + mParameter.ParameterName + " not found.");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            reader.SetBOF();
        }

        public override object ExecuteScalar()
        {
            object obj = (object)null;
            IntPtr hCursor = IntPtr.Zero;
            this.ExecuteStatement(out hCursor, out string _);
            if (hCursor != IntPtr.Zero)
            {
                AdsDataReader reader = new AdsDataReader(hCursor, -1, this, this.mConnection, CommandBehavior.Default);
                this.StoreSPValues(reader);
                if (reader.Read() && reader.FieldCount > 0)
                    obj = reader[0];
                reader.Dispose();
            }

            return obj;
        }

        private string MakeTablePath(string strConnectPath, string strTableName)
        {
            if (strTableName.Length > 1 && strTableName[1] == ':' ||
                strTableName.Length > 0 && (strTableName[0] == '\\' || strTableName[0] == '/'))
                return strTableName;
            if (strConnectPath == null)
            {
                ushort pusLen = 261;
                char[] pucConnectionPath = new char[(int)pusLen];
                if (ACE.AdsGetConnectionPath(this.mConnection.Handle, pucConnectionPath, ref pusLen) != 0U)
                    return strTableName;
                strConnectPath = new string(pucConnectionPath, 0, (int)pusLen);
            }

            string path = strConnectPath;
            if (path.EndsWith(".add") || path.EndsWith(".ADD"))
                path = Path.GetDirectoryName(path);
            if (!path.EndsWith("\\") && !path.EndsWith("/"))
                path += "\\";
            return path + this.mstrCmdText;
        }

        private void ExecuteStatement(out IntPtr hCursor, out string strIndex)
        {
            hCursor = IntPtr.Zero;
            strIndex = (string)null;
            this.Prepare();
            if (this.mCommandType == CommandType.TableDirect)
            {
                ushort usTableType = this.mConnection.TableType;
                string pucName;
                if (this.mConnection.HandleType != (ushort)1)
                {
                    usTableType = (ushort)0;
                    pucName = this.mstrCmdText;
                }
                else
                    pucName = this.MakeTablePath(this.mConnection.ConnectionPath, this.mstrCmdText);

                uint ulRet = ACE.AdsOpenTable90(this.mConnection.Handle, pucName, (string)null, usTableType,
                    this.mConnection.CharType, this.mConnection.LockType, this.mConnection.RightsChecking,
                    this.mConnection.TableOpenOptions, this.mConnection.Collation, out hCursor);
                if (ulRet == 5132U && this.mConnection.HandleType != (ushort)1)
                    ulRet = ACE.AdsOpenTable90(this.mConnection.Handle,
                        this.MakeTablePath(this.mConnection.ConnectionPath, this.mstrCmdText), (string)null,
                        this.mConnection.TableType, this.mConnection.CharType, this.mConnection.LockType,
                        this.mConnection.RightsChecking, this.mConnection.TableOpenOptions, this.mConnection.Collation,
                        out hCursor);
                AdsException.CheckACE(ulRet);
                if (this.mConnection.HandleType == (ushort)1 && this.mConnection.EncryptionPassword.Length > 0)
                {
                    uint uiRet = ACE.AdsEnableEncryption(hCursor, this.mConnection.EncryptionPassword);
                    if (uiRet != 0U)
                    {
                        AdsException adsException = new AdsException(uiRet);
                        ACE.AdsCloseTable(hCursor);
                        hCursor = IntPtr.Zero;
                        throw adsException;
                    }
                }

                if (this.mConnection.IsDictionaryConn)
                {
                    char[] pucProperty = new char[201];
                    ushort length = (ushort)pucProperty.Length;
                    if (ACE.AdsDDGetTableProperty(this.mConnection.Handle, this.mstrCmdText, (ushort)213, pucProperty,
                            ref length) == 0U && length > (ushort)1)
                    {
                        --length;
                        strIndex = new string(pucProperty, 0, (int)length);
                    }
                }
            }
            else
            {
                this.SetParameters();
                if (this.mCmdCallback == null)
                    this.mCmdCallback = new ACE.CallbackFn(this.ProgressCallback);
                try
                {
                    AdsException.CheckACE(ACE.AdsRegisterCallbackFunction(this.mCmdCallback, 1U));
                    this.mCmdStartTime = DateTime.Now;
                    this.mbCmdTimedOut = false;
                    this.mbAttemptTimeout = false;
                    this.mbCancelled = false;
                    this.mbAttemptCancel = false;
                    this.miProgress = 0;
                    if (ACE.AdsExecuteSQL(this.mStmt.mhHandle, out hCursor) != 0U)
                    {
                        AdsException adsException = new AdsException("AdsCommand query execution failed.");
                        if (adsException.Number == 7209)
                        {
                            if (this.mbAttemptTimeout)
                            {
                                this.mbCmdTimedOut = true;
                                adsException.ReplaceText("AdsCommand query execution failed.", "Query was timed out.");
                            }
                            else
                            {
                                this.mbCancelled = true;
                                adsException.ReplaceText("AdsCommand query execution failed.", "Query was cancelled.");
                            }
                        }

                        if (adsException.Number != 2102)
                            throw adsException;
                        this.mConnection.FireWarning(adsException.Number, adsException.Message);
                    }
                }
                finally
                {
                    int num = (int)ACE.AdsClearCallbackFunction();
                }
            }

            if (this.miProgress == 100)
                return;
            this.miProgress = 100;
            this.FireProgress(this.miProgress);
        }

        private void CheckConnection()
        {
            if (this.mConnection == null || this.mConnection.State != ConnectionState.Open)
                throw new InvalidOperationException("Connection must be open.");
            if (this.mConnection.Busy)
                throw new InvalidOperationException("The connection has an open DataReader.  It must be closed first.");
        }

        private void InternalPrepare()
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            this.CheckConnection();
            this.miProgress = 0;
            if (this.mCommandType == CommandType.TableDirect || this.mStmt.mhHandle != IntPtr.Zero)
                return;
            this.mbPrepared = false;
            AdsException.CheckACE(ACE.AdsCreateSQLStatement(this.mConnection.Handle, out this.mStmt.mhHandle));
            this.mConnection.AddProxyHandle(this.mStmt);
            AdsException.CheckACE(ACE.AdsStmtSetTableType(this.mStmt.mhHandle, this.mConnection.TableType));
            AdsException.CheckACE(ACE.AdsStmtSetTableCollation(this.mStmt.mhHandle, this.mConnection.Collation));
            AdsException.CheckACE(ACE.AdsStmtSetTableLockType(this.mStmt.mhHandle, this.mConnection.LockType));
            AdsException.CheckACE(ACE.AdsStmtSetTableRights(this.mStmt.mhHandle, this.mConnection.RightsChecking));
            AdsException.CheckACE(((int)this.mConnection.TableOpenOptions & 2) == 0
                ? ACE.AdsStmtSetTableReadOnly(this.mStmt.mhHandle, (ushort)2)
                : ACE.AdsStmtSetTableReadOnly(this.mStmt.mhHandle, (ushort)1));
        }

        private void GenerateEntityMetadataTables()
        {
            IntPtr phCursor = IntPtr.Zero;
            string pucSQL =
                "DECLARE Tables CURSOR as EXECUTE PROCEDURE sp_GetTables( NULL, NULL, '%', 'TABLE' );\nDECLARE Columns CURSOR as EXECUTE PROCEDURE sp_GetColumns( Tables.TABLE_CAT, NULL, Tables.TABLE_NAME, NULL );\nDECLARE Views CURSOR as EXECUTE PROCEDURE sp_GetTables( NULL, NULL, '%', 'VIEW' );\nDECLARE ViewColumns CURSOR as EXECUTE PROCEDURE sp_GetColumns( Views.TABLE_CAT, NULL, Views.TABLE_NAME, NULL );\nDECLARE RI CURSOR as EXECUTE PROCEDURE sp_GetForeignKeyColumns( NULL, NULL, NULL, NULL );\nDECLARE UniqueIndexes CURSOR AS EXECUTE PROCEDURE sp_GetIndexInfo( Tables.TABLE_CAT, NULL, Tables.TABLE_NAME, TRUE, false );\nDECLARE WorkCursor CURSOR;\nDECLARE PrimaryKeys CURSOR AS EXECUTE PROCEDURE sp_GetPrimaryKeys( WorkCursor.SchemaName, NULL, WorkCursor.Name );\nDECLARE Procedures CURSOR AS EXECUTE PROCEDURE sp_GetProcedures( NULL, NULL, NULL );\nDECLARE ProcColumns CURSOR AS EXECUTE PROCEDURE sp_getProcedureColumns( RTRIM( Procedures.PROCEDURE_CAT ), NULL, RTRIM( Procedures.PROCEDURE_NAME ), NULL );\nDECLARE @Schema String;\nDECLARE @FSchema String;\nDECLARE @ParentID String;\nDECLARE @FParentID String;\nDECLARE @ID String;\nDECLARE @FID String;\nDECLARE @ColumnID String;\nDECLARE @DataType String;\nDECLARE @IsIdentity logical;\nDECLARE @IsStoreGenerated logical;\nDECLARE @Position INTEGER;\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_p1wLQyVUQyCbc7oa7SR1zg_STables;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_oqpBG1cEQUiOhyrNzNa1ww_STableColumns;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_WbQIJ2TaREKc05VZnT3Ttg_SViews;\nCATCH ALL\n-- Ignore all ERRORS returned on this call.\nEND TRY;\n\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_fAMP9jIlSFuvMyHN1woedg_SViewColumns;\nCATCH ALL\n-- Ignore all ERRORS returned on this call.\nEND TRY;\n\n\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_p1wLQyVUQyCbc7oa7SR1zg_STables( ID varchar( 456 ), CatalogName varchar( 200 ), SchemaName varchar( 200 ), Name varchar( 255 ) );\n\n\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_oqpBG1cEQUiOhyrNzNa1ww_STableColumns( ID varchar( 657 ),\n                                   ParentId varchar( 456 ),\n                                   Name varchar( 200 ),\n                                   Ordinal INTEGER,\n                                   IsNullable LOGICAL,\n                                   TypeName varchar( 256 ),\n                                   MaxLength INTEGER,\n                                   Precision INTEGER,\n                                   DateTimePrecision INTEGER,\n                                   Scale INTEGER,\n                                   CollationCatalog varchar( 128 ),\n                                   CollationSchema varchar( 128 ),\n                                   CollationName varchar( 128 ),\n                                   CharacterSetCatalog varchar( 128 ),\n                                   CharacterSetSchema varchar( 128 ),\n                                   CharacterSetName varchar( 128 ),\n                                   IsMultiSet LOGICAL,\n                                   IsIdentity LOGICAL,\n                                   IsStoreGenerated LOGICAL,\n                                   [Default] memo );\n\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_WbQIJ2TaREKc05VZnT3Ttg_SViews( ID varchar( 456 ), CatalogName varchar( 200 ), SchemaName varchar( 200 ), Name varchar( 255 ), ViewDefinition MEMO, IsUpdatable LOGICAL );\n\n\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_fAMP9jIlSFuvMyHN1woedg_SViewColumns(  Id varchar( 657 ),\n                                                                                     ParentId varchar( 456 ),\n                                                                                     Name varchar( 200 ),\n                                                                                     Ordinal INTEGER,\n                                                                                     IsNullable LOGICAL,\n                                                                                     TypeName varchar( 256 ),\n                                                                                     MaxLength INTEGER,\n                                                                                     Precision INTEGER,\n                                                                                     DateTimePrecision INTEGER,\n                                                                                     Scale INTEGER,\n                                                                                     CollationCatalog varchar( 128 ),\n                                                                                     CollationSchema varchar( 128 ),\n                                                                                     CollationName varchar( 128 ),\n                                                                                     CharacterSetCatalog varchar( 128 ),\n                                                                                     CharacterSetSchema varchar( 128 ),\n                                                                                     CharacterSetName varchar( 128 ),\n                                                                                     IsMultiSet LOGICAL,\n                                                                                     IsIdentity LOGICAL,\n                                                                                     IsStoreGenerated LOGICAL,\n                                                                                     [Default] memo );\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_yxmlhu1QQRCaH7RRB6Ih2Q_SContraints;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n\n-- SConstraints\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_yxmlhu1QQRCaH7RRB6Ih2Q_SContraints( ID varchar( 401 ), \n                                                                                    ParentId varchar( 456 ),\n                                            \n                                                           Name varchar( 200 ),\n                                                              ConstraintType varchar( 11 ),\n                                                              IsDeferrable logical,\n                                                              IsInitiallyDeferred logical );\n\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_HjKKob2HSGy7ZBeM716Y0A_SConstraintColumns;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n\n-- SConstraintColumns\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_HjKKob2HSGy7ZBeM716Y0A_SConstraintColumns( ConstraintId varchar( 401 ), \n                                                                                    ColumnId varchar( 657 ) );\n                                                              \nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_2BQ1qv15Qg2jNWEJRRLFIw_SForeignKeyConstraints;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n\n-- SForeignKeyConstraints\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_2BQ1qv15Qg2jNWEJRRLFIw_SForeignKeyConstraints( ID varchar( 401 ), \n                                                                                    UpdateRule varchar( 11 ),\n                                                                                      DeleteRule varchar( 11 ) );\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_KlYebalJROGCtfxDWYbCeQ_SForeignKeys;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n\n-- SForeignKeys\nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_KlYebalJROGCtfxDWYbCeQ_SForeignKeys( ID varchar( 401 ), \n                                                                                    ToColumnId varchar( 657 ),\n                                                                                      FromColumnId varchar( 657 ),\n                                                                                   ConstraintId varchar( 401 ),\n                                                                                      Ordinal integer );\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_p6veFSIeS4mIKEByRvQhegAA_SProcedures;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n                                                                                    \nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_p6veFSIeS4mIKEByRvQhegAA_SProcedures( ID char( 401 ),\n                                                                                                  CatalogName char( 401 ),\n                                                                                                  SchemaName char( 401 ),\n                                                                                                  Name char( 200 ) );\n\nTRY\n   DROP TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_VAy9VFx0SHadO67Dsy5keQBB_SProcedureParameters;\nCATCH ALL\n   -- Ignore all ERRORS returned on this call.\nEND TRY;\n                                                                                    \nCREATE TABLE #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_VAy9VFx0SHadO67Dsy5keQBB_SProcedureParameters( ID char( 657 ),\n                                   ParentId char( 456 ),\n                                   Name char( 200 ),\n                                   Ordinal INTEGER,\n                                   TypeName char( 256 ),\n                                   MaxLength INTEGER,\n                                   Precision INTEGER,\n                                   DateTimePrecision INTEGER,\n                                    Scale INTEGER,\n                                   CollationCatalog char( 128 ),\n                                   CollationSchema char( 128 ),\n                                   CollationName char( 128 ),\n                                   CharacterSetCatalog char( 128 ),\n                                   CharacterSetSchema char( 128 ),\n                                   CharacterSetName char( 128 ),\n                                   IsMultiSet LOGICAL,\n                                   [Mode] char( 10 ),\n                                   [Default] memo );\n\n\nOPEN Tables;\n\nWHILE FETCH Tables DO\n\n    OPEN Columns;\n\n    IF Database() = Tables.TABLE_CAT THEN\n        @Schema = NULL;\n        @ParentID = RTRIM( Tables.TABLE_NAME );\n    ELSE\n        @Schema = RTRIM( Tables.TABLE_CAT );\n        @ParentID = @Schema + '_' + RTRIM( Tables.TABLE_NAME );\n    END IF;\n\n    WHILE FETCH Columns DO\n\n        @ID = @ParentID + '_' + RTRIM( Columns.COLUMN_NAME );\n\n        @DataType = LOWER( RTRIM( Columns.LOCAL_TYPE_NAME ) );\n        @IsIdentity = false;\n        @IsStoreGenerated = false;\n\n        IF ( @DataType = 'autoinc' ) THEN\n           @IsIdentity = true;\n           @DataType = 'integer';\n        ENDIF;\n\n        IF ( @DataType = 'rowversion' ) THEN\n           @IsStoreGenerated = true;\n        ENDIF;\n\n        IF ( @DataType = 'modtime' ) THEN\n           @IsStoreGenerated = true;\n        ENDIF;\n\n        IF ( @DataType = 'shortint' ) THEN\n           @DataType = 'short';\n        ENDIF;\n\n        IF ( @DataType = 'binary' ) THEN\n           @DataType = 'blob';\n        ENDIF;\n\n        INSERT INTO\n               #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_oqpBG1cEQUiOhyrNzNa1ww_STableColumns\n            VALUES\n               ( @ID,\n                 @ParentID,\n                 RTRIM( Columns.COLUMN_NAME ),\n                 Columns.ORDINAL_POSITION,\n                 CAST( Columns.NULLABLE AS SQL_BIT ),\n                 @DataType,\n                 Columns.COLUMN_SIZE,\n                 Columns.COLUMN_SIZE,\n                 3,\n                 Columns.DECIMAL_DIGITS,\n                 NULL,\n                 NULL,\n                 NULL,\n                 NULL,\n                 NULL,\n                 NULL,\n                 FALSE,\n                 @IsIdentity,\n                 @IsStoreGenerated,\n                 Columns.COLUMN_DEF );\n\n    END WHILE;\n\n    CLOSE Columns;\n\n\n    OPEN UniqueIndexes;\n\n  \n  -- Skip blank record containing tables statistics at front of table\n  FETCH UniqueIndexes;\n  \n    WHILE FETCH UniqueIndexes DO\n  \n       @ID = @ParentID + '_' + RTRIM( UniqueIndexes.INDEX_NAME ) + '_U';\n  \n     -- Only one entry per unique index in the SConstraints table\n       IF ( UniqueIndexes.ORDINAL_POSITION = 1 ) THEN\n          \n          INSERT INTO \n              #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_yxmlhu1QQRCaH7RRB6Ih2Q_SContraints\n             VALUES\n                ( @ID,\n                  @ParentID,\n                  RTRIM( UniqueIndexes.INDEX_NAME ),\n                  'UNIQUE',\n                  FALSE,\n                  FALSE   );\n     ENDIF;\n     \n   INSERT INTO \n         #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_HjKKob2HSGy7ZBeM716Y0A_SConstraintColumns\n      VALUES\n           ( @ID,\n             @ParentID + '_' + RTRIM( UniqueIndexes.COLUMN_NAME ) );\n           \n   END WHILE;\n\n   CLOSE UniqueIndexes;\n\n    INSERT INTO\n          #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_p1wLQyVUQyCbc7oa7SR1zg_STables\n       VALUES\n          ( @ParentID, NULL, @Schema, RTRIM( Tables.TABLE_NAME ) );\nEND WHILE;\n\nCLOSE Tables;\n\nOPEN Views;\n\nWHILE FETCH Views DO\n\n    OPEN ViewColumns;\n\n    IF Database() = Views.TABLE_CAT THEN\n       @Schema = NULL;\n       @ParentID = RTRIM( Views.TABLE_NAME );\n    ELSE\n       @Schema = RTRIM( Views.TABLE_CAT );\n       @ParentID = @Schema + '_' + RTRIM( Views.TABLE_NAME );\n    END IF;\n\n\n    WHILE FETCH ViewColumns DO\n\n        @ID = @ParentID + '_' + RTRIM( ViewColumns.COLUMN_NAME );\n\n        @DataType = LOWER( RTRIM( ViewColumns.LOCAL_TYPE_NAME ) );\n        @IsIdentity = false;\n        @IsStoreGenerated = false;\n\n        IF ( @DataType = 'autoinc' ) THEN\n           @IsIdentity = true;\n           @DataType = 'integer';\n        ENDIF;\n\n        IF ( @DataType = 'rowversion' ) THEN\n           @IsStoreGenerated = true;\n        ENDIF;\n\n        IF ( @DataType = 'modtime' ) THEN\n           @IsStoreGenerated = true;\n        ENDIF;\n\n        IF ( @DataType = 'shortint' ) THEN\n           @DataType = 'short';\n        ENDIF;\n\n        IF ( @DataType = 'binary' ) THEN\n           @DataType = 'blob';\n        ENDIF;\n\n        INSERT INTO\n               #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_fAMP9jIlSFuvMyHN1woedg_SViewColumns\n           VALUES ( @ID,\n                    @ParentID,\n                    RTRIM( ViewColumns.COLUMN_NAME ),\n                    ViewColumns.ORDINAL_POSITION,\n                    CAST( ViewColumns.NULLABLE AS SQL_BIT ),\n                    @DataType,\n                    ViewColumns.COLUMN_SIZE,\n                    ViewColumns.COLUMN_SIZE,\n                    3,\n                    ViewColumns.DECIMAL_DIGITS,\n                    NULL,\n                    NULL,\n                    NULL,\n                    NULL,\n                    NULL,\n                    NULL,\n                    FALSE,\n                    @IsIdentity,\n                    @IsStoreGenerated,\n                    ViewColumns.COLUMN_DEF );\n    END WHILE;\n\n    CLOSE ViewColumns;\n\n    INSERT INTO\n          #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_WbQIJ2TaREKc05VZnT3Ttg_SViews\n       VALUES( @ParentID,\n               NULL,\n               @Schema,\n               RTRIM( Views.TABLE_NAME ),\n               ( SELECT\n                       View_Stmt\n                    FROM\n                       system.views\n                    WHERE\n                       Name = RTRIM( Views.TABLE_NAME ) ),\n               FALSE );\nEND WHILE;\nOPEN RI;\n\nWHILE FETCH RI DO\n\n   IF Database() = RI.PKTABLE_CAT THEN\n      @Schema = NULL;\n      @ParentID = RTRIM( RI.PKTABLE_NAME  );\n   ELSE\n      @Schema = RTRIM( RI.PKTABLE_CAT );\n      @ParentID = @Schema + '_' + RTRIM( RI.PKTABLE_NAME );\n   END IF;\n  \n   @ID = @ParentID + '_' + RTRIM( RI.PK_NAME ) + '_P';\n   IF ( RI.KEY_SEQ = 0 ) THEN\n       MERGE \n           #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_yxmlhu1QQRCaH7RRB6Ih2Q_SContraints AS C\n         ON \n              ( C.ID = @ID )\n        WHEN MATCHED THEN\n           UPDATE SET ConstraintType = 'PRIMARY KEY'\n        WHEN NOT MATCHED THEN\n             INSERT \n                VALUES\n                   ( @ID,\n                     @ParentID,\n                     RTRIM( RI.PK_NAME ),\n                     'PRIMARY KEY',\n                     FALSE,\n                     FALSE   );\n   ENDIF;\n   \n   @ColumnID = @ParentID + '_' + RTRIM( RI.PKCOLUMN_NAME );\n   \n   MERGE\n         #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_HjKKob2HSGy7ZBeM716Y0A_SConstraintColumns AS cc\n      ON\n         ( cc.ConstraintId = @ID AND \n           cc.ColumnID = @ColumnID )\n      WHEN MATCHED THEN\n         UPDATE SET cc.ConstraintId = @ID \n      WHEN NOT MATCHED THEN\n         INSERT  \n            VALUES\n               ( @ID,\n                   @ColumnID );\n                 \n   IF Database() = RI.FKTABLE_CAT THEN\n      @FSchema = NULL;\n      @FParentID = RTRIM( RI.FKTABLE_NAME  );\n   ELSE\n      @FSchema = RTRIM( RI.FKTABLE_CAT ) + '_';\n      @FParentID = @FSchema + '_' + RTRIM( RI.FKTABLE_NAME );\n   END IF;\n   \n   @FID = @FParentID + '_' + RTRIM( RI.FK_NAME ) + '_F';\n   IF ( RI.KEY_SEQ = 0 ) THEN\n   \n      INSERT INTO \n            #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_yxmlhu1QQRCaH7RRB6Ih2Q_SContraints\n         VALUES\n            ( @FID,\n              @FParentID,\n              RTRIM( RI.FK_NAME ),\n              'FOREIGN KEY',\n              FALSE,\n              FALSE );\n                                   \n      INSERT INTO\n            #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_2BQ1qv15Qg2jNWEJRRLFIw_SForeignKeyConstraints\n         VALUES\n            (  @FID, \n               CASE RI.UPDATE_RULE\n                  WHEN 0 THEN 'NO ACTION'\n                  WHEN 1 THEN 'CASCADE'\n                  WHEN 2 THEN 'SET NULL'\n                  ELSE        'SET DEFAULT'\n               END,\n               CASE RI.DELETE_RULE\n                  WHEN 0 THEN 'NO ACTION'\n                  WHEN 1 THEN 'CASCADE'\n                  WHEN 2 THEN 'SET NULL'\n                  ELSE        'SET DEFAULT'\n                END );         \n   ENDIF;\n                   \n   INSERT INTO \n         #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_KlYebalJROGCtfxDWYbCeQ_SForeignKeys\n      VALUES\n         ( RTRIM( RI.NAME ) + \n           ( RTRIM( CAST( RI.KEY_SEQ AS SQL_CHAR( 30 ) ) ) ) COLLATE ads_default_ci, \n           @ColumnID,\n           @FParentID + '_' + RTRIM( RI.FKCOLUMN_NAME ),\n           @FID,\n             RI.KEY_SEQ + 1 );\n                                      \nEND WHILE;\n\n\nOPEN WorkCursor AS\n   SELECT\n         *\n      FROM\n         #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_p1wLQyVUQyCbc7oa7SR1zg_STables tables\n      WHERE\n         tables.ID NOT IN (\n                            SELECT\n                                  ParentID\n                               FROM\n                                  #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_yxmlhu1QQRCaH7RRB6Ih2Q_SContraints\n                               WHERE\n                                  ConstraintType = 'PRIMARY KEY'\n                           );\n\nWHILE FETCH WorkCursor DO\n\n   OPEN PrimaryKeys;\n   WHILE FETCH PrimaryKeys DO\n\n      IF Database() = PrimaryKeys.TABLE_CAT THEN\n         @Schema = NULL;\n         @ParentID = RTRIM( PrimaryKeys.TABLE_NAME  );\n      ELSE\n         @Schema = RTRIM( PrimaryKeys.TABLE_CAT );\n         @ParentID = @Schema + '_' + RTRIM( PrimaryKeys.TABLE_NAME );\n      END IF;\n\n      @ID = @ParentID + '_' + RTRIM( PrimaryKeys.PK_NAME ) + '_P';\n      IF ( PrimaryKeys.KEY_SEQ = 1 ) THEN\n          MERGE\n              #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_yxmlhu1QQRCaH7RRB6Ih2Q_SContraints AS C\n            ON\n                 ( C.ID = @ID )\n           WHEN MATCHED THEN\n              UPDATE SET ConstraintType = 'PRIMARY KEY'\n           WHEN NOT MATCHED THEN\n                INSERT\n                   VALUES\n                      ( @ID,\n                        @ParentID,\n                        RTRIM( PrimaryKeys.PK_NAME ),\n                        'PRIMARY KEY',\n                        FALSE,\n                        FALSE   );\n      ENDIF;\n\n      @ColumnID = @ParentID + '_' + RTRIM( PrimaryKeys.COLUMN_NAME );\n\n      MERGE\n            #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_HjKKob2HSGy7ZBeM716Y0A_SConstraintColumns AS cc\n         ON\n            ( cc.ConstraintId = @ID AND\n              cc.ColumnID = @ColumnID )\n         WHEN MATCHED THEN\n            UPDATE SET cc.ConstraintId = @ID\n         WHEN NOT MATCHED THEN\n            INSERT\n               VALUES\n                  ( @ID,\n                   @ColumnID );\n\n   END WHILE;\n\n   CLOSE PrimaryKeys;\n\nEND WHILE;\n\nOPEN Procedures;\n\nWHILE FETCH Procedures DO\n\n    IF Procedures.PROCEDURE_TYPE <> 2 THEN\n        CONTINUE;\n    END IF;\n\n    OPEN ProcColumns;\n\n    IF Database() = Procedures.PROCEDURE_CAT THEN\n       @Schema = NULL;\n       @ParentID = RTRIM( Procedures.PROCEDURE_NAME );\n    ELSE\n       @Schema = RTRIM( Procedures.PROCEDURE_CAT );\n       @ParentID = @Schema + '_' + RTRIM( Procedures.PROCEDURE_NAME );\n    END IF;\n\n    @Position = 1;\n    WHILE FETCH ProcColumns DO\n    \n        IF ProcColumns.COLUMN_TYPE <> 1 THEN\n             CONTINUE;\n        END IF;\n\n        @ID = @ParentID + '_' + RTRIM( ProcColumns.COLUMN_NAME );\n\n        @DataType = LOWER( RTRIM( ProcColumns.TYPE_NAME ) );\n\n        IF ( @DataType = 'shortint' ) THEN\n           @DataType = 'short';\n        ENDIF;\n\n        IF ( @DataType = 'binary' ) THEN\n           @DataType = 'blob';\n        ENDIF;\n\n        INSERT INTO\n               #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_VAy9VFx0SHadO67Dsy5keQBB_SProcedureParameters\n           VALUES ( @ID,\n                    @ParentID,\n                    RTRIM( ProcColumns.COLUMN_NAME ),\n                    @Position,                    \n                    @DataType,\n                    ProcColumns.PRECISION,\n                    ProcColumns.SCALE,\n                    3,\n                    ProcColumns.PRECISION,\n                    NULL,\n                    NULL,\n                    NULL,\n                    NULL,\n                    NULL,\n                    NULL,\n                    FALSE,\n                    CASE ProcColumns.COLUMN_TYPE\n                       WHEN 1 THEN 'IN'\n                       WHEN 2 THEN 'INOUT'\n                       WHEN 3 THEN 'IN'\n                       WHEN 4 THEN 'IN'\n                       WHEN 5 THEN 'RETURN'\n                       ELSE        'UNKNOWN'                                                                                                                   \n                    END,\n                    NULL );\n       \n       @Position = @Position + 1;\n                                \n    END WHILE;\n\n    CLOSE ProcColumns;\n\n    INSERT INTO\n          #NEawjtV9QSyEXa5XICWIBg_9OJutwEmTZel3MBWeAxcuA_p6veFSIeS4mIKEByRvQhegAA_SProcedures\n       VALUES( @ParentID,\n               NULL,\n               @Schema,\n               RTRIM( Procedures.PROCEDURE_NAME ) );\n               \nEND WHILE;\n\n\n";
            AdsException.CheckACE(ACE.AdsStmtSetTableType(this.mStmt.mhHandle, (ushort)3));
            this.mConnection.TableType = (ushort)3;
            AdsException.CheckACE(ACE.AdsExecuteSQLDirect(this.mStmt.mhHandle, pucSQL, out phCursor));
            this.mConnection.mbEntityTablesGenerated = true;
        }

        public void Unprepare()
        {
            if (!this.mbPrepared || !(this.mStmt.mhHandle != IntPtr.Zero))
                return;
            if (this.mConnection != null && this.mConnection.State == ConnectionState.Open)
            {
                AdsException.CheckACE(ACE.AdsPrepareSQL(this.mStmt.mhHandle, "CLOSE"));
                AdsException.CheckACE(ACE.AdsCloseSQLStatement(this.mStmt.mhHandle));
                this.mConnection.RemoveProxyHandle(this.mStmt);
            }

            this.mStmt.mhHandle = IntPtr.Zero;
        }

        public override void Prepare()
        {
            this.InternalPrepare();
            if (this.mbPrepared || this.mCommandType == CommandType.TableDirect)
                return;
            if (this.mCommandType == CommandType.StoredProcedure)
            {
                Match match = new Regex("(\\[::this\\]|\"::this\")\\.(.+)").Match(this.mstrCmdText);
                string str = !match.Success ? this.mstrCmdText : match.Groups[2].Value;
                if (str[0] != '"' && str[0] != '[')
                    str = string.Format("[{0}]", (object)str);
                StringBuilder stringBuilder = new StringBuilder("execute procedure " + str + "(");
                int num = 0;
                for (int index = 0; index < this.mParameters.Count; ++index)
                {
                    AdsParameter mParameter = this.mParameters[index];
                    if (mParameter.Direction == ParameterDirection.Input)
                    {
                        if (num > 0)
                            stringBuilder.Append(", ");
                        if (mParameter.ParameterName != null && mParameter.ParameterName != "")
                            stringBuilder.Append(":" + mParameter.ParameterName);
                        else
                            stringBuilder.Append("?");
                        ++num;
                    }
                }

                stringBuilder.Append(")");
                this.mstrProcCall = stringBuilder.ToString();
            }
            else if (this.mCommandType == CommandType.Text && !this.mConnection.mbEntityTablesGenerated &&
                     this.mstrCmdText.IndexOf(this.mstrEnityTablePrefix) > -1)
                this.GenerateEntityMetadataTables();

            AdsException.CheckACE(this.mCommandType != CommandType.StoredProcedure
                ? ACE.AdsPrepareSQLW(this.mStmt.mhHandle, this.mstrCmdText)
                : ACE.AdsPrepareSQLW(this.mStmt.mhHandle, this.mstrProcCall));
            this.mbPrepared = true;
        }

        private void SetParameters()
        {
            string str = "";
            try
            {
                for (int index = 0; index < this.mParameters.Count; ++index)
                {
                    AdsParameter mParameter = this.mParameters[index];
                    if (mParameter.Direction == ParameterDirection.Input)
                    {
                        str = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                            ? Convert.ToString(mParameter.Index)
                            : mParameter.ParameterName;
                        uint ulRet;
                        if (mParameter.IsNull)
                        {
                            ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                ? ACE.AdsSetEmpty(this.mStmt.mhHandle, (uint)mParameter.Index)
                                : ACE.AdsSetEmpty(this.mStmt.mhHandle, mParameter.ParameterName);
                        }
                        else
                        {
                            if (mParameter.Value == null)
                                throw new ArgumentException("Parameter value has not been set (parameter " + str +
                                                            ").");
                            switch (mParameter.DbType)
                            {
                                case DbType.AnsiString:
                                case DbType.String:
                                case DbType.AnsiStringFixedLength:
                                case DbType.StringFixedLength:
                                    uint ulLen = mParameter.Size != -1
                                        ? (uint)Math.Min(mParameter.Value.ToString().Length, mParameter.Size)
                                        : (uint)mParameter.Value.ToString().Length;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetStringW(this.mStmt.mhHandle, (uint)mParameter.Index,
                                            mParameter.Value.ToString(), ulLen)
                                        : ACE.AdsSetStringW(this.mStmt.mhHandle, mParameter.ParameterName,
                                            mParameter.Value.ToString(), ulLen);
                                    break;
                                case DbType.Binary:
                                    byte[] pucBuf1 = (byte[])mParameter.Value;
                                    uint num1 = mParameter.Size != -1
                                        ? (uint)Math.Min(pucBuf1.Length, mParameter.Size)
                                        : (uint)pucBuf1.Length;
                                    ulRet = num1 != 0U
                                        ? (mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                            ? ACE.AdsSetBinary(this.mStmt.mhHandle, (uint)mParameter.Index, (ushort)6,
                                                num1, 0U, pucBuf1, num1)
                                            : ACE.AdsSetBinary(this.mStmt.mhHandle, mParameter.ParameterName, (ushort)6,
                                                num1, 0U, pucBuf1, num1))
                                        : (mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                            ? ACE.AdsSetEmpty(this.mStmt.mhHandle, (uint)mParameter.Index)
                                            : ACE.AdsSetEmpty(this.mStmt.mhHandle, mParameter.ParameterName));
                                    break;
                                case DbType.Byte:
                                    byte sValue1 = (byte)mParameter.Value;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetShort(this.mStmt.mhHandle, (uint)mParameter.Index, (short)sValue1)
                                        : ACE.AdsSetShort(this.mStmt.mhHandle, mParameter.ParameterName,
                                            (short)sValue1);
                                    break;
                                case DbType.Boolean:
                                    ushort bValue = !(bool)mParameter.Value ? (ushort)0 : (ushort)1;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetLogical(this.mStmt.mhHandle, (uint)mParameter.Index, bValue)
                                        : ACE.AdsSetLogical(this.mStmt.mhHandle, mParameter.ParameterName, bValue);
                                    break;
                                case DbType.Currency:
                                case DbType.Decimal:
                                    string pwcBuf = Convert.ToDecimal(mParameter.Value)
                                        .ToString((IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetStringW(this.mStmt.mhHandle, (uint)mParameter.Index, pwcBuf,
                                            (uint)pwcBuf.Length)
                                        : ACE.AdsSetStringW(this.mStmt.mhHandle, mParameter.ParameterName, pwcBuf,
                                            (uint)pwcBuf.Length);
                                    break;
                                case DbType.Date:
                                case DbType.DateTime:
                                    DateTime dateTime = (DateTime)mParameter.Value;
                                    string pucJulian = dateTime.ToString("yyyyMMdd");
                                    double pdJulian;
                                    AdsException.CheckACE(ACEUNPUB.AdsConvertStringToJulian(pucJulian,
                                        (ushort)pucJulian.Length, out pdJulian));
                                    int lDate = (int)pdJulian;
                                    if (mParameter.DbType == DbType.Date)
                                    {
                                        ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                            ? ACE.AdsSetJulian(this.mStmt.mhHandle, (uint)mParameter.Index, lDate)
                                            : ACE.AdsSetJulian(this.mStmt.mhHandle, mParameter.ParameterName, lDate);
                                        break;
                                    }

                                    ulong num2 = (ulong)lDate;
                                    ulong num3 =
                                        (ulong)(((dateTime.Hour * 60 + dateTime.Minute) * 60 + dateTime.Second) * 1000 +
                                                dateTime.Millisecond);
                                    ulong pucBuf2 = !BitConverter.IsLittleEndian
                                        ? num2 << 32 | num3
                                        : num3 << 32 | num2;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACEUNPUB.AdsSetTimeStampRaw(this.mStmt.mhHandle, (uint)mParameter.Index,
                                            ref pucBuf2, 8U)
                                        : ACEUNPUB.AdsSetTimeStampRaw(this.mStmt.mhHandle, mParameter.ParameterName,
                                            ref pucBuf2, 8U);
                                    break;
                                case DbType.Double:
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetDouble(this.mStmt.mhHandle, (uint)mParameter.Index,
                                            (double)mParameter.Value)
                                        : ACE.AdsSetDouble(this.mStmt.mhHandle, mParameter.ParameterName,
                                            (double)mParameter.Value);
                                    break;
                                case DbType.Guid:
                                    throw new ArgumentException("Guid is not supported (parameter " + str + ").");
                                case DbType.Int16:
                                    short lValue1 = (short)mParameter.Value;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetLong(this.mStmt.mhHandle, (uint)mParameter.Index, (int)lValue1)
                                        : ACE.AdsSetLong(this.mStmt.mhHandle, mParameter.ParameterName, (int)lValue1);
                                    break;
                                case DbType.Int32:
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetLong(this.mStmt.mhHandle, (uint)mParameter.Index,
                                            (int)mParameter.Value)
                                        : ACE.AdsSetLong(this.mStmt.mhHandle, mParameter.ParameterName,
                                            (int)mParameter.Value);
                                    break;
                                case DbType.Int64:
                                    long int64 = Convert.ToInt64(mParameter.Value);
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetLongLong(this.mStmt.mhHandle, (uint)mParameter.Index, int64)
                                        : ACE.AdsSetLongLong(this.mStmt.mhHandle, mParameter.ParameterName, int64);
                                    break;
                                case DbType.Object:
                                    throw new ArgumentException("Unable to infer parameter data type (parameter " +
                                                                str + ").  Set parameter's DbType.");
                                case DbType.SByte:
                                    sbyte sValue2 = (sbyte)mParameter.Value;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetShort(this.mStmt.mhHandle, (uint)mParameter.Index, (short)sValue2)
                                        : ACE.AdsSetShort(this.mStmt.mhHandle, mParameter.ParameterName,
                                            (short)sValue2);
                                    break;
                                case DbType.Single:
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetDouble(this.mStmt.mhHandle, (uint)mParameter.Index,
                                            (double)(float)mParameter.Value)
                                        : ACE.AdsSetDouble(this.mStmt.mhHandle, mParameter.ParameterName,
                                            (double)(float)mParameter.Value);
                                    break;
                                case DbType.Time:
                                    TimeSpan timeSpan = (TimeSpan)mParameter.Value;
                                    int lTime = ((timeSpan.Hours * 60 + timeSpan.Minutes) * 60 + timeSpan.Seconds) *
                                        1000 + timeSpan.Milliseconds;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetMilliseconds(this.mStmt.mhHandle, (uint)mParameter.Index, lTime)
                                        : ACE.AdsSetMilliseconds(this.mStmt.mhHandle, mParameter.ParameterName, lTime);
                                    break;
                                case DbType.UInt16:
                                    ushort lValue2 = (ushort)mParameter.Value;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetLong(this.mStmt.mhHandle, (uint)mParameter.Index, (int)lValue2)
                                        : ACE.AdsSetLong(this.mStmt.mhHandle, mParameter.ParameterName, (int)lValue2);
                                    break;
                                case DbType.UInt32:
                                    uint dValue = (uint)mParameter.Value;
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetDouble(this.mStmt.mhHandle, (uint)mParameter.Index, (double)dValue)
                                        : ACE.AdsSetDouble(this.mStmt.mhHandle, mParameter.ParameterName,
                                            (double)dValue);
                                    break;
                                case DbType.UInt64:
                                    ulong uint64 = Convert.ToUInt64(mParameter.Value);
                                    ulRet = mParameter.ParameterName == null || !(mParameter.ParameterName != "")
                                        ? ACE.AdsSetLongLong(this.mStmt.mhHandle, (uint)mParameter.Index, (long)uint64)
                                        : ACE.AdsSetLongLong(this.mStmt.mhHandle, mParameter.ParameterName,
                                            (long)uint64);
                                    break;
                                case DbType.VarNumeric:
                                    throw new ArgumentException("VarNumeric is not supported (parameter " + str + ").");
                                default:
                                    throw new ArgumentException("Unrecognized parameter data type (parameter " + str +
                                                                ").");
                            }
                        }

                        AdsException.CheckACE(ulRet);
                    }
                }
            }
            catch (InvalidCastException ex)
            {
                throw new AdsException("Invalid DbType specified for parameter " + str + ".", (Exception)ex);
            }
            catch (FormatException ex)
            {
                throw new AdsException("Invalid DbType specified for parameter " + str + ".", (Exception)ex);
            }
        }

        internal static DbType ConvertACETypeNameToDbType(string strType)
        {
            switch (strType.ToLower().Trim())
            {
                case "logical":
                    return DbType.Boolean;
                case "char":
                case "character":
                case "memo":
                case "varchar":
                case "cichar":
                case "cicharacter":
                case "nchar":
                case "nmemo":
                case "nvarchar":
                case "varcharfox":
                    return DbType.String;
                case "numeric":
                    return DbType.Decimal;
                case "date":
                    return DbType.Date;
                case "timestamp":
                case "modtime":
                    return DbType.DateTime;
                case "binary":
                case "raw":
                    return DbType.Binary;
                case "double":
                    return DbType.Double;
                case "integer":
                    return DbType.Int32;
                case "short":
                case "shortint":
                    return DbType.Int16;
                case "time":
                    return DbType.Time;
                case "autoinc":
                    return DbType.Int32;
                case "curdouble":
                    return DbType.Double;
                case "money":
                    return DbType.Currency;
                case "rowversion":
                    return DbType.Int64;
                default:
                    throw new ArgumentException("Unrecognized stored procedure parameter type '" + strType + "'.");
            }
        }

        private void AddSPParams(
            string strParams,
            ParameterDirection eDir,
            ref AdsParameterCollection SPParams)
        {
            string str1 = strParams;
            char[] chArray1 = new char[1] { ';' };
            foreach (string str2 in str1.Split(chArray1))
            {
                char[] chArray2 = new char[1] { ',' };
                string[] strArray = str2.Split(chArray2);
                if (strArray.Length > 1)
                {
                    DbType dbType = AdsCommand.ConvertACETypeNameToDbType(strArray[1]);
                    AdsParameter adsParameter = new AdsParameter(strArray[0], dbType);
                    adsParameter.Direction = eDir;
                    SPParams.Add(adsParameter);
                }
            }
        }

        private void LoadSPParams(ParameterDirection eDir, ref AdsParameterCollection SPParams)
        {
            char[] pucProperty = new char[2048];
            ushort usPropertyID = eDir != ParameterDirection.Input ? (ushort)801 : (ushort)800;
            ushort length = (ushort)pucProperty.Length;
            uint procedureProperty = ACE.AdsDDGetProcedureProperty(this.mConnection.Handle, this.mstrCmdText,
                usPropertyID, pucProperty, ref length);
            if (procedureProperty == 5005U)
            {
                pucProperty = new char[(int)length + 1];
                length = (ushort)pucProperty.Length;
                procedureProperty = ACE.AdsDDGetProcedureProperty(this.mConnection.Handle, this.mstrCmdText,
                    usPropertyID, pucProperty, ref length);
            }

            if (procedureProperty != 0U && procedureProperty != 5138U)
                AdsException.CheckACE(procedureProperty);
            if (procedureProperty == 5138U)
                return;
            if (length > (ushort)0 && pucProperty[(int)length - 1] == char.MinValue)
                --length;
            this.AddSPParams(new string(pucProperty, 0, (int)length), eDir, ref SPParams);
        }

        private void LoadSystemProcedureParameter(ref AdsParameterCollection SPParams)
        {
            AdsCommand command = this.mConnection.CreateCommand();
            command.CommandText = "select * from System.SystemProcedures where Name = ?";
            command.Parameters.Add(1, (object)this.mstrCmdText);
            AdsDataReader adsDataReader = command.ExecuteReader();
            try
            {
                if (!adsDataReader.Read())
                    throw new ArgumentException(string.Format("{0} is not recognized as a system procedure.",
                        (object)this.mstrCmdText));
                if (!(adsDataReader["Proc_Input"] is DBNull))
                    this.AddSPParams((string)adsDataReader["Proc_Input"], ParameterDirection.Input, ref SPParams);
                if (adsDataReader["Proc_Output"] is DBNull)
                    return;
                this.AddSPParams((string)adsDataReader["Proc_Output"], ParameterDirection.Output, ref SPParams);
            }
            finally
            {
                adsDataReader.Close();
                command.Dispose();
            }
        }

        private void InternalLoadParameterInformation(out AdsParameterCollection SPParams)
        {
            SPParams = (AdsParameterCollection)null;
            if (this.mstrCmdText == null || this.mCommandType != CommandType.StoredProcedure)
                return;
            SPParams = new AdsParameterCollection();
            try
            {
                this.LoadSPParams(ParameterDirection.Input, ref SPParams);
                this.LoadSPParams(ParameterDirection.Output, ref SPParams);
            }
            catch (AdsException ex)
            {
                try
                {
                    this.LoadSystemProcedureParameter(ref SPParams);
                }
                catch
                {
                    throw ex;
                }
            }
        }

        public void DeriveParameters()
        {
            AdsParameterCollection SPParams = (AdsParameterCollection)null;
            if (this.mCommandType != CommandType.StoredProcedure)
                throw new ArgumentException(
                    "The CommandType must be StoredProcedure in order to automatically load parameter information.");
            if (this.mstrCmdText == null)
                throw new ArgumentException("The stored procedure name has not be supplied.");
            this.InternalLoadParameterInformation(out SPParams);
            this.mParameters.Clear();
            foreach (AdsParameter adsParameter in (DbParameterCollection)SPParams)
                this.mParameters.Add(new AdsParameter(adsParameter.ParameterName, adsParameter.DbType,
                    adsParameter.Size, adsParameter.Direction, adsParameter.IsNullable, adsParameter.Precision,
                    adsParameter.Scale, adsParameter.SourceColumn, adsParameter.SourceVersion, (object)DBNull.Value));
        }

        public void VerifySQL(string strSQL)
        {
            this.InternalPrepare();
            AdsException.CheckACE(ACE.AdsVerifySQLW(this.mStmt.mhHandle, strSQL));
        }

        public int LastAutoinc
        {
            get
            {
                this.InternalPrepare();
                uint pulAutoIncVal;
                AdsException.CheckACE(ACE.AdsGetLastAutoinc(this.mStmt.mhHandle, out pulAutoIncVal));
                return (int)pulAutoIncVal;
            }
        }
    }
}