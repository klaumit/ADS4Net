using Advantage.Data.Provider;
using Sample.Con.Core;

namespace Sample.Con.Cases
{
    public static class TestAdoCases
    {
        public static void RunSimple(IOutputHelper @out)
        {
            @out.WriteLine($" === {nameof(RunSimple)} === ");

            var source = AdsHelper.GetSource("");
            var connStr = AdsHelper.GetConnStr(source, isLocal: true);

            using var conn = AdsHelper.GetConn(connStr);
            conn.Open();

            @out.WriteLine($"{conn.ConnectionString} {conn.State}");

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Poland";

            var json = cmd.ToJson();
            @out.WriteLine(json);
        }
    }
}