using System;
using System.Data.Common;
using Advantage.Data.Provider;
using Advantage.Data.Provider.Test;

namespace SmallConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var source = AdsHelper.GetSource("sample");
            var connStr = AdsHelper.GetConnStr(source, isLocal: true);

            using var conn = AdsHelper.GetConn(connStr);
            conn.Open();

            Console.WriteLine(conn.ConnectionString);
            Console.WriteLine(conn.State);

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Poland";

            using var reader = cmd.ExecuteReader();
            Console.WriteLine(reader.FieldCount);
        }
    }
}