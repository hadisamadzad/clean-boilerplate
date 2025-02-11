using Blog.Application.Interfaces.Repositories;
using Blog.Application.Types.Entities;
using Common.Persistence.MongoDB;
using MongoDB.Driver;

namespace Blog.Infrastructure.Database.Repositories;

public class ArticleRepository(IMongoDatabase database, string collectionName) :
    MongoDbRepositoryBase<ArticleEntity>(database, collectionName), IArticleRepository
{
    public async Task<ArticleEntity> GetArticleByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }
}
