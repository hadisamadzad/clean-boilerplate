using Communal.Application.Constants;
using Communal.Application.Infrastructure.Errors;

namespace Communal.Application.Errors;

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