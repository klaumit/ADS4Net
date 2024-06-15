using System;
using Advantage.Data.Provider.Test;

namespace SmallConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var source = AdsHelper.GetSource("hello");
            var connStr = AdsHelper.GetConnStr(source, isLocal: true);

            using var conn = AdsHelper.GetConn(connStr);
            conn.Open();

            Console.WriteLine(conn);
            Console.WriteLine(conn.State);
        }
    }
}