using System;
using System.Linq;

namespace Identity.Application.Extensions
{
    public static class EnumExtension
    {
        public static int GetMaxLength(this Enum value)
        {
            var type = value.GetType();
            var names = Enum.GetNames(type);
            return names.Select(name => name.Length).Concat(new[] { 0 }).Max();
        }
    }
}