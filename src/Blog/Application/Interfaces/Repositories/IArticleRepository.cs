using Blog.Application.Types.Entities;
using Blog.Application.Types.Models.Articles;
using Common.Interfaces;

namespace Blog.Application.Interfaces.Repositories;

public interface IArticleRepository : IRepository<ArticleEntity>
{
    Task<ArticleEntity> GetByIdAsync(string id);
    Task<ArticleEntity> GetBySlugAsync(string slug);
    Task<List<ArticleEntity>> GetByIdsAsync(IEnumerable<string> ids);
    Task<List<ArticleEntity>> GetByFilterAsync(ArticleFilter filter);
    Task<int> CountByFilterAsync(ArticleFilter filter);
}