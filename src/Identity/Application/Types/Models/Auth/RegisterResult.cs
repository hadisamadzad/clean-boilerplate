namespace Identity.Application.Types.Models.Base.Auth;

public record RegisterResult
{
    public int UserId { get; set; }
    public string Email { get; set; }
    public string ActivationToken { get; set; }
}