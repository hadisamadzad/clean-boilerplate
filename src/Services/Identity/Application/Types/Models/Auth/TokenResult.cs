namespace Identity.Application.Types.Models.Base.Auth;

public record TokenResult
{
    public string AccessToken { get; set; }
}