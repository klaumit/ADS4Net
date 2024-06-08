using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using AVDPAPR = Advantage.VisualStudio.Data.Providers.Advantage.Properties.Resources;

namespace Advantage.VisualStudio.Data.Providers.Advantage.Properties
{
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(
                        (object)AVDPAPR.resourceMan,
                        (object)null))
                    AVDPAPR.resourceMan =
                        new ResourceManager("AVDPAPR",
                            typeof(AVDPAPR).Assembly);
                return AVDPAPR.resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => AVDPAPR.resourceCulture;
            set => AVDPAPR.resourceCulture = value;
        }

        internal static string AdsMappedObjectConverter_InvalidParameters
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(AdsMappedObjectConverter_InvalidParameters),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string Category_Identifier
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(Category_Identifier),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string DataView_AdsServer
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(DataView_AdsServer),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string Description_Name
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(Description_Name),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string Node_StoredProcedures
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(Node_StoredProcedures),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string Node_Tables
        {
            get => AVDPAPR.ResourceManager.GetString(
                nameof(Node_Tables),
                AVDPAPR.resourceCulture);
        }

        internal static string Node_Views
        {
            get => AVDPAPR.ResourceManager.GetString(
                nameof(Node_Views),
                AVDPAPR.resourceCulture);
        }

        internal static string Property_Name
        {
            get => AVDPAPR.ResourceManager.GetString(
                nameof(Property_Name),
                AVDPAPR.resourceCulture);
        }

        internal static string Provider_Description
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(Provider_Description),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string Provider_DisplayName
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(Provider_DisplayName),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string Provider_ShortDisplayName
        {
            get
            {
                return AVDPAPR.ResourceManager.GetString(
                    nameof(Provider_ShortDisplayName),
                    AVDPAPR.resourceCulture);
            }
        }

        internal static string Type_User
        {
            get => AVDPAPR.ResourceManager.GetString(
                nameof(Type_User),
                AVDPAPR.resourceCulture);
        }
    }
}