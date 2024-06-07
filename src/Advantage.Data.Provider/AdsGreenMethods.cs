using System;
using System.Reflection;

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
            if ((object)AdvantageDataProviderAdsProviderServices_Instance_FieldInfo == null)
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                var type = Type.GetType(
                    string.Format(
                        "Advantage.Data.Provider.AdsProviderServices, Advantage.Data.Entity, Version={0}.{1}.0.{2}, Culture=neutral, PublicKeyToken=be02e6ebac78e7ab",
                        version.Major, version.Minor, version.MinorRevision), false);
                if ((object)type != null)
                    AdvantageDataProviderAdsProviderServices_Instance_FieldInfo =
                        type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            }

            return AdvantageDataProviderAdsProviderServices_Instance_GetValue();
        }

        // TODO [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
        private static object AdvantageDataProviderAdsProviderServices_Instance_GetValue()
        {
            object obj = null;
            if ((object)AdvantageDataProviderAdsProviderServices_Instance_FieldInfo != null)
                obj = AdvantageDataProviderAdsProviderServices_Instance_FieldInfo
                    .GetValue(null);
            return obj;
        }
    }
}