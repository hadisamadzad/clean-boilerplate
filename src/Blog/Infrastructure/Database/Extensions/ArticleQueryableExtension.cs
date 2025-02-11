using Blog.Application.Types.Entities;
using Blog.Application.Types.Models.Articles;

namespace Blog.Infrastructure.Database.Extensions;

public static class ArticleQueryableExtension
{
    public static IQueryable<ArticleEntity> ApplyFilter(this IQueryable<ArticleEntity> query, ArticleFilter filter)
    {
        // Filter by keyword
        if (!string.IsNullOrEmpty(filter.Keyword))
            query = query.Where(x =>
                x.Title.ToLower().Contains(filter.Keyword.ToLower().Trim()));

        // Filter by statuses
        if (filter.States?.Count == 0)
            query = query.Where(x => filter.States.Contains(x.State));

        return query;
    }

    public static IQueryable<ArticleEntity> ApplySort(this IQueryable<ArticleEntity> query, ArticleSortBy? sortBy)
    {
        return sortBy switch
        {
            ArticleSortBy.CreationDate => query.OrderBy(x => x.CreatedAt),
            ArticleSortBy.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderByDescending(x => x.Id)
        };
    }
}
