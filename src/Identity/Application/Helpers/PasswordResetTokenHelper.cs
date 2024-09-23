using Common.Application.Helpers;

namespace Identity.Application.Helpers;

public static class PasswordResetTokenHelper
{
    private const string _ = "|&|";
    private const string Key = "***";
    const int TypeIndex = 0;
    const int EmailIndex = 1;
    const int ExpirationIndex = 2;

    public static string GeneratePasswordResetToken(string email, DateTime expiration)
    {
        var randomPart = RandomGenerator
            .GenerateString(length: 4, allowedCharacters: AllowedCharacters.AlphanumericCase);

        var payload = $"password-reset{_}{email}{_}{expiration:yyyy-MM-dd}{_}{randomPart}";
        var base64 = StringEncryptor.Encrypt(payload, Key);
        return Base64Helper.ConvertBase64ToBase64Url(base64);
    }

    public static (bool Succeeded, string Email) ReadPasswordResetToken(string passwordResetToken)
    {
        var base64 = Base64Helper.ConvertBase64UrlToBase64(passwordResetToken);
        var payload = StringEncryptor.Decrypt(base64, Key);
        var values = payload.Split(_);

        if (values[TypeIndex] != "password-reset")
            return (Succeeded: false, Email: string.Empty);

        if (DateTime.Parse(values[ExpirationIndex]) < DateTime.UtcNow)
            return (Succeeded: false, Email: string.Empty);

        return (
            Succeeded: true, Email: values[EmailIndex]);
    }
}