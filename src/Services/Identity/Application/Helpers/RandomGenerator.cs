using System;

namespace Identity.Application.Helpers
{
    public static class AllowedCharacters
    {
        public const string Numeric = "123456789";
        public const string Numeric0 = "1234567890";
        public const string AlphanumericCase = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const string Alphanumeric = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string AlphanumericReadable = "1234567890abcdefghkmnprstuvwyz";
    }

    public static class RandomGenerator
    {
        public static string GenerateString(int length, string allowedCharacters, string prefix = null)
        {
            var randomChars = new char[length];
            var rndgen = new Random();

            for (var i = 0; i < length; ++i)
                randomChars[i] = allowedCharacters[rndgen.Next(allowedCharacters.Length)];

            return $"{prefix}{new string(randomChars)}";
        }

        public static int GenerateNumber(int max, int min = 0)
        {
            if (max < min || max < 0 || min < 0)
                throw new ArgumentOutOfRangeException();
            return new Random().Next(min, max);
        }
    }
}