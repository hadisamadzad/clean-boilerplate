using Blog.Application.Types.Entities;

namespace Blog.Application.Types.Models.Articles;

public record ArticleModel
{
    public string Id { get; set; } = string.Empty;
    public string AuthorId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;

    public int TimeToReadInMinute { get; set; }
    public int Likes { get; set; }

    public ArticleState Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? ArchivedAt { get; set; }
}