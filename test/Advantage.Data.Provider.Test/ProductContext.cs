using System.Data.Common;
using System.Data.Entity;

namespace Advantage.Data.Provider.Test
{
    [DbConfigurationType(typeof(ProductConfig))]
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(string connStr) : base(GetConn(connStr), true)
        {
        }

        private static DbConnection GetConn(string connStr)
        {
            return new AdsConnection(connStr);
        }
    }
}