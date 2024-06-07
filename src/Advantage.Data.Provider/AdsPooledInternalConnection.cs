using System;

namespace Advantage.Data.Provider
{
    internal class AdsPooledInternalConnection : AdsInternalConnection
    {
        private AdsConnectionPool mPool;
        private DateTime mCreateTime;

        public AdsPooledInternalConnection() => throw new NotSupportedException();

        public AdsPooledInternalConnection(
            string strConnectionString,
            AdsConnectionStringHandler handler,
            AdsConnectionPool pool)
            : base(strConnectionString, handler)
        {
            this.mPool = pool;
            this.mCreateTime = DateTime.Now;
        }

        public static void ReturnConnectionToPool(AdsPooledInternalConnection pooledConnection)
        {
            pooledConnection.ConnectionPool.ReturnConnectionToPool(pooledConnection);
        }

        public void DecrementPoolOpenCount()
        {
            if (this.mbDisposed || this.mPool == null)
                return;
            this.mPool.DecrementOpenCount();
        }

        public AdsConnectionPool ConnectionPool => this.mPool;

        public DateTime CreationTime => this.mCreateTime;
    }
}