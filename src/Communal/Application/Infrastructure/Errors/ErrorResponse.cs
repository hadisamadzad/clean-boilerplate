using Communal.Application.Constants;

namespace Communal.Application.Infrastructure.Errors;

public class ErrorResponse(ErrorModel error, string language = null)
{
    private const string EnglishLanguage = "en";

    public int Code { get; } = error.Code;
    public string Title { get; } = error.Title;
    public string Message { get; } = language switch
    {
        EnglishLanguage => error.Messages[Language.English],
        _ => error.Messages[Language.English],
    };
}
