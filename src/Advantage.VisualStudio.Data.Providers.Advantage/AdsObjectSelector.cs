using Advantage.Data.Provider;
using Microsoft.VisualStudio.Data.Framework;
using Microsoft.VisualStudio.Data.Framework.AdoDotNet;
using Microsoft.VisualStudio.Data.Services;
using Microsoft.VisualStudio.Data.Services.SupportEntities;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal class AdsObjectSelector : DataObjectSelector
    {
        private const string rootEnumerationSql =
            "SELECT   USER() as [User],   DATABASE() as [DataSource]FROM system.iota;";

        protected virtual IList<string> GetRequiredRestrictions(string typeName, object[] parameters)
        {
            return typeName != null
                ? base.GetRequiredRestrictions(typeName, parameters)
                : throw new ArgumentNullException(nameof(typeName));
        }

        protected override IVsDataReader SelectObjects(
            string typeName,
            object[] restrictions,
            string[] properties,
            object[] parameters)
        {
            if (typeName == null)
                throw new ArgumentNullException(nameof(typeName));
            if (!(((DataSiteableObject<IVsDataConnection>)this).Site.GetLockedProviderObject() is AdsConnection
                    lockedProviderObject))
                throw new NotSupportedException();
            try
            {
                if (this.Site.State != (DataConnectionState)1)
                    this.Site.Open();
                AdsCommand command = lockedProviderObject.CreateCommand();
                if (typeName.Equals("", StringComparison.OrdinalIgnoreCase))
                {
                    ((DbCommand)command).CommandText =
                        "SELECT   USER() as [User],   DATABASE() as [DataSource]FROM system.iota;";
                    return (IVsDataReader)new AdoDotNetReader((DbDataReader)command.ExecuteReader());
                }

                if (restrictions.Length == 0 || !(restrictions[0] is string))
                    throw new ArgumentException("Missing required restriction(s).");
                throw new NotSupportedException();
            }
            finally
            {
                ((DataSiteableObject<IVsDataConnection>)this).Site.UnlockProviderObject();
            }
        }
    }
}