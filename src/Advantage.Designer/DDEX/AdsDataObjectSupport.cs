using Microsoft.VisualStudio.Data;

namespace Advantage.Data.DDEX
{
    internal class AdsDataObjectSupport : DataObjectSupport
    {
        public AdsDataObjectSupport()
            : base("Advantage.Data.DDEX.AdsDataObjectSupport", typeof(AdsDataObjectSupport).Assembly)
        {
        }
    }
}