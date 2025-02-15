using Blog.Application.Interfaces.Repositories;
using Blog.Application.Types.Entities;
using Blog.Application.Types.Models.Articles;
using Blog.Infrastructure.Database.Extensions;
using Common.Persistence.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Blog.Infrastructure.Database.Repositories;

public class ArticleRepository(IMongoDatabase database, string collectionName) :
    MongoDbRepositoryBase<ArticleEntity>(database, collectionName), IArticleRepository
{
    public async Task<ArticleEntity> GetArticleByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }
    public async Task<ArticleEntity> GetArticleBySlugAsync(string slug)
    {
        return await _collection.Find(x => x.Slug == slug).SingleOrDefaultAsync();
    }
    public async Task<List<ArticleEntity>> GetArticlesByIdsAsync(IEnumerable<string> ids)
    {
        return await _collection.Find(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<List<ArticleEntity>> GetArticlesByFilterAsync(ArticleFilter filter)
    {
        var query = _collection.AsQueryable()
            .Where(x => !x.IsDeleted);
        query = query.ApplyFilter(filter);
        query = query.ApplySort(filter.SortBy);

        if (filter.HasPagination)
            query = query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        return await query.ToListAsync();
    }

    public async Task<int> CountArticlesByFilterAsync(ArticleFilter filter)
    {
        var query = _collection.AsQueryable()
            .Where(x => !x.IsDeleted);
        query = query.ApplyFilter(filter);
        return await query.CountAsync();
    }
}
