using System;
using System.Text;
using System.IO;

namespace Advantage.Data.Provider.Test
{
    public static class AdsHelper
    {
        static AdsHelper()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static AdsConnection GetConn(string connStr)
        {
            return new AdsConnection(connStr);
        }

        public static string GetSource(string sub, string name = "data", string? root = null)
        {
            var dir = Path.GetFullPath(root ?? Environment.CurrentDirectory);
            dir = Path.Combine(dir, name, sub);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            dir += Path.DirectorySeparatorChar;
            return dir;
        }

        public static string GetConnStr(string source, bool isLocal, string type = "ADT")
        {
            var bld = new AdsConnectionStringBuilder
            {
                DataSource = source,
                ServerType = isLocal ? "local" : "remote",
                TableType = type
            };
            return bld.ConnectionString;
        }
    }
}