using Common.Application.Helpers;

namespace Identity.Application.Helpers;

public static class ActivationTokenHelper
{
    private const string _ = "|&|";
    private const string Key = "***";
    const int TypeIndex = 0;
    const int EmailIndex = 1;
    const int ExpirationIndex = 2;

    public static string GenerateActivationToken(string email, DateTime expiration)
    {
        var randomPart = RandomGenerator
            .GenerateString(length: 4, allowedCharacters: AllowedCharacters.AlphanumericCase);

        var payload = $"activation{_}{email}{_}{expiration:yyyy-MM-dd}{_}{randomPart}";
        var base64 = StringEncryptor.Encrypt(payload, Key);
        return Base64Helper.ConvertBase64ToBase64Url(base64);
    }

    public static (bool Succeeded, string Email) ReadActivationToken(string activationToken)
    {
        var base64 = Base64Helper.ConvertBase64UrlToBase64(activationToken);
        var payload = StringEncryptor.Decrypt(base64, Key);
        var values = payload.Split(_);

        if (values[TypeIndex] != "activation")
            return (Succeeded: false, Email: string.Empty);

        if (DateTime.Parse(values[ExpirationIndex]) < DateTime.UtcNow)
            return (Succeeded: false, Email: string.Empty);

        return (Succeeded: true, Email: values[EmailIndex]);
    }
}