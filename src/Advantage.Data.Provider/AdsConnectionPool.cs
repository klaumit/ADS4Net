using AdvantageClientEngine;
using System;
using System.Collections;

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
            this.mAvailableConnections = new ArrayList();
            this.mstrConnectionString = strConnectionString;
            this.miLifeTime = handler.LifeTime;
            this.miMin = handler.MinPoolSize;
            this.miMax = handler.MaxPoolSize;
            this.mbReset = handler.ConnectionReset;
            if (this.miMin > this.miMax)
                throw new ArgumentException(
                    "Invalid min or max pool size values, min pool size cannot be greater than the max pool size.");
        }

        public AdsPooledInternalConnection GetOpenPoolConnection(
            AdsConnectionStringHandler handler,
            int iConnectTimeout)
        {
            AdsPooledInternalConnection openPoolConnection = (AdsPooledInternalConnection)null;
            lock (this)
            {
                if (this.miOpen >= this.miMax)
                {
                    openPoolConnection = (AdsPooledInternalConnection)null;
                    return openPoolConnection;
                }

                if (this.mAvailableConnections.Count != 0)
                {
                    while (this.mAvailableConnections.Count != 0)
                    {
                        openPoolConnection = (AdsPooledInternalConnection)this.mAvailableConnections[0];
                        this.mAvailableConnections.RemoveAt(0);
                        ushort pbConnectionIsAlive;
                        if (ACE.AdsIsConnectionAlive(openPoolConnection.Handle, out pbConnectionIsAlive) != 0U ||
                            pbConnectionIsAlive == (ushort)0)
                        {
                            --this.miOpen;
                            openPoolConnection.Dispose();
                            openPoolConnection = (AdsPooledInternalConnection)null;
                        }
                        else
                            break;
                    }
                }

                if (openPoolConnection == null)
                {
                    openPoolConnection = new AdsPooledInternalConnection(this.mstrConnectionString, handler, this);
                    try
                    {
                        openPoolConnection.Connect();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    ++this.miOpen;
                }

                if (this.miOpen >= this.miMin)
                    ;
                for (; this.miOpen < this.miMin; ++this.miOpen)
                {
                    AdsPooledInternalConnection internalConnection =
                        new AdsPooledInternalConnection(this.mstrConnectionString, handler, this);
                    try
                    {
                        internalConnection.Connect();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                    this.mAvailableConnections.Add((object)internalConnection);
                }
            }

            return openPoolConnection;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        public void Dispose(bool bDisposing)
        {
            if (this.mbDisposed)
                return;
            lock (this)
            {
                if (this.mbDisposed)
                    return;
                foreach (AdsInternalConnection availableConnection in this.mAvailableConnections)
                    availableConnection.Dispose(bDisposing);
                this.mAvailableConnections.Clear();
                this.mbDisposed = true;
            }
        }

        public void ReturnConnectionToPool(AdsPooledInternalConnection pooledConnection)
        {
            lock (this)
            {
                if (this.miLifeTime > 0 &&
                    DateTime.Now > pooledConnection.CreationTime.AddSeconds((double)this.miLifeTime))
                {
                    pooledConnection.Dispose();
                    --this.miOpen;
                }
                else
                {
                    if (this.mbReset)
                        pooledConnection.Reset();
                    this.mAvailableConnections.Add((object)pooledConnection);
                }
            }
        }

        public void FlushConnections()
        {
            lock (this)
            {
                foreach (AdsInternalConnection availableConnection in this.mAvailableConnections)
                {
                    availableConnection.Dispose();
                    --this.miOpen;
                }

                this.mAvailableConnections.Clear();
            }
        }

        public void DecrementOpenCount()
        {
            lock (this)
            {
                if (this.miOpen <= 0)
                    return;
                --this.miOpen;
            }
        }

        public string ConnectionString => this.mstrConnectionString;

        public int LifeTime => this.miLifeTime;

        public int Min => this.miMin;

        public int Max => this.miMax;
    }
}