using System.Globalization;

namespace Advantage.Data.Provider.SqlGen
{
    internal class StartAtClause : ISqlFragment
    {
        private ISqlFragment startatCount;
        private bool hasTop;

        internal ISqlFragment StartAtCount => startatCount;

        internal bool HasTop
        {
            get => hasTop;
            set => hasTop = value;
        }

        internal StartAtClause(ISqlFragment startatCount, bool hasTop)
        {
            this.startatCount = startatCount;
            this.hasTop = hasTop;
        }

        internal StartAtClause(int startatCount, bool hasTop)
        {
            var sqlBuilder = new SqlBuilder();
            ++startatCount;
            sqlBuilder.Append(startatCount.ToString(CultureInfo.InvariantCulture));
            this.startatCount = sqlBuilder;
            this.hasTop = hasTop;
        }

        public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
        {
            if (hasTop)
                writer.Write("START AT ");
            else
                writer.Write("TOP 2000000000 START AT ");
            StartAtCount.WriteSql(writer, sqlGenerator);
            writer.Write(" ");
        }
    }
}