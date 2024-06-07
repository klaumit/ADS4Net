using System.Data;
using System.Data.Common;

namespace Advantage.Data.Provider
{
    public class AdsRowUpdatedEventArgs : RowUpdatedEventArgs
    {
        public AdsRowUpdatedEventArgs(
            DataRow row,
            IDbCommand command,
            StatementType statementType,
            DataTableMapping tableMapping)
            : base(row, command, statementType, tableMapping)
        {
        }

        public new AdsCommand Command => (AdsCommand)base.Command;
    }
}