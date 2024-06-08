using System.Text;

namespace Advantage.VisualStudio.Data.Providers.Advantage
{
    internal static class AdsDebugHelpers
    {
        public static string ArrayToString(object[] identifier)
        {
            StringBuilder stringBuilder = new StringBuilder("[");
            for (int index = 0; index < identifier.Length; ++index)
            {
                if (index > 0)
                    stringBuilder.Append(", ");
                if (identifier[index] == null)
                    stringBuilder.Append("null");
                else
                    stringBuilder.Append(identifier[index].ToString());
            }

            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}