using Communal.Application.Constants;
using Communal.Application.Infrastructure.Errors;

namespace Identity.Application.Constants
{
    public static class AuthErrors
    {
        // Code ranges for UserManagement is between 10001 and 19999
        public static ErrorModel InvalidLoginError = new ErrorModel(
            code: 11001,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Invalid login information"
            ));

        public static ErrorModel InvalidCredentialsError = new ErrorModel(
            code: 11002,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Invalid credentials"
            ));

        public static ErrorModel UnauthorizedRequestError = new ErrorModel(
            code: 11003,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Unauthorized request"
            ));
    }
}