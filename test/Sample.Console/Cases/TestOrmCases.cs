using System.Linq;
using Sample.Con.Core;
using Sample.Model;
using static Advantage.Data.Provider.AdsHelper;

namespace Sample.Con.Cases
{
    public static class TestOrmCases
    {
        public static void RunEntity(IOutputHelper @out)
        {
            @out.WriteLine($" === {nameof(RunEntity)} === ");

            var connStr = GetConnStr(GetSource(""), true);
            @out.WriteLine(connStr);

            using var context = new ProductContext(connStr);
            var newProduct = new Product { Name = "Example Product", Price = 9.99M };
            context.Products.Add(newProduct);
            context.SaveChanges();

            var product = context.Products.FirstOrDefault();
            if (product != null)
            {
                @out.WriteLine($"Product found: {product.Name}, {product.Price}");
            }

            if (product != null)
            {
                product.Price = 19.99M;
                context.SaveChanges();
                @out.WriteLine("Product price updated.");
            }

            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
                @out.WriteLine("Product deleted.");
            }
        }
    }
}