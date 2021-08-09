using System.Collections.Generic;

namespace XyzCase.Extensions
{
    public static class Extensions
    {

        public static bool IsNullOrEmpty(this string value)
        {
            bool result = string.IsNullOrEmpty(value);
            return result;
        }

        public static string Combine<T>(this IEnumerable<T> source, string seperator = "\t\t")
        {
            string result = string.Join(seperator, source);
            return result;
        }
    }
}
