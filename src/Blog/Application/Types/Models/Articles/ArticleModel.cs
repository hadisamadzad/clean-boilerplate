using Blog.Application.Types.Entities;

namespace Blog.Application.Types.Models.Articles;

public record ArticleModel
{
    public string ArticleId { get; set; }
    public ArticleState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}