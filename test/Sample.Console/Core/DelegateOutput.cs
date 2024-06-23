using System;

namespace Sample.Con.Core
{
    public sealed class DelegateOutput : IOutputHelper
    {
        public Action<string>? WriteLiner { get; set; }

        public void WriteLine(string text)
        {
            WriteLiner?.Invoke(text);
        }
    }
}