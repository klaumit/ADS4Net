using System.Data.Entity;

namespace Advantage.Data.Provider.Test
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