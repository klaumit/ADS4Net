using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Win32;

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
            mConnectionPools = new ArrayList();
            try
            {
                var type = typeof(AdsConnection);
                var num = new IntPtr(0);
                if (!type.Assembly.GlobalAssemblyCache)
                    num = LoadLibrary(Regex.Replace(type.Assembly.Location.ToLower(),
                        "advantage.data.provider.dll", "ace32.dll"));
                if (num.ToInt32() == 0)
                {
                    var registryKey1 =
                        Registry.LocalMachine.OpenSubKey(
                            "SOFTWARE\\Microsoft\\.NETFramework\\AssemblyFolders\\Advantage .NET");
                    if (registryKey1 != null)
                    {
                        var str = (string)registryKey1.GetValue("");
                        if (!str.EndsWith("\\"))
                            str += "\\";
                        num = LoadLibrary(str + "ace32.dll");
                    }
                    else
                    {
                        var registryKey2 =
                            Registry.CurrentUser.OpenSubKey(
                                "SOFTWARE\\Microsoft\\.NETFramework\\AssemblyFolders\\Advantage .NET");
                        if (registryKey2 != null)
                        {
                            var str = (string)registryKey2.GetValue("");
                            if (!str.EndsWith("\\"))
                                str += "\\";
                            num = LoadLibrary(str + "ace32.dll");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            AdsConnectionStringHandler.Initialize();
        }

        ~AdsPoolManager() => Dispose(false);

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
                mbDisposed = true;
                foreach (AdsConnectionPool mConnectionPool in mConnectionPools)
                    mConnectionPool?.Dispose(bDisposing);
                mConnectionPools.Clear();
            }
        }

        public void GetConnection(
            string strConnectionString,
            out AdsInternalConnection internalConnection,
            out AdsConnectionPool pool)
        {
            var handler = new AdsConnectionStringHandler();
            internalConnection = null;
            pool = null;
            handler.ParseConnectionString(strConnectionString);
            var flag = handler.Pooling;
            var connectTimeout = handler.ConnectTimeout;
            if (flag && handler.HaveConnectionHandle)
                flag = false;
            if (flag)
            {
                var connectionPoolCompare =
                    new AdsConnectionPoolCompare();
                pool = new AdsConnectionPool(strConnectionString, handler);
                var index = mConnectionPools.BinarySearch(pool,
                    connectionPoolCompare);
                if (index >= 0)
                {
                    pool = (AdsConnectionPool)mConnectionPools[index];
                    internalConnection = pool.GetOpenPoolConnection(handler, connectTimeout);
                }
                else
                {
                    internalConnection = pool.GetOpenPoolConnection(handler, connectTimeout);
                    if (internalConnection != null)
                    {
                        lock (this)
                        {
                            mConnectionPools.Add(pool);
                            mConnectionPools.Sort(connectionPoolCompare);
                        }
                    }
                    else
                        pool = null;
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
            var connectionPoolCompare =
                new AdsConnectionPoolCompare();
            var handler = new AdsConnectionStringHandler();
            var adsConnectionPool = new AdsConnectionPool(strConnectionString, handler);
            var index = mConnectionPools.BinarySearch(adsConnectionPool,
                connectionPoolCompare);
            if (index < 0)
                return;
            ((AdsConnectionPool)mConnectionPools[index]).FlushConnections();
        }

        public void FlushConnections()
        {
            foreach (AdsConnectionPool mConnectionPool in mConnectionPools)
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