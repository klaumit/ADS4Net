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
            mPool = pool;
            mCreateTime = DateTime.Now;
        }

        public static void ReturnConnectionToPool(AdsPooledInternalConnection pooledConnection)
        {
            pooledConnection.ConnectionPool.ReturnConnectionToPool(pooledConnection);
        }

        public void DecrementPoolOpenCount()
        {
            if (mbDisposed || mPool == null)
                return;
            mPool.DecrementOpenCount();
        }

        public AdsConnectionPool ConnectionPool => mPool;

        public DateTime CreationTime => mCreateTime;
    }
}