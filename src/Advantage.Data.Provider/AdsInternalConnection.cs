using System;
using System.Data;
using System.Text;
using System.Transactions;
using AdvantageClientEngine;

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
            mhConnect = IntPtr.Zero;
            mState = ConnectionState.Closed;
            mstrConnectionString = strConnectionString;
            ExtractConnectionPropsFromHash(handler);
        }

        ~AdsInternalConnection()
        {
            if (this is AdsPooledInternalConnection)
                ((AdsPooledInternalConnection)this).DecrementPoolOpenCount();
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool bDisposing)
        {
            if (mbDisposed)
                return;
            lock (this)
            {
                if (mbDisposed)
                    return;
                mbDisposed = true;
                var num = bDisposing ? 1 : 0;
                Disconnect();
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
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            musTableType = handler.TableType;
            mstrCharType = handler.CharType;
            mstrUnicodeCollation = handler.UnicodeCollation;
            musLockType = handler.LockType;
            musCheckRights = handler.CheckRights;
            mbShowDeleted = handler.ShowDeleted;
            mstrEncryptionPassword = handler.EncryptionPassword;
            mbDbfsUseNulls = handler.DbfsUseNulls;
            meFilterOptions = handler.FilterOptions;
            mbTrimTrailingSpaces = handler.TrimTrailingSpaces;
            muiOpenOptions = !handler.Shared ? 1U : 4U;
            if (handler.ReadOnly)
                muiOpenOptions |= 2U;
            mstrDataSource = handler.DataSource;
            mstrInitialCatalog = handler.InitialCatalog;
            mbEnlist = handler.TransScopeEnlist;
            if (handler.HaveConnectionHandle)
            {
                mhInitConnect = handler.ConnectionHandle;
            }
            else
            {
                CreateConnectionPath(mstrDataSource, mstrInitialCatalog, out mstrConnectPath);
                if (mstrConnectPath == null || mstrConnectPath == "")
                    throw new ArgumentException("A Data Source must be provided with the connection string.");
            }

            var strCS = new StringBuilder();
            AddProperty(strCS, "Data Source", mstrConnectPath);
            AddProperty(strCS, "User ID", handler.UserID);
            AddProperty(strCS, "Password", handler.Password);
            AddProperty(strCS, "ServerType", handler.ServerType);
            AddProperty(strCS, "CommType", handler.CommType);
            AddProperty(strCS, "Compression", handler.Compression);
            if (handler.IncUserCount)
                AddProperty(strCS, "IncrementUserCount", "TRUE");
            if (handler.StoredProcConn)
                AddProperty(strCS, "StoredProcedureConnection", "TRUE");
            AddProperty(strCS, "EncryptionType", handler.EncryptionType);
            if (handler.FIPSMode)
                AddProperty(strCS, "FIPS", "TRUE");
            AddProperty(strCS, "DDPassword", handler.DDPassword);
            AddProperty(strCS, "TLSCiphers", handler.TLSCiphers);
            AddProperty(strCS, "TLSCertificate", handler.TLSCertificate);
            AddProperty(strCS, "TLSCommonName", handler.TLSCommonName);
            mstrConnect101 = strCS.ToString();
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
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            if (mState == ConnectionState.Open)
                return;
            if (mhInitConnect != IntPtr.Zero)
                mhConnect = mhInitConnect;
            else
                num = ACE.AdsConnect101(mstrConnect101, IntPtr.Zero, out mhConnect);
            if (num != 0U)
                throw new AdsException();
            AdsException.CheckACE(ACE.AdsGetHandleType(mhConnect, out musHandleType));
            if (mhInitConnect == IntPtr.Zero)
                AdsException.CheckACE(ACE.AdsShowDeleted(mbShowDeleted ? (ushort)1 : (ushort)0));
            mState = ConnectionState.Open;
        }

        public virtual void Disconnect()
        {
            if (mState != ConnectionState.Open || mhInitConnect != IntPtr.Zero)
                return;
            mState = ConnectionState.Closed;
            var num = ACE.AdsDisconnect(mhConnect);
            mhConnect = IntPtr.Zero;
            if (num != 0U)
                throw new AdsException();
        }

        internal void EnlistTransaction(Transaction transaction, AdsConnection conn)
        {
            if (!(transaction != null))
                return;
            if (mCurrentTransaction != null)
                throw new InvalidOperationException("Transaction is already active.");
            transaction.EnlistVolatile(this, EnlistmentOptions.None);
            mCurrentTransaction = conn.BeginTransaction();
        }

        private void DisposeOrReturnToPool()
        {
            if (this is AdsPooledInternalConnection)
                AdsPooledInternalConnection.ReturnConnectionToPool(this as AdsPooledInternalConnection);
            else
                Dispose();
        }

        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            try
            {
                if (mCurrentTransaction != null)
                {
                    mCurrentTransaction.InternalCommit(mhConnect);
                    mCurrentTransaction.Dispose();
                    mCurrentTransaction = null;
                }

                preparingEnlistment.Prepared();
            }
            catch
            {
                preparingEnlistment.ForceRollback();
                if (mCurrentTransaction != null)
                {
                    mCurrentTransaction.InternalRollback(mhConnect);
                    mCurrentTransaction.Dispose();
                    mCurrentTransaction = null;
                }

                if (!mbDisposeOnCommit)
                    return;
                DisposeOrReturnToPool();
            }
        }

        public void Commit(Enlistment enlistment)
        {
            if (mbDisposeOnCommit)
                DisposeOrReturnToPool();
            enlistment.Done();
        }

        public void Rollback(Enlistment enlistment)
        {
            if (mCurrentTransaction != null)
            {
                mCurrentTransaction.InternalRollback(mhConnect);
                mCurrentTransaction.Dispose();
                mCurrentTransaction = null;
            }

            if (mbDisposeOnCommit)
                DisposeOrReturnToPool();
            enlistment.Done();
        }

        public void InDoubt(Enlistment enlistment) => enlistment.Done();

        public virtual void Reset()
        {
            var num = (int)ACE.AdsResetConnection(mhConnect);
        }

        public bool TrimTrailingSpaces
        {
            get => mbTrimTrailingSpaces;
            set => mbTrimTrailingSpaces = value;
        }

        public IntPtr Handle => mhConnect;

        public uint TableOpenOptions => muiOpenOptions;

        public ushort TableType => musTableType;

        public ushort LockType => musLockType;

        public ushort RightsChecking => musCheckRights;

        public string CharType => mstrCharType;

        public string UnicodeCollation => mstrUnicodeCollation;

        public string ConnectionString => mstrConnectionString;

        public ConnectionState State => mState;

        public ushort HandleType => musHandleType;

        public string ConnectionPath => mstrConnectPath;

        public string InitialCatalog => mstrInitialCatalog;

        public bool DbfsUseNulls => mbDbfsUseNulls;

        public string EncryptionPassword
        {
            get => mstrEncryptionPassword == null ? "" : mstrEncryptionPassword;
        }

        public AdsTransaction CurrentTransaction => mCurrentTransaction;

        public bool DisposeOnCommit
        {
            set => mbDisposeOnCommit = value;
        }

        public bool TransScopeEnlist => mbEnlist;
    }
}