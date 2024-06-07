using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Advantage.Data.Provider
{
    public class AdsFactory : DbProviderFactory, IServiceProvider
    {
        public static readonly AdsFactory Instance = new AdsFactory();

        public override DbCommand CreateCommand() => (DbCommand)new AdsCommand();

        public override DbConnection CreateConnection() => (DbConnection)new AdsConnection();

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return (DbCommandBuilder)new AdsCommandBuilder();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return (DbConnectionStringBuilder)new AdsConnectionStringBuilder();
        }

        public override DbDataAdapter CreateDataAdapter() => (DbDataAdapter)new AdsDataAdapter();

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            throw new NotImplementedException();
        }

        public override DbParameter CreateParameter() => (DbParameter)new AdsParameter();

        object IServiceProvider.GetService(Type serviceType)
        {
            object service = (object)null;
            if ((object)serviceType == (object)AdsGreenMethods.SystemDataCommonDbProviderServices_Type)
                service = AdsGreenMethods.AdvantageDataProviderAdsProviderServices_Instance();
            return service;
        }
    }
}