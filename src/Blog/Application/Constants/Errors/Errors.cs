using Common.Utilities;

namespace Blog.Application.Constants.Errors;

public static class Errors
{
    // Common
    public static readonly ErrorModel InvalidId =
        new("BLCO-100", "Identity Error", "Invalid ID.");
}