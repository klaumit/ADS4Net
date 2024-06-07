using System.Data;
using System.Data.Common;

namespace Advantage.Data.Provider
{
    public class AdsConnection : DbConnection
    {
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new System.NotImplementedException();
        }

        public override void Close()
        {
            throw new System.NotImplementedException();
        }

        public override void Open()
        {
            throw new System.NotImplementedException();
        }

        public override string ConnectionString { get; set; }
        public override string Database { get; }
        public override ConnectionState State { get; }
        public override string DataSource { get; }
        public override string ServerVersion { get; }

        protected override DbCommand CreateDbCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}