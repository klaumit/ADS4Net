using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static Advantage.Data.Provider.Test.AdsHelper;

namespace Advantage.Data.Provider.Test
{
    public class EntityTest
    {
        private readonly ITestOutputHelper _out;

        public EntityTest(ITestOutputHelper @out)
        {
            _out = @out;
        }

        [Fact]
        public void TestProducts()
        {
            var connStr = GetConnStr(GetSource(nameof(TestProducts)), true);
            _out.WriteLine(connStr);

            using var context = new ProductContext(connStr);
            var newProduct = new Product { Name = "Example Product", Price = 9.99M };
            context.Products.Add(newProduct);
            context.SaveChanges();

            var product = context.Products.FirstOrDefault();
            if (product != null)
            {
                _out.WriteLine($"Product found: {product.Name}, {product.Price}");
            }

            if (product != null)
            {
                product.Price = 19.99M;
                context.SaveChanges();
                _out.WriteLine("Product price updated.");
            }

            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                _out.WriteLine("Product deleted.");
            }
        }
    }
}