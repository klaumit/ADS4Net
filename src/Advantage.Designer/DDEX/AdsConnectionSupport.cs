using System;
using Microsoft.VisualStudio.Data;
using Microsoft.VisualStudio.Data.AdoDotNet;

namespace Advantage.Data.DDEX
{
    internal class AdsConnectionSupport : AdoDotNetConnectionSupport
    {
        private DataViewSupport viewSupport;
        private DataObjectSupport objectSupport;
        private DataObjectIdentifierResolver objectIdentifierResolver;

        public AdsConnectionSupport()
            : base("Advantage.Data.Provider")
        {
        }

        public virtual bool Open(bool doPromptCheck) => base.Open(doPromptCheck);

        protected virtual DataObjectIdentifierConverter CreateObjectIdentifierConverter()
        {
            return new AdsDataObjectIdentifierConverter(
                Site as DataConnection);
        }

        protected virtual DataSourceInformation CreateDataSourceInformation()
        {
            return new AdsDataSourceInformation(
                Site as DataConnection);
        }

        protected virtual DataObjectItemComparer CreateObjectItemComparer()
        {
            return new AdsDataObjectItemComparer(
                Site as DataConnection);
        }

        protected virtual object GetServiceImpl(Type serviceType)
        {
            if (serviceType == (object)typeof(DataViewSupport))
            {
                if (viewSupport == null)
                    viewSupport = new AdsDataViewSupport();
                return viewSupport;
            }

            if (serviceType == (object)typeof(DataObjectSupport))
            {
                if (objectSupport == null)
                    objectSupport = new AdsDataObjectSupport();
                return objectSupport;
            }

            if (serviceType != (object)typeof(DataObjectIdentifierResolver))
                return base.GetServiceImpl(serviceType);
            if (objectIdentifierResolver == null)
                objectIdentifierResolver =
                    new AdsDataObjectIdentifierResolver(
                        Site as DataConnection);
            return objectIdentifierResolver;
        }

        public virtual DataParameter[] DeriveParameters(
            string command,
            int commandType,
            int commandTimeout)
        {
            return base.DeriveParameters(command, commandType, commandTimeout);
        }
    }
}