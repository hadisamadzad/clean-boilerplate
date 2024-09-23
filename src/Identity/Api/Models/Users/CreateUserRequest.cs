namespace Identity.Api.Models.Users;

public record CreateUserRequest(
    string Email, string Password, string FirstName, string LastName);