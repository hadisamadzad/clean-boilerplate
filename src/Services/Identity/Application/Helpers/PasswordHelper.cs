using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Identity.Application.Helpers;

public static class PasswordHelper
{
    private const int SaltSize = 128 / 8; // 128 bit
    private const int KeySize = 256 / 8; // 128 bit
    private const int Iteration = 10000;

    public static string Hash(string password)
    {
        using var algorithm =
            new Rfc2898DeriveBytes(password, SaltSize, Iteration, HashAlgorithmName.SHA512);
        var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{key}.{salt}";
    }

    public static bool CheckPasswordHash(string hash, string password)
    {
        var parts = hash.Split('.', 2);

        if (parts.Length != 2)
        {
            throw new FormatException("Unexpected hash format. " +
              "Should be formatted as `{hash}.{salt}`");
        }

        //var iterations = Convert.ToInt32(parts[0]);
        var key = Convert.FromBase64String(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);

        using var algorithm =
            new Rfc2898DeriveBytes(password, salt, Iteration, HashAlgorithmName.SHA512);
        var keyToCheck = algorithm.GetBytes(KeySize);
        return keyToCheck.SequenceEqual(key);
    }

    public static PasswordScore CheckStrength(string password)
    {
        if (password.Length < 1)
            return PasswordScore.Blank;
        if (password.Length < 6)
            return PasswordScore.VeryWeak;

        var score = 0;

        if (password.Length >= 8)
            score++;
        if (password.Length >= 12)
            score++;
        if (Regex.Match(password, @"\d").Success)
            score++;
        if (Regex.Match(password, "[a-z]").Success && Regex.Match(password, "[A-Z]").Success)
            score++;
        if (Regex.Match(password, ".[~,!,@,#,$,%,^,&,*,(,),-,_,=,?,_]").Success)
            score++;

        if (score > (int)PasswordScore.VeryStrong)
            score = (int)PasswordScore.VeryStrong;

        return (PasswordScore)score;
    }
}

public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5
}