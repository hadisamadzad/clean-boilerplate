namespace Identity.Api.Models.Users;

public record UpdateUserPasswordRequest(string CurrentPassword, string NewPassword);
