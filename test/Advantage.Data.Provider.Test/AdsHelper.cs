namespace Advantage.Data.Provider.Test
{
    public static class AdsHelper
    {
        public static string GetConnStr(string source)
        {
            var bld = new AdsConnectionStringBuilder
            {
                DataSource = source
            };
            return bld.ConnectionString;
        }
    }
}