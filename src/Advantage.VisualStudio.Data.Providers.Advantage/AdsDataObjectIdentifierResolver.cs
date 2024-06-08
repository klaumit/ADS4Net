using Microsoft.VisualStudio.Data.Framework;
using Microsoft.VisualStudio.Data.Services;
using Microsoft.VisualStudio.Data.Services.SupportEntities;
using System;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal class AdsDataObjectIdentifierResolver : DataObjectIdentifierResolver
    {
        public override object[] ExpandIdentifier(string typeName, object[] partialIdentifier)
        {
            if (typeName == null)
                throw new ArgumentNullException(nameof(typeName));
            IVsDataObjectType ivsDataObjectType = (IVsDataObjectType)null;
            if (((IServiceProvider)((DataSiteableObject<IVsDataConnection>)this).Site).GetService(
                    typeof(IVsDataObjectSupportModel)) is IVsDataObjectSupportModel service1 &&
                service1.Types.ContainsKey(typeName))
                ivsDataObjectType = service1.Types[typeName];
            if (ivsDataObjectType == null)
                throw new ArgumentException("Invalid type " + typeName + ".");
            object[] objArray = new object[ivsDataObjectType.Identifier.Count];
            if (partialIdentifier != null)
            {
                if (partialIdentifier.Length > ivsDataObjectType.Identifier.Count)
                    throw new ArgumentException("Invalid partial identifier.");
                partialIdentifier.CopyTo((Array)objArray,
                    ivsDataObjectType.Identifier.Count - partialIdentifier.Length);
            }

            if (!(((IServiceProvider)((DataSiteableObject<IVsDataConnection>)this).Site).GetService(
                    typeof(IVsDataSourceInformation)) is IVsDataSourceInformation service2))
                return objArray;
            if (ivsDataObjectType.Identifier.Count > 0 && !(objArray[0] is string))
                objArray[0] = (object)(service2["DefaultCatalog"] as string);
            if (ivsDataObjectType.Identifier.Count > 1 && !(objArray[1] is string))
                objArray[1] = (object)(service2["DefaultSchema"] as string);
            return objArray;
        }

        public virtual object[] ContractIdentifier(string typeName, object[] fullIdentifier)
        {
            switch (typeName)
            {
                case null:
                    throw new ArgumentNullException(nameof(typeName));
                case "":
                case "User":
                    return base.ContractIdentifier(typeName, fullIdentifier);
                default:
                    IVsDataObjectType ivsDataObjectType = (IVsDataObjectType)null;
                    if (((IServiceProvider)((DataSiteableObject<IVsDataConnection>)this).Site).GetService(
                            typeof(IVsDataObjectSupportModel)) is IVsDataObjectSupportModel service1 &&
                        service1.Types.ContainsKey(typeName))
                        ivsDataObjectType = service1.Types[typeName];
                    if (ivsDataObjectType == null)
                        throw new ArgumentException("Invalid type " + typeName + ".");
                    object[] objArray = new object[ivsDataObjectType.Identifier.Count];
                    if (fullIdentifier != null)
                    {
                        if (fullIdentifier.Length > ivsDataObjectType.Identifier.Count)
                            throw new ArgumentException("Invalid full identifier.");
                        fullIdentifier.CopyTo((Array)objArray,
                            ivsDataObjectType.Identifier.Count - fullIdentifier.Length);
                    }

                    if (!(((IServiceProvider)((DataSiteableObject<IVsDataConnection>)this).Site).GetService(
                            typeof(IVsDataSourceInformation)) is IVsDataSourceInformation service2) ||
                        !(((IServiceProvider)((DataSiteableObject<IVsDataConnection>)this).Site).GetService(
                            typeof(IVsDataObjectMemberComparer)) is IVsDataObjectMemberComparer service3))
                        return objArray;
                    if (ivsDataObjectType.Identifier.Count > 0 && objArray.Length > 0 && objArray[0] != null)
                    {
                        string str = service2["DefaultCatalog"] as string;
                        if (service3.Compare("", objArray, 0, (object)str) == 0)
                            objArray[0] = (object)null;
                    }

                    if (ivsDataObjectType.Identifier.Count > 1 && objArray.Length > 1 && objArray[1] != null)
                    {
                        string str = "::this";
                        if (service3.Compare(typeName, objArray, 1, (object)str) == 0)
                            objArray[1] = (object)null;
                    }

                    return objArray;
            }
        }
    }
}