using Blog.Application.Interfaces.Repositories;
using Blog.Application.Types.Entities;
using Common.Persistence.MongoDB;
using MongoDB.Driver;

namespace Blog.Infrastructure.Database.Repositories;

public class SettingRepository(IMongoDatabase database, string collectionName) :
    MongoDbRepositoryBase<SettingEntity>(database, collectionName), ISettingRepository
{
    public async Task<SettingEntity> GetBlogSettingAsync()
    {
        return await _collection.Find(x => x.Id == "blog_settings").SingleOrDefaultAsync();
    }
}
