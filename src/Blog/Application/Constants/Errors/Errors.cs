using Common.Utilities;

namespace Blog.Application.Constants.Errors;

public static class Errors
{
    // Common
    public static readonly ErrorModel InvalidId =
        new("BLCO-100", "Identity Error", "Invalid ID.");

    // Article
    public static readonly ErrorModel InvalidArticleTitle =
        new("BLAR-100", "Article Error", "Invalid article title.");

    // Article
    public static readonly ErrorModel InvalidArticleSubtitle =
        new("BLAR-101", "Article Error", "Invalid article subtitle.");

    public static readonly ErrorModel InvalidArticleSummary =
    new("BLAR-102", "Article Error", "Invalid article summary.");

    public static readonly ErrorModel InvalidArticleSlug =
        new("BLAR-103", "Article Error", "Invalid article slug.");

    public static readonly ErrorModel InvalidArticleThumbnailUrl =
        new("BLAR-104", "Article Error", "Invalid article thumbnail URL.");

    public static readonly ErrorModel InvalidArticleCoverImageUrl =
        new("BLAR-105", "Article Error", "Invalid article cover image URL.");

    // Tag
    public static readonly ErrorModel InvalidTagName =
        new("BLTG-101", "Tag Error", "Invalid tag name.");

    public static readonly ErrorModel InvalidTagSlug =
        new("BLTG-102", "Tag Error", "Invalid tag slug.");

    public static readonly ErrorModel DuplicateTag =
        new("BLTG-103", "Tag Error", "Duplicate tag.");
}