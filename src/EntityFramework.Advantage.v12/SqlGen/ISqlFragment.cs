namespace Advantage.Data.Provider.SqlGen
{
    internal interface ISqlFragment
    {
        void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator);
    }
}