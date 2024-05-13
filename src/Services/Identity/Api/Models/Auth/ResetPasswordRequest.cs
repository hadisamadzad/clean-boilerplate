namespace Identity.Api.Models.Auth;

public record ResetPasswordRequest(string Token, string NewPassword);