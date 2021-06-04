using System.Globalization;

namespace Identity.Application.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
                return true;
            return str.Trim().Length == 0;
        }

        public static string ToTitleCase(this string value)
        {
            var ti = new CultureInfo("en-US", false).TextInfo;
            return ti.ToTitleCase(value);
        }

        public static string ToCamelCase(this string value)
        {
            if (value == null || value.Length <= 2)
                return value;
            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }
    }
}