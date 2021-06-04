using Communal.Application.Constants;
using Communal.Application.Infrastructure.Errors;

namespace Identity.Application.Constants
{
    public static class Errors
    {
        // Code ranges for UserManagement is between 10001 and 19999

        public static ErrorModel InvalidInputValidationError = new ErrorModel(
            code: 10000,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Invalid input"
            ));

        public static ErrorModel InvalidUsernameValidationError = new ErrorModel(
            code: 10001,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Invalid username"
            ));

        public static ErrorModel InvalidEmailValidationError = new ErrorModel(
            code: 10002,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Invalid email address"
            ));

        public static ErrorModel InvalidPasswordValidationError = new ErrorModel(
            code: 10003,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Password is not provided"
            ));

        public static ErrorModel InvalidFirstNameValidationError = new ErrorModel(
            code: 10004,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Invalid first name"
            ));

        public static ErrorModel InvalidLastNameValidationError = new ErrorModel(
            code: 10005,
            title: "UserManagement Error",
            (
                Language: Language.English,
                Message: "Invalid last name"
            ));
    }
}