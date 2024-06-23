using Sample.Con.Cases;
using Sample.Con.Core;

namespace Sample.Con
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var helper = new DelegateOutput();
            TestAdoCases.RunSimple(helper);
            TestOrmCases.RunEntity(helper);
        }
    }
}