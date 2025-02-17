using Common.Utilities;

namespace Blog.Application.Constants;

public static class Errors
{
    // Common
    public static readonly ErrorModel InvalidId =
        new("BLCO-100", "Blog Error", "Invalid ID.");

    public static readonly ErrorModel InvalidSlug =
        new("BLCO-101", "Blog Error", "Invalid slug.");

    public static readonly ErrorModel SettingsNotFound =
        new("BLCO-102", "Blog Error", "Settings not found.");

    public static readonly ErrorModel InvalidEmail =
        new("BLCO-103", "Blog Error", "Invalid email address.");

    // Article
    public static readonly ErrorModel InvalidArticleTitle =
        new("BLAR-100", "Article Error", "Invalid article title.");

    public static readonly ErrorModel InvalidArticleSubtitle =
        new("BLAR-101", "Article Error", "Invalid article subtitle.");

    public static readonly ErrorModel InvalidArticleSummary =
    new("BLAR-102", "Article Error", "Invalid article summary.");

    public static readonly ErrorModel InvalidArticleThumbnailUrl =
        new("BLAR-104", "Article Error", "Invalid article thumbnail URL.");

    public static readonly ErrorModel InvalidArticleCoverImageUrl =
        new("BLAR-105", "Article Error", "Invalid article cover image URL.");

    public static readonly ErrorModel ArticleNotFound =
        new("BLAR-106", "Article Error", "Article not found.");

    public static readonly ErrorModel DuplicateArticle =
        new("BLAR-107", "Tag Error", "Duplicate article.");

    // Tag
    public static readonly ErrorModel InvalidTagName =
        new("BLTG-101", "Tag Error", "Invalid tag name.");

    public static readonly ErrorModel DuplicateTag =
        new("BLTG-102", "Tag Error", "Duplicate tag.");

    // BlogSettings
    public static readonly ErrorModel InvalidBlogTitle =
        new("BLST-101", "Setting Error", "Invalid blog title.");

    public static readonly ErrorModel InvalidBlogDescription =
        new("BLST-102", "Setting Error", "Invalid blog description.");

    public static readonly ErrorModel InvalidSeoTitle =
        new("BLST-103", "Setting Error", "Invalid SEO title.");

    public static readonly ErrorModel InvalidSeoDescription =
        new("BLST-104", "Setting Error", "Invalid SEO description.");

    public static readonly ErrorModel InvalidBlogUrl =
        new("BLST-105", "Setting Error", "Invalid blog URL.");

    public static readonly ErrorModel InvalidBlogLogoUrl =
        new("BLST-106", "Setting Error", "Invalid blog logo URL.");

    public static readonly ErrorModel InvalidSocialNetworkName =
        new("BLST-107", "Setting Error", "Invalid social network name.");
}