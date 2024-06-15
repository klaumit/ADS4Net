using System.Data.Entity;
using static Advantage.Data.Provider.Test.AdsHelper;

namespace Advantage.Data.Provider.Test
{
    [DbConfigurationType(typeof(ProductConfig))]
    public class ProductContext : DbContext
    {
        public ProductContext(string connStr) : base(GetConn(connStr), true)
        {
            // TODO Database.SetInitializer(new CreateDatabaseIfNotExists<ProductContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fix: ADS doesn't support schemas for some reason...
            modelBuilder.HasDefaultSchema("");
        }

        public DbSet<Product> Products { get; set; }
    }
}