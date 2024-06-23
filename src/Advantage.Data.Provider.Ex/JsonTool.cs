using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Advantage.Data.Provider
{
    public static class JsonTool
    {
        private static readonly JsonSerializerSettings Config = new()
        {
            Formatting = Formatting.Indented,
            Converters = { new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore
        };

        public static string ToJson(this AdsCommand cmd)
        {
            using var adapter = new AdsDataAdapter(cmd);

            using var table = new DataTable();
            adapter.Fill(table);

            var json = JsonConvert.SerializeObject(table, Config);
            return json;
        }
    }
}