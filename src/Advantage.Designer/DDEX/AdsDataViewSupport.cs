using Microsoft.VisualStudio.Data;

namespace Advantage.Data.DDEX
{
    internal class AdsDataViewSupport : DataViewSupport
    {
        public AdsDataViewSupport()
            : base("Advantage.Data.DDEX.AdsDataViewSupport", typeof(AdsDataViewSupport).Assembly)
        {
        }
    }
}