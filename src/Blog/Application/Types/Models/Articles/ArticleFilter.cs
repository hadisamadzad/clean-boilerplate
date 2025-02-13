using Blog.Application.Types.Entities;
using Common.Utilities.Pagination;

namespace Blog.Application.Types.Models.Articles;

public record ArticleFilter : PaginationFilter
{
    public string? Keyword { get; set; }
    public List<string> TagIds { get; set; } = [];
    public List<ArticleState> Statuses { get; set; } = [];
    public ArticleSortBy SortBy { get; set; } = ArticleSortBy.CreatedAtNewest;
}
