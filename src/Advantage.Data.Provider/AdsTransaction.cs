using AdvantageClientEngine;
using System;
using System.Data;
using System.Data.Common;

namespace Advantage.Data.Provider
{
    public sealed class AdsTransaction : DbTransaction, IDbTransaction, IDisposable
    {
        private bool mbDisposed;
        private AdsConnection mConnection;

        public AdsTransaction() => throw new NotSupportedException();

        public AdsTransaction(AdsConnection Conn)
        {
            AdsException.CheckACE(ACE.AdsBeginTransaction(Conn.InternalConnection.Handle));
            this.mConnection = Conn;
        }

        ~AdsTransaction() => this.Dispose(false);

        protected override void Dispose(bool bDisposing)
        {
            if (!this.mbDisposed)
            {
                lock (this)
                {
                    if (!this.mbDisposed)
                    {
                        int num = bDisposing ? 1 : 0;
                        this.mbDisposed = true;
                    }
                }
            }

            base.Dispose(bDisposing);
        }

        public override IsolationLevel IsolationLevel => IsolationLevel.ReadCommitted;

        IDbConnection IDbTransaction.Connection => (IDbConnection)this.Connection;

        protected override DbConnection DbConnection => (DbConnection)this.Connection;

        public new AdsConnection Connection => this.mConnection;

        internal void InternalCommit(IntPtr hConn)
        {
            uint ulRet = ACE.AdsCommitTransaction(hConn);
            if (ulRet == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(ulRet);
        }

        public override void Commit()
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            if (this.mConnection.State != ConnectionState.Open)
                return;
            this.InternalCommit(this.mConnection.InternalConnection.Handle);
        }

        internal void InternalRollback(IntPtr hConn)
        {
            uint ulRet = ACE.AdsRollbackTransaction(hConn);
            if (ulRet == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(ulRet);
        }

        public override void Rollback()
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            if (this.mConnection.State != ConnectionState.Open)
                return;
            this.InternalRollback(this.mConnection.InternalConnection.Handle);
        }

        public void Rollback(string strSavepoint)
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            if (this.mConnection.State != ConnectionState.Open)
                return;
            uint ulRet = ACE.AdsRollbackTransaction80(this.mConnection.InternalConnection.Handle, strSavepoint, 0U);
            if (ulRet == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(ulRet);
        }

        public void Save(string strSavepoint)
        {
            if (this.mbDisposed)
                throw new ObjectDisposedException(this.ToString());
            if (this.mConnection.State != ConnectionState.Open)
                return;
            uint savepoint = ACE.AdsCreateSavepoint(this.mConnection.InternalConnection.Handle, strSavepoint, 0U);
            if (savepoint == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(savepoint);
        }
    }
}