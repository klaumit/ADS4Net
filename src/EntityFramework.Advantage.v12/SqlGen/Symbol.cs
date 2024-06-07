using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;

namespace Advantage.Data.Provider.SqlGen
{
    internal class Symbol : ISqlFragment
    {
        private Dictionary<string, Symbol> columns;
        private bool needsRenaming;
        private bool outputColumnsRenamed;
        private string name;
        private string newName;
        private TypeUsage type;

        internal Dictionary<string, Symbol> Columns
        {
            get
            {
                if (columns == null)
                    columns =
                        new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase);
                return columns;
            }
        }

        internal bool NeedsRenaming
        {
            get => needsRenaming;
            set => needsRenaming = value;
        }

        internal bool OutputColumnsRenamed
        {
            get => outputColumnsRenamed;
            set => outputColumnsRenamed = value;
        }

        public string Name => name;

        public string NewName
        {
            get => newName;
            set => newName = value;
        }

        internal TypeUsage Type
        {
            get => type;
            set => type = value;
        }

        public Symbol(string name, TypeUsage type)
        {
            this.name = name;
            newName = name;
            Type = type;
        }

        public Symbol(string name, TypeUsage type, Dictionary<string, Symbol> columns)
        {
            this.name = name;
            newName = name;
            Type = type;
            this.columns = columns;
            OutputColumnsRenamed = true;
        }

        public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
        {
            if (NeedsRenaming)
            {
                int num;
                if (sqlGenerator.AllColumnNames.TryGetValue(NewName, out num))
                {
                    string key;
                    do
                    {
                        ++num;
                        key = NewName + num.ToString(CultureInfo.InvariantCulture);
                    } while (sqlGenerator.AllColumnNames.ContainsKey(key));

                    sqlGenerator.AllColumnNames[NewName] = num;
                    NewName = key;
                }

                sqlGenerator.AllColumnNames[NewName] = 0;
                NeedsRenaming = false;
            }

            writer.Write(SqlGenerator.QuoteIdentifier(NewName));
        }
    }
}