using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Advantage.Data.Provider.Properties
{
    [CompilerGenerated]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan,
                        null))
                    resourceMan = new ResourceManager(
                        "Advantage.Data.Provider.Properties.Resources",
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

        internal static Bitmap Connection
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject(
                    nameof(Connection), resourceCulture);
            }
        }

        internal static Bitmap QueryBuild
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject(
                    nameof(QueryBuild), resourceCulture);
            }
        }

        internal static Bitmap QueryType
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject(nameof(QueryType),
                    resourceCulture);
            }
        }

        internal static Bitmap Welcome
        {
            get
            {
                return (Bitmap)ResourceManager.GetObject(nameof(Welcome),
                    resourceCulture);
            }
        }
    }
}