using Identity.Application.Interfaces.Repositories;
using Identity.Application.Types.Entities;
using MongoDB.Driver;

namespace Identity.Infrastructure.Database.Repositories;

public class UserRepository(IMongoDatabase database, string collectionName) :
    MongoDbRepositoryBase<UserEntity>(database, collectionName), IUserRepository
{
    public async Task<bool> AnyUsersAsync()
    {
        return await _collection.Find(x => true).AnyAsync();
    }

    public async Task<UserEntity> GetUserByIdAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }

    public async Task<UserEntity> GetUserByEmailAsync(string email)
    {
        email = email.ToLower();
        return await _collection.Find(x => x.Email.ToLower() == email).SingleOrDefaultAsync();
    }
}
