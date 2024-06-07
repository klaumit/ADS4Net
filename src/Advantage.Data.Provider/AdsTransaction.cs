using System;
using System.Data;
using System.Data.Common;
using AdvantageClientEngine;

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
            mConnection = Conn;
        }

        ~AdsTransaction() => Dispose(false);

        protected override void Dispose(bool bDisposing)
        {
            if (!mbDisposed)
            {
                lock (this)
                {
                    if (!mbDisposed)
                    {
                        var num = bDisposing ? 1 : 0;
                        mbDisposed = true;
                    }
                }
            }

            base.Dispose(bDisposing);
        }

        public override IsolationLevel IsolationLevel => IsolationLevel.ReadCommitted;

        IDbConnection IDbTransaction.Connection => Connection;

        protected override DbConnection DbConnection => Connection;

        public new AdsConnection Connection => mConnection;

        internal void InternalCommit(IntPtr hConn)
        {
            var ulRet = ACE.AdsCommitTransaction(hConn);
            if (ulRet == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(ulRet);
        }

        public override void Commit()
        {
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            if (mConnection.State != ConnectionState.Open)
                return;
            InternalCommit(mConnection.InternalConnection.Handle);
        }

        internal void InternalRollback(IntPtr hConn)
        {
            var ulRet = ACE.AdsRollbackTransaction(hConn);
            if (ulRet == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(ulRet);
        }

        public override void Rollback()
        {
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            if (mConnection.State != ConnectionState.Open)
                return;
            InternalRollback(mConnection.InternalConnection.Handle);
        }

        public void Rollback(string strSavepoint)
        {
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            if (mConnection.State != ConnectionState.Open)
                return;
            var ulRet = ACE.AdsRollbackTransaction80(mConnection.InternalConnection.Handle, strSavepoint, 0U);
            if (ulRet == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(ulRet);
        }

        public void Save(string strSavepoint)
        {
            if (mbDisposed)
                throw new ObjectDisposedException(ToString());
            if (mConnection.State != ConnectionState.Open)
                return;
            var savepoint = ACE.AdsCreateSavepoint(mConnection.InternalConnection.Handle, strSavepoint, 0U);
            if (savepoint == 5047U)
                throw new InvalidOperationException("This transaction has already been committed or rolled back.");
            AdsException.CheckACE(savepoint);
        }
    }
}