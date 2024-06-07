using System.Collections.Generic;
using System.Globalization;

namespace Advantage.Data.Provider.SqlGen
{
    internal sealed class SqlSelectStatement : ISqlFragment
    {
        private bool outputColumnsRenamed;
        private Dictionary<string, Symbol> outputColumns;
        private bool isDistinct;
        private List<Symbol> allJoinExtents;
        private List<Symbol> fromExtents;
        private Dictionary<Symbol, bool> outerExtents;
        private TopClause top;
        private StartAtClause startat;
        private SqlBuilder select = new SqlBuilder();
        private SqlBuilder from = new SqlBuilder();
        private SqlBuilder where;
        private SqlBuilder groupBy;
        private SqlBuilder orderBy;
        private bool isTopMost;

        internal bool OutputColumnsRenamed
        {
            get => outputColumnsRenamed;
            set => outputColumnsRenamed = value;
        }

        internal Dictionary<string, Symbol> OutputColumns
        {
            get => outputColumns;
            set => outputColumns = value;
        }

        internal bool IsDistinct
        {
            get => isDistinct;
            set => isDistinct = value;
        }

        internal List<Symbol> AllJoinExtents
        {
            get => allJoinExtents;
            set => allJoinExtents = value;
        }

        internal List<Symbol> FromExtents
        {
            get
            {
                if (fromExtents == null)
                    fromExtents = new List<Symbol>();
                return fromExtents;
            }
        }

        internal Dictionary<Symbol, bool> OuterExtents
        {
            get
            {
                if (outerExtents == null)
                    outerExtents = new Dictionary<Symbol, bool>();
                return outerExtents;
            }
        }

        internal TopClause Top
        {
            get => top;
            set => top = value;
        }

        internal StartAtClause StartAt
        {
            get => startat;
            set => startat = value;
        }

        internal SqlBuilder Select => select;

        internal SqlBuilder From => from;

        internal SqlBuilder Where
        {
            get
            {
                if (where == null)
                    where = new SqlBuilder();
                return where;
            }
        }

        internal SqlBuilder GroupBy
        {
            get
            {
                if (groupBy == null)
                    groupBy = new SqlBuilder();
                return groupBy;
            }
        }

        public SqlBuilder OrderBy
        {
            get
            {
                if (orderBy == null)
                    orderBy = new SqlBuilder();
                return orderBy;
            }
        }

        internal bool IsTopMost
        {
            get => isTopMost;
            set => isTopMost = value;
        }

        public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
        {
            List<string> stringList = null;
            if (outerExtents != null && 0 < outerExtents.Count)
            {
                foreach (var key in outerExtents.Keys)
                {
                    if (key is JoinSymbol joinSymbol)
                    {
                        foreach (var flattenedExtent in joinSymbol.FlattenedExtentList)
                        {
                            if (stringList == null)
                                stringList = new List<string>();
                            stringList.Add(flattenedExtent.NewName);
                        }
                    }
                    else
                    {
                        if (stringList == null)
                            stringList = new List<string>();
                        stringList.Add(key.NewName);
                    }
                }
            }

            var symbolList = AllJoinExtents ?? fromExtents;
            if (symbolList != null)
            {
                foreach (var symbol in symbolList)
                {
                    if (stringList != null && stringList.Contains(symbol.Name))
                    {
                        var allExtentName = sqlGenerator.AllExtentNames[symbol.Name];
                        string key;
                        do
                        {
                            ++allExtentName;
                            key = symbol.Name + allExtentName.ToString(CultureInfo.InvariantCulture);
                        } while (sqlGenerator.AllExtentNames.ContainsKey(key));

                        sqlGenerator.AllExtentNames[symbol.Name] = allExtentName;
                        symbol.NewName = key;
                        sqlGenerator.AllExtentNames[key] = 0;
                    }

                    if (stringList == null)
                        stringList = new List<string>();
                    stringList.Add(symbol.NewName);
                }
            }

            ++writer.Indent;
            writer.Write("SELECT ");
            if (IsDistinct)
                writer.Write("DISTINCT ");
            if (Top != null)
                Top.WriteSql(writer, sqlGenerator);
            if (StartAt != null)
            {
                if (Top != null)
                    StartAt.HasTop = true;
                StartAt.WriteSql(writer, sqlGenerator);
            }

            if (select == null || Select.IsEmpty)
                writer.Write("*");
            else
                Select.WriteSql(writer, sqlGenerator);
            writer.WriteLine();
            writer.Write("FROM ");
            From.WriteSql(writer, sqlGenerator);
            if (where != null && !Where.IsEmpty)
            {
                writer.WriteLine();
                writer.Write("WHERE (");
                Where.WriteSql(writer, sqlGenerator);
                writer.Write(")");
            }

            if (groupBy != null && !GroupBy.IsEmpty)
            {
                writer.WriteLine();
                writer.Write("GROUP BY ");
                GroupBy.WriteSql(writer, sqlGenerator);
            }

            if (orderBy != null && !OrderBy.IsEmpty && (IsTopMost || Top != null))
            {
                writer.WriteLine();
                writer.Write("ORDER BY ");
                OrderBy.WriteSql(writer, sqlGenerator);
            }

            --writer.Indent;
        }
    }
}