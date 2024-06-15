using System;
using System.Data;
using Advantage.Data.Provider;
using Advantage.Data.Provider.Test;
using Newtonsoft.Json;

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

            using var table = new DataTable();
            var adapter = new AdsDataAdapter(cmd);
            adapter.Fill(table);

            var json = JsonConvert.SerializeObject(table);
            Console.WriteLine(json);
        }
    }
}