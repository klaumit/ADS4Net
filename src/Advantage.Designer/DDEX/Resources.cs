using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Advantage.Data.DDEX
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                    resourceMan = new ResourceManager("Advantage.Data.DDEX.Resources",
                        typeof(Resources).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => resourceCulture;
            set => resourceCulture = value;
        }

        internal static string DataView_AdsServer
        {
            get
            {
                return ResourceManager.GetString(nameof(DataView_AdsServer),
                    resourceCulture);
            }
        }

        internal static string Node_Functions
        {
            get
            {
                return ResourceManager.GetString(nameof(Node_Functions),
                    resourceCulture);
            }
        }

        internal static string Node_StoredProcedures
        {
            get
            {
                return ResourceManager.GetString(nameof(Node_StoredProcedures),
                    resourceCulture);
            }
        }

        internal static string Node_Tables
        {
            get => ResourceManager.GetString(nameof(Node_Tables),
                resourceCulture);
        }

        internal static string Node_Users
        {
            get => ResourceManager.GetString(nameof(Node_Users),
                resourceCulture);
        }

        internal static string Node_Views
        {
            get => ResourceManager.GetString(nameof(Node_Views),
                resourceCulture);
        }

        internal static string Provider_Description
        {
            get
            {
                return ResourceManager.GetString(nameof(Provider_Description),
                    resourceCulture);
            }
        }

        internal static string Provider_DisplayName
        {
            get
            {
                return ResourceManager.GetString(nameof(Provider_DisplayName),
                    resourceCulture);
            }
        }

        internal static string Provider_ShortDisplayName
        {
            get
            {
                return ResourceManager.GetString(nameof(Provider_ShortDisplayName),
                    resourceCulture);
            }
        }
    }
}