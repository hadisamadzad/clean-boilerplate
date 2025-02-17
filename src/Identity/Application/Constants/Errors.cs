using Common.Utilities;

namespace Identity.Application.Constants;

public static class Errors
{
    // Common
    public static readonly ErrorModel InvalidId =
        new("IDCO-100", "Identity Error", "Invalid ID.");

    public static readonly ErrorModel InvalidEmail =
        new("IDCO-101", "Identity Error", "Invalid email address.");

    public static readonly ErrorModel WeakPassword =
        new("IDCO-102", "Identity Error", "Password is weak.");

    public static readonly ErrorModel InvalidPassword =
        new("IDCO-103", "Identity Error", "Password is not provided.");

    public static readonly ErrorModel InvalidFirstName =
        new("IDCO-104", "Identity Error", "Invalid first name.");

    public static readonly ErrorModel InvalidLastName =
        new("IDCO-105", "Identity Error", "Invalid last name.");

    public static readonly ErrorModel InvalidUserState =
        new("IDCO-106", "Identity Error", "Invalid user state.");


    // Auth
    public static readonly ErrorModel InvalidCredentials =
        new("IDAU-101", "Identity Error", "Provided username or password.");

    public static readonly ErrorModel LockedUser =
        new("IDAU-102", "Identity Error", "User is locked out due to multiple failed logins.");

    public static readonly ErrorModel OwnershipAlreadyDone =
        new("IDAU-103", "Identity Error", "Registration is already done.");

    public static readonly ErrorModel DuplicateUsername =
        new("IDAU-104", "Identity Error", "User is already registered.");

    public static readonly ErrorModel InsufficientAccessLevel =
        new("IDAU-105", "Identity Error", "Access denied.");

    public static readonly ErrorModel InvalidToken =
        new("IDAU-105", "Identity Error", "Invalid token.");
}