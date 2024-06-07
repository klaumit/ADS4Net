using System.Data;
using System.Data.Common;

namespace Advantage.Data.Provider
{
    public class AdsRowUpdatingEventArgs : RowUpdatingEventArgs
    {
        public AdsRowUpdatingEventArgs(
            DataRow row,
            IDbCommand command,
            StatementType statementType,
            DataTableMapping tableMapping)
            : base(row, command, statementType, tableMapping)
        {
        }

        public new AdsCommand Command
        {
            get => (AdsCommand)base.Command;
            set => base.Command = value;
        }
    }
}