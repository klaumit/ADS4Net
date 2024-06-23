using System;
using Advantage.Data.Native;
using Sample.Con.Cases;
using Sample.Con.Core;

namespace Sample.Con
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var helper = new DelegateOutput { WriteLiner = Console.WriteLine };
            NativeBoot.RegisterDefault();

            TestAdoCases.RunSimple(helper);
            TestOrmCases.RunEntity(helper);
        }
    }
}