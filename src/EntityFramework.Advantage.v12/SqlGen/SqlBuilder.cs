using System;
using System.Collections.Generic;

namespace Advantage.Data.Provider.SqlGen
{
    internal sealed class SqlBuilder : ISqlFragment
    {
        private List<object> _sqlFragments;

        private List<object> sqlFragments
        {
            get
            {
                if (_sqlFragments == null)
                    _sqlFragments = new List<object>();
                return _sqlFragments;
            }
        }

        public void Append(object s) => sqlFragments.Add(s);

        public void AppendLine() => sqlFragments.Add("\r\n");

        public bool IsEmpty => _sqlFragments == null || 0 == _sqlFragments.Count;

        public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
        {
            if (_sqlFragments == null)
                return;
            foreach (var sqlFragment1 in _sqlFragments)
            {
                switch (sqlFragment1)
                {
                    case string str:
                        writer.Write(str);
                        continue;
                    case ISqlFragment sqlFragment2:
                        sqlFragment2.WriteSql(writer, sqlGenerator);
                        continue;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
    }
}