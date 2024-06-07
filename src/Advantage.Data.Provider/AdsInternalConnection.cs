using AdvantageClientEngine;
using System;
using System.Data;
using System.Text;
using System.Transactions;

namespace Advantage.Data.Provider
{
    internal class AdsInternalConnection : IEnlistmentNotification, IDisposable
    {
        protected bool mbDisposed;
        private IntPtr mhConnect = IntPtr.Zero;
        private string mstrConnectionString;
        private ConnectionState mState;
        private ushort musHandleType;
        private string mstrDataSource;
        private string mstrInitialCatalog;
        private string mstrConnectPath;
        private string mstrConnect101;
        private bool mbShowDeleted;
        private string mstrEncryptionPassword;
        private ushort musCheckRights;
        private ushort musLockType;
        private string mstrCharType;
        private string mstrUnicodeCollation;
        private ushort musTableType;
        private AdsConnectionStringHandler.FilterOption meFilterOptions;
        private bool mbDbfsUseNulls;
        private bool mbTrimTrailingSpaces;
        private uint muiOpenOptions;
        private IntPtr mhInitConnect = IntPtr.Zero;
        private AdsTransaction mCurrentTransaction;
        private bool mbDisposeOnCommit;
        private bool mbEnlist = true;

        public AdsInternalConnection() => throw new NotSupportedException();

        public AdsInternalConnection(string strConnectionString, AdsConnectionStringHandler handler)
        {
            this.mhConnect = IntPtr.Zero;
            this.mState = ConnectionState.Closed;
            this.mstrConnectionString = strConnectionString;
            this.ExtractConnectionPropsFromHash(handler);
        }

        ~AdsInternalConnection()
        {
            if (this is AdsPooledInternalConnection)
                ((AdsPooledInternalConnection)this).DecrementPoolOpenCount();
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        public virtual void Dispose(bool bDisposing)
        {
            if (this.mbDisposed)
                return;
            lock (this)
            {
                if (this.mbDisposed)
                    return;
                this.mbDisposed = true;
                int num = bDisposing ? 1 : 0;
                this.Disconnect();
            }
        }

        private void AddProperty(StringBuilder strCS, string strName, string strValue)
        {
            switch (strValue)
            {
                case null:
                    break;
                case "":
                    break;
                default:
                    strCS.Append(strName);
                    strCS.Append("=");
                    strCS.Append(strValue);
                    strCS.Append(";");
                    break;
            }
        }

        private void ExtractConnectionPropsFromHash(AdsConnectionStringHandler handler)
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            this.musTableType = handler.TableType;
            this.mstrCharType = handler.CharType;
            this.mstrUnicodeCollation = handler.UnicodeCollation;
            this.musLockType = handler.LockType;
            this.musCheckRights = handler.CheckRights;
            this.mbShowDeleted = handler.ShowDeleted;
            this.mstrEncryptionPassword = handler.EncryptionPassword;
            this.mbDbfsUseNulls = handler.DbfsUseNulls;
            this.meFilterOptions = handler.FilterOptions;
            this.mbTrimTrailingSpaces = handler.TrimTrailingSpaces;
            this.muiOpenOptions = !handler.Shared ? 1U : 4U;
            if (handler.ReadOnly)
                this.muiOpenOptions |= 2U;
            this.mstrDataSource = handler.DataSource;
            this.mstrInitialCatalog = handler.InitialCatalog;
            this.mbEnlist = handler.TransScopeEnlist;
            if (handler.HaveConnectionHandle)
            {
                this.mhInitConnect = handler.ConnectionHandle;
            }
            else
            {
                this.CreateConnectionPath(this.mstrDataSource, this.mstrInitialCatalog, out this.mstrConnectPath);
                if (this.mstrConnectPath == null || this.mstrConnectPath == "")
                    throw new ArgumentException("A Data Source must be provided with the connection string.");
            }

            StringBuilder strCS = new StringBuilder();
            this.AddProperty(strCS, "Data Source", this.mstrConnectPath);
            this.AddProperty(strCS, "User ID", handler.UserID);
            this.AddProperty(strCS, "Password", handler.Password);
            this.AddProperty(strCS, "ServerType", handler.ServerType);
            this.AddProperty(strCS, "CommType", handler.CommType);
            this.AddProperty(strCS, "Compression", handler.Compression);
            if (handler.IncUserCount)
                this.AddProperty(strCS, "IncrementUserCount", "TRUE");
            if (handler.StoredProcConn)
                this.AddProperty(strCS, "StoredProcedureConnection", "TRUE");
            this.AddProperty(strCS, "EncryptionType", handler.EncryptionType);
            if (handler.FIPSMode)
                this.AddProperty(strCS, "FIPS", "TRUE");
            this.AddProperty(strCS, "DDPassword", handler.DDPassword);
            this.AddProperty(strCS, "TLSCiphers", handler.TLSCiphers);
            this.AddProperty(strCS, "TLSCertificate", handler.TLSCertificate);
            this.AddProperty(strCS, "TLSCommonName", handler.TLSCommonName);
            this.mstrConnect101 = strCS.ToString();
        }

        private void CreateConnectionPath(
            string strDataSource,
            string strCatalog,
            out string strConnect)
        {
            strConnect = "";
            if (strCatalog == null)
                strConnect = strDataSource;
            else if (strDataSource == null)
                strConnect = strCatalog;
            else if (strDataSource.EndsWith(".add") || strDataSource.EndsWith(".ADD"))
            {
                strConnect = strDataSource;
            }
            else
            {
                strConnect = strDataSource;
                if (!strConnect.EndsWith("\\") && !strConnect.EndsWith("/"))
                    strConnect += "\\";
                strConnect += strCatalog;
            }
        }

        public virtual void Connect()
        {
            uint num = 0;
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            if (this.mState == ConnectionState.Open)
                return;
            if (this.mhInitConnect != IntPtr.Zero)
                this.mhConnect = this.mhInitConnect;
            else
                num = ACE.AdsConnect101(this.mstrConnect101, IntPtr.Zero, out this.mhConnect);
            if (num != 0U)
                throw new AdsException();
            AdsException.CheckACE(ACE.AdsGetHandleType(this.mhConnect, out this.musHandleType));
            if (this.mhInitConnect == IntPtr.Zero)
                AdsException.CheckACE(ACE.AdsShowDeleted(this.mbShowDeleted ? (ushort)1 : (ushort)0));
            this.mState = ConnectionState.Open;
        }

        public virtual void Disconnect()
        {
            if (this.mState != ConnectionState.Open || this.mhInitConnect != IntPtr.Zero)
                return;
            this.mState = ConnectionState.Closed;
            uint num = ACE.AdsDisconnect(this.mhConnect);
            this.mhConnect = IntPtr.Zero;
            if (num != 0U)
                throw new AdsException();
        }

        internal void EnlistTransaction(Transaction transaction, AdsConnection conn)
        {
            if (!(transaction != (Transaction)null))
                return;
            if (this.mCurrentTransaction != null)
                throw new InvalidOperationException("Transaction is already active.");
            transaction.EnlistVolatile((IEnlistmentNotification)this, EnlistmentOptions.None);
            this.mCurrentTransaction = conn.BeginTransaction();
        }

        private void DisposeOrReturnToPool()
        {
            if (this is AdsPooledInternalConnection)
                AdsPooledInternalConnection.ReturnConnectionToPool(this as AdsPooledInternalConnection);
            else
                this.Dispose();
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            try
            {
                if (this.mCurrentTransaction != null)
                {
                    this.mCurrentTransaction.InternalCommit(this.mhConnect);
                    this.mCurrentTransaction.Dispose();
                    this.mCurrentTransaction = (AdsTransaction)null;
                }

                preparingEnlistment.Prepared();
            }
            catch
            {
                preparingEnlistment.ForceRollback();
                if (this.mCurrentTransaction != null)
                {
                    this.mCurrentTransaction.InternalRollback(this.mhConnect);
                    this.mCurrentTransaction.Dispose();
                    this.mCurrentTransaction = (AdsTransaction)null;
                }

                if (!this.mbDisposeOnCommit)
                    return;
                this.DisposeOrReturnToPool();
            }
        }

        public void Commit(Enlistment enlistment)
        {
            if (this.mbDisposeOnCommit)
                this.DisposeOrReturnToPool();
            enlistment.Done();
        }

        public void Rollback(Enlistment enlistment)
        {
            if (this.mCurrentTransaction != null)
            {
                this.mCurrentTransaction.InternalRollback(this.mhConnect);
                this.mCurrentTransaction.Dispose();
                this.mCurrentTransaction = (AdsTransaction)null;
            }

            if (this.mbDisposeOnCommit)
                this.DisposeOrReturnToPool();
            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment) => enlistment.Done();

        public virtual void Reset()
        {
            int num = (int)ACE.AdsResetConnection(this.mhConnect);
        }

        public bool TrimTrailingSpaces
        {
            get => this.mbTrimTrailingSpaces;
            set => this.mbTrimTrailingSpaces = value;
        }

        public IntPtr Handle => this.mhConnect;

        public uint TableOpenOptions => this.muiOpenOptions;

        public ushort TableType => this.musTableType;

        public ushort LockType => this.musLockType;

        public ushort RightsChecking => this.musCheckRights;

        public string CharType => this.mstrCharType;

        public string UnicodeCollation => this.mstrUnicodeCollation;

        public string ConnectionString => this.mstrConnectionString;

        public ConnectionState State => this.mState;

        public ushort HandleType => this.musHandleType;

        public string ConnectionPath => this.mstrConnectPath;

        public string InitialCatalog => this.mstrInitialCatalog;

        public bool DbfsUseNulls => this.mbDbfsUseNulls;

        public string EncryptionPassword
        {
            get => this.mstrEncryptionPassword == null ? "" : this.mstrEncryptionPassword;
        }

        public AdsTransaction CurrentTransaction => this.mCurrentTransaction;

        public bool DisposeOnCommit
        {
            set => this.mbDisposeOnCommit = value;
        }

        public bool TransScopeEnlist => this.mbEnlist;
    }
}