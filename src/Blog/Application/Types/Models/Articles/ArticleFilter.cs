using Blog.Application.Types.Entities;
using Common.Utilities.Pagination;

namespace Blog.Application.Types.Models.Articles;

public record ArticleFilter(
    string Keyword,
    List<ArticleState> States,
    ArticleSortBy? SortBy) : PaginationFilter;
