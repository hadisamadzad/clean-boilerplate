using Common.Application.Constants;
using Common.Application.Infrastructure.Errors;

namespace Identity.Application.Constants.Errors;

public static class Errors
{
    // Code ranges for Identity is between 10001 and 19999

    public static ErrorModel InvalidInputValidationError = new(
        code: 10000,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Invalid input"
        ));

    public static ErrorModel InvalidIdentifierError = new(
        code: 10000,
        title: "Identifier Error",
        (
            Language: Language.English,
            Message: "Invalid identifier"
        ));

    public static ErrorModel InvalidEmailError = new(
        code: 10002,
        title: "Email Error",
        (
            Language: Language.English,
            Message: "Invalid email address"
        ),
        (
            Language: Language.Persian,
            Message: "ایمیل وارد شده درست نمی باشد"
        ));

    public static ErrorModel NotSecurePasswordValidationError = new(
        code: 10003,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Password is not strong enough"
        ));

    public static ErrorModel InvalidPasswordValidationError = new(
        code: 10003,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Password is not provided"
        ));

    public static ErrorModel WeakPasswordValidationError = new(
        code: 10003,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Provided password is not strong enough"
        ));

    public static ErrorModel InvalidFirstNameError = new(
        code: 10004,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Invalid first name"
        ));

    public static ErrorModel InvalidLastNameError = new(
        code: 10005,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Invalid last name"
        ));

    public static ErrorModel InvalidUserStateValidationError = new(
        code: 10006,
        title: "Identity Error",
        (
            Language: Language.English,
            Message: "Invalid user state"
        ));

    public static ErrorModel InvalidPhoneNumberError = new(
        code: 10007,
        title: "Phone Number Error",
        (
            Language: Language.English,
            Message: "Invalid phone number"
        ));

    public static ErrorModel InvalidUrlError = new(
        code: 10007,
        title: "URL Error",
        (
            Language: Language.English,
            Message: "Invalid url"
        ));

    public static ErrorModel InvalidCityError = new(
        code: 10008,
        title: "City Name Error",
        (
            Language: Language.English,
            Message: "Invalid city name"
        ));

    public static ErrorModel InvalidStateOrProvinceError = new(
        code: 10008,
        title: "State/Province Error",
        (
            Language: Language.English,
            Message: "Invalid state or province name"
        ));

    public static ErrorModel InvalidAddressError = new(
        code: 10009,
        title: "Address Error",
        (
            Language: Language.English,
            Message: "Invalid address"
        ));

    public static ErrorModel InvalidTimeZoneError = new(
        code: 10010,
        title: "TimeZone Name Error",
        (
            Language: Language.English,
            Message: "Invalid TimeZone name"
        ));
}