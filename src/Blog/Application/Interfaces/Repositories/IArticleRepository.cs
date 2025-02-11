using Blog.Application.Types.Entities;
using Common.Interfaces;

namespace Blog.Application.Interfaces.Repositories;

public interface IArticleRepository : IRepository<ArticleEntity>
{
    Task<ArticleEntity> GetArticleByIdAsync(string id);
}