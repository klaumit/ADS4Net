using Microsoft.VisualStudio.Data.AdoDotNet;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal class AdsConnectionSupport : AdoDotNetConnectionSupport
    {
        public AdsConnectionSupport()
            : base("Advantage.Data.Provider")
        {
        }

        public virtual bool Open(bool doPromptCheck)
        {
            bool flag = base.Open(doPromptCheck);
            return !flag ? flag : flag;
        }
    }
}