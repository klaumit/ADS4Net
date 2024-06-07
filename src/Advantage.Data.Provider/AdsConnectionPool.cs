using System;
using System.Collections;
using AdvantageClientEngine;

namespace Advantage.Data.Provider
{
    internal class AdsConnectionPool
    {
        private ArrayList mAvailableConnections;
        private bool mbDisposed;
        private string mstrConnectionString;
        private bool mbReset = true;
        private int miLifeTime;
        private int miMin;
        private int miMax = 100;
        private int miOpen;

        public AdsConnectionPool() => throw new NotSupportedException();

        public AdsConnectionPool(string strConnectionString, AdsConnectionStringHandler handler)
        {
            mAvailableConnections = new ArrayList();
            mstrConnectionString = strConnectionString;
            miLifeTime = handler.LifeTime;
            miMin = handler.MinPoolSize;
            miMax = handler.MaxPoolSize;
            mbReset = handler.ConnectionReset;
            if (miMin > miMax)
                throw new ArgumentException(
                    "Invalid min or max pool size values, min pool size cannot be greater than the max pool size.");
        }

        public AdsPooledInternalConnection GetOpenPoolConnection(
            AdsConnectionStringHandler handler,
            int iConnectTimeout)
        {
            AdsPooledInternalConnection openPoolConnection = null;
            lock (this)
            {
                if (miOpen >= miMax)
                {
                    openPoolConnection = null;
                    return openPoolConnection;
                }

                if (mAvailableConnections.Count != 0)
                {
                    while (mAvailableConnections.Count != 0)
                    {
                        openPoolConnection = (AdsPooledInternalConnection)mAvailableConnections[0];
                        mAvailableConnections.RemoveAt(0);
                        ushort pbConnectionIsAlive;
                        if (ACE.AdsIsConnectionAlive(openPoolConnection.Handle, out pbConnectionIsAlive) != 0U ||
                            pbConnectionIsAlive == 0)
                        {
                            --miOpen;
                            openPoolConnection.Dispose();
                            openPoolConnection = null;
                        }
                        else
                            break;
                    }
                }

                if (openPoolConnection == null)
                {
                    openPoolConnection = new AdsPooledInternalConnection(mstrConnectionString, handler, this);
                    try
                    {
                        openPoolConnection.Connect();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    ++miOpen;
                }

                if (miOpen >= miMin)
                    ;
                for (; miOpen < miMin; ++miOpen)
                {
                    var internalConnection =
                        new AdsPooledInternalConnection(mstrConnectionString, handler, this);
                    try
                    {
                        internalConnection.Connect();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                    mAvailableConnections.Add(internalConnection);
                }
            }

            return openPoolConnection;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool bDisposing)
        {
            if (mbDisposed)
                return;
            lock (this)
            {
                if (mbDisposed)
                    return;
                foreach (AdsInternalConnection availableConnection in mAvailableConnections)
                    availableConnection.Dispose(bDisposing);
                mAvailableConnections.Clear();
                mbDisposed = true;
            }
        }

        public void ReturnConnectionToPool(AdsPooledInternalConnection pooledConnection)
        {
            lock (this)
            {
                if (miLifeTime > 0 &&
                    DateTime.Now > pooledConnection.CreationTime.AddSeconds(miLifeTime))
                {
                    pooledConnection.Dispose();
                    --miOpen;
                }
                else
                {
                    if (mbReset)
                        pooledConnection.Reset();
                    mAvailableConnections.Add(pooledConnection);
                }
            }
        }

        public void FlushConnections()
        {
            lock (this)
            {
                foreach (AdsInternalConnection availableConnection in mAvailableConnections)
                {
                    availableConnection.Dispose();
                    --miOpen;
                }

                mAvailableConnections.Clear();
            }
        }

        public void DecrementOpenCount()
        {
            lock (this)
            {
                if (miOpen <= 0)
                    return;
                --miOpen;
            }
        }

        public string ConnectionString => mstrConnectionString;

        public int LifeTime => miLifeTime;

        public int Min => miMin;

        public int Max => miMax;
    }
}