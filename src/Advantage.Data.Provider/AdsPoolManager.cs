using Microsoft.Win32;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Advantage.Data.Provider
{
    internal class AdsPoolManager : IDisposable
    {
        private static ArrayList mConnectionPools;
        private bool mbDisposed;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        public AdsPoolManager()
        {
            AdsPoolManager.mConnectionPools = new ArrayList();
            try
            {
                Type type = typeof(AdsConnection);
                IntPtr num = new IntPtr(0);
                if (!type.Assembly.GlobalAssemblyCache)
                    num = AdsPoolManager.LoadLibrary(Regex.Replace(type.Assembly.Location.ToLower(),
                        "advantage.data.provider.dll", "ace32.dll"));
                if (num.ToInt32() == 0)
                {
                    RegistryKey registryKey1 =
                        Registry.LocalMachine.OpenSubKey(
                            "SOFTWARE\\Microsoft\\.NETFramework\\AssemblyFolders\\Advantage .NET");
                    if (registryKey1 != null)
                    {
                        string str = (string)registryKey1.GetValue("");
                        if (!str.EndsWith("\\"))
                            str += "\\";
                        num = AdsPoolManager.LoadLibrary(str + "ace32.dll");
                    }
                    else
                    {
                        RegistryKey registryKey2 =
                            Registry.CurrentUser.OpenSubKey(
                                "SOFTWARE\\Microsoft\\.NETFramework\\AssemblyFolders\\Advantage .NET");
                        if (registryKey2 != null)
                        {
                            string str = (string)registryKey2.GetValue("");
                            if (!str.EndsWith("\\"))
                                str += "\\";
                            num = AdsPoolManager.LoadLibrary(str + "ace32.dll");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            AdsConnectionStringHandler.Initialize();
        }

        ~AdsPoolManager() => this.Dispose(false);

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
                this.mbDisposed = true;
                foreach (AdsConnectionPool mConnectionPool in AdsPoolManager.mConnectionPools)
                    mConnectionPool?.Dispose(bDisposing);
                AdsPoolManager.mConnectionPools.Clear();
            }
        }

        public void GetConnection(
            string strConnectionString,
            out AdsInternalConnection internalConnection,
            out AdsConnectionPool pool)
        {
            AdsConnectionStringHandler handler = new AdsConnectionStringHandler();
            internalConnection = (AdsInternalConnection)null;
            pool = (AdsConnectionPool)null;
            handler.ParseConnectionString(strConnectionString);
            bool flag = handler.Pooling;
            int connectTimeout = handler.ConnectTimeout;
            if (flag && handler.HaveConnectionHandle)
                flag = false;
            if (flag)
            {
                AdsPoolManager.AdsConnectionPoolCompare connectionPoolCompare =
                    new AdsPoolManager.AdsConnectionPoolCompare();
                pool = new AdsConnectionPool(strConnectionString, handler);
                int index = AdsPoolManager.mConnectionPools.BinarySearch((object)pool,
                    (IComparer)connectionPoolCompare);
                if (index >= 0)
                {
                    pool = (AdsConnectionPool)AdsPoolManager.mConnectionPools[index];
                    internalConnection = (AdsInternalConnection)pool.GetOpenPoolConnection(handler, connectTimeout);
                }
                else
                {
                    internalConnection = (AdsInternalConnection)pool.GetOpenPoolConnection(handler, connectTimeout);
                    if (internalConnection != null)
                    {
                        lock (this)
                        {
                            AdsPoolManager.mConnectionPools.Add((object)pool);
                            AdsPoolManager.mConnectionPools.Sort((IComparer)connectionPoolCompare);
                        }
                    }
                    else
                        pool = (AdsConnectionPool)null;
                }
            }
            else
            {
                internalConnection = new AdsInternalConnection(strConnectionString, handler);
                internalConnection.Connect();
            }
        }

        public void FlushConnections(string strConnectionString)
        {
            AdsPoolManager.AdsConnectionPoolCompare connectionPoolCompare =
                new AdsPoolManager.AdsConnectionPoolCompare();
            AdsConnectionStringHandler handler = new AdsConnectionStringHandler();
            AdsConnectionPool adsConnectionPool = new AdsConnectionPool(strConnectionString, handler);
            int index = AdsPoolManager.mConnectionPools.BinarySearch((object)adsConnectionPool,
                (IComparer)connectionPoolCompare);
            if (index < 0)
                return;
            ((AdsConnectionPool)AdsPoolManager.mConnectionPools[index]).FlushConnections();
        }

        public void FlushConnections()
        {
            foreach (AdsConnectionPool mConnectionPool in AdsPoolManager.mConnectionPools)
                mConnectionPool.FlushConnections();
        }

        public class AdsConnectionPoolCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                return string.Compare(((AdsConnectionPool)x).ConnectionString, ((AdsConnectionPool)y).ConnectionString);
            }
        }
    }
}