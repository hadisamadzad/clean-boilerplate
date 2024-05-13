namespace Identity.Application.Types.Configs;

public record SecurityTokenConfig
{
    public const string Key = "SecurityToken";

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string AccessTokenSecretKey { get; set; }
    public TimeSpan AccessTokenLifetime { get; set; }
    public string RefreshTokenSecretKey { get; set; }
    public TimeSpan RefreshTokenLifetime { get; set; }
}