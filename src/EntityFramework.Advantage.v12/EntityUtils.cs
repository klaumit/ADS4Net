using System;

namespace Advantage.Data.Provider
{
    internal static class EntityUtils
    {
        internal static T CheckArgumentNull<T>(T value, string parameterName) where T : class
        {
            return value != null ? value : throw new ArgumentNullException(parameterName);
        }
    }
}