using Common.Application.Constants;
using Common.Application.Infrastructure.Errors;

namespace Identity.Application.Constants.Errors;

public static class AuthErrors
{
    // Code ranges for Identity is between 10001 and 19999
    public static ErrorModel InvalidLoginError = new(
        code: 11001,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Provided username or password is incorrect"
        ));

    public static ErrorModel InvalidCredentialsError = new(
        code: 11002,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Invalid credentials"
        ));

    public static ErrorModel NotActivatedUserLoginError = new(
        code: 11003,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "User is not activated"
        ));

    public static ErrorModel LockoutUserLoginError = new(
        code: 11004,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "User is locked out due to multiple failed logins"
        ));

    public static ErrorModel InvalidActivationTokenError = new(
        code: 11005,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Invalid activation token"
        ));

    public static ErrorModel InvalidUserStateForActivationTokenError = new(
        code: 11006,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "User is already activated"
        ));

    public static ErrorModel InconsistentTokenError = new(
        code: 11007,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Inconsistent token"
        ));

    public static ErrorModel InvalidPasswordResetTokenError = new(
        code: 11008,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Invalid password reset token"
        ));
}