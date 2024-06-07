using System.Data;
using System.Data.Common;

namespace Advantage.Data.Provider
{
    public class AdsCommandBuilder : DbCommandBuilder
    {
        protected override void ApplyParameterInfo(DbParameter parameter, DataRow row,
            StatementType statementType, bool whereClause)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetParameterName(int parameterOrdinal)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetParameterName(string parameterName)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetParameterPlaceholder(int parameterOrdinal)
        {
            throw new System.NotImplementedException();
        }

        protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
        {
            throw new System.NotImplementedException();
        }
    }
}