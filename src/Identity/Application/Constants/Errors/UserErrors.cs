using Common.Application.Constants;
using Common.Application.Infrastructure.Errors;

namespace Identity.Application.Constants.Errors;

public static class UserErrors
{
    // Code ranges for UserManagement is between 10001 and 19999
    public static ErrorModel UserNotFoundError = new(
        code: 12001,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "User not found"
        ));

    public static ErrorModel DuplicateUsernameError = new(
        code: 12002,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Email is already registered"
        ));

    public static ErrorModel InactiveUserError = new(
        code: 12003,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "User is not active"
        ));

    public static ErrorModel InsufficientAccessLevelError = new(
        code: 12004,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "User role is insufficient for this operation"
        ));
}