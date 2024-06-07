using System.Globalization;

namespace Advantage.Data.Provider.SqlGen
{
    internal class TopClause : ISqlFragment
    {
        private ISqlFragment topCount;
        private bool withTies;

        internal bool WithTies => withTies;

        internal ISqlFragment TopCount => topCount;

        internal TopClause(ISqlFragment topCount, bool withTies)
        {
            this.topCount = topCount;
            this.withTies = withTies;
        }

        internal TopClause(int topCount, bool withTies)
        {
            var sqlBuilder = new SqlBuilder();
            sqlBuilder.Append(topCount.ToString(CultureInfo.InvariantCulture));
            this.topCount = sqlBuilder;
            this.withTies = withTies;
        }

        public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
        {
            writer.Write("TOP ");
            TopCount.WriteSql(writer, sqlGenerator);
            writer.Write(" ");
        }
    }
}