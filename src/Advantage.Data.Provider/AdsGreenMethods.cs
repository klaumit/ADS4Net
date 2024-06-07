using System;
using System.Reflection;
using System.Security.Permissions;

namespace Advantage.Data.Provider
{
    internal static class AdsGreenMethods
    {
        private const string ExtensionAssemblyRef =
            "System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        private const string SystemDataCommonDbProviderServices_TypeName =
            "System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        private const string AdvantageDataProviderAdsProviderServices_TypeName =
            "Advantage.Data.Provider.AdsProviderServices, Advantage.Data.Entity, Version={0}.{1}.0.{2}, Culture=neutral, PublicKeyToken=be02e6ebac78e7ab";

        internal static Type SystemDataCommonDbProviderServices_Type = Type.GetType(
            "System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
            false);

        private static FieldInfo AdvantageDataProviderAdsProviderServices_Instance_FieldInfo;

        internal static object AdvantageDataProviderAdsProviderServices_Instance()
        {
            if ((object)AdsGreenMethods.AdvantageDataProviderAdsProviderServices_Instance_FieldInfo == null)
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                Type type = Type.GetType(
                    string.Format(
                        "Advantage.Data.Provider.AdsProviderServices, Advantage.Data.Entity, Version={0}.{1}.0.{2}, Culture=neutral, PublicKeyToken=be02e6ebac78e7ab",
                        (object)version.Major, (object)version.Minor, (object)version.MinorRevision), false);
                if ((object)type != null)
                    AdsGreenMethods.AdvantageDataProviderAdsProviderServices_Instance_FieldInfo =
                        type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            }

            return AdsGreenMethods.AdvantageDataProviderAdsProviderServices_Instance_GetValue();
        }

        // TODO [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        private static object AdvantageDataProviderAdsProviderServices_Instance_GetValue()
        {
            object obj = (object)null;
            if ((object)AdsGreenMethods.AdvantageDataProviderAdsProviderServices_Instance_FieldInfo != null)
                obj = AdsGreenMethods.AdvantageDataProviderAdsProviderServices_Instance_FieldInfo
                    .GetValue((object)null);
            return obj;
        }
    }
}