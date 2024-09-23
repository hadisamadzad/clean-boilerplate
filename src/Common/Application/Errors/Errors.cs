using Common.Application.Constants;
using Common.Application.Infrastructure.Errors;

namespace Common.Application.Errors;

public record Errors
{
    public static ErrorModel InvalidIdentifierError => new(
        code: 10001,
        title: "Identifier Error",
        (
            Language: Language.English,
            Message: "Invalid identifier"
        ),
        (
            Language: Language.Persian,
            Message: "درخواست ارسال شده نامعتبر است"
        ));
}