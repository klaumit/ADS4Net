using System;
using System.Data.Common;

namespace Advantage.Data.Provider
{
    public class AdsFactory : DbProviderFactory, IServiceProvider
    {
        public static readonly AdsFactory Instance = new AdsFactory();

        public override DbCommand CreateCommand() => new AdsCommand();

        public override DbConnection CreateConnection() => new AdsConnection();

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new AdsCommandBuilder();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new AdsConnectionStringBuilder();
        }

        public override DbDataAdapter CreateDataAdapter() => new AdsDataAdapter();

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            throw new NotImplementedException();
        }

        public override DbParameter CreateParameter() => new AdsParameter();

        object IServiceProvider.GetService(Type serviceType)
        {
            object service = null;
            if (serviceType == (object)AdsGreenMethods.SystemDataCommonDbProviderServices_Type)
                service = AdsGreenMethods.AdvantageDataProviderAdsProviderServices_Instance();
            return service;
        }
    }
}