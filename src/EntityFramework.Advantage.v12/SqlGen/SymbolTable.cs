using System;
using System.Collections.Generic;

namespace Advantage.Data.Provider.SqlGen
{
    internal sealed class SymbolTable
    {
        private List<Dictionary<string, Symbol>> symbols = new List<Dictionary<string, Symbol>>();

        internal void EnterScope()
        {
            symbols.Add(
                new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase));
        }

        internal void ExitScope() => symbols.RemoveAt(symbols.Count - 1);

        internal void Add(string name, Symbol value)
        {
            symbols[symbols.Count - 1][name] = value;
        }

        internal Symbol Lookup(string name)
        {
            for (var index = symbols.Count - 1; index >= 0; --index)
            {
                if (symbols[index].ContainsKey(name))
                    return symbols[index][name];
            }

            return null;
        }
    }
}