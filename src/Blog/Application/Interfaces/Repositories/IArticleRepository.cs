using Blog.Application.Types.Entities;
using Blog.Application.Types.Models.Articles;
using Common.Interfaces;

namespace Blog.Application.Interfaces.Repositories;

public interface IArticleRepository : IRepository<ArticleEntity>
{
    Task<ArticleEntity> GetArticleByIdAsync(string id);
    Task<ArticleEntity> GetArticleBySlugAsync(string slug);
    Task<List<ArticleEntity>> GetArticlesByIdsAsync(IEnumerable<string> ids);
    Task<List<ArticleEntity>> GetArticlesByFilterAsync(ArticleFilter filter);
    Task<int> CountArticlesByFilterAsync(ArticleFilter filter);
}