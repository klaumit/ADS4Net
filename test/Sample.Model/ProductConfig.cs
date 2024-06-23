using System.Data.Entity;
using Advantage.Data.Provider;

namespace Sample.Model
{
    public class ProductConfig : DbConfiguration
    {
        public ProductConfig()
        {
            const string name = "Advantage.Data.Provider";
            SetProviderServices(name, AdsProviderServices.Instance);
            SetProviderFactory(name, new AdsFactory());
        }
    }
}