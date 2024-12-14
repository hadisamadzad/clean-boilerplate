using Identity.Application.Interfaces.Repositories;
using Identity.Application.Types.Entities.Users;
using MongoDB.Driver;

namespace Identity.Infrastructure.Database.Repositories;

public class UserRepository(IMongoDatabase database, string collectionName) :
    MongoDbRepositoryBase<User>(database, collectionName), IUserRepository
{
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        email = email.ToLower();
        return await _collection.Find(x => x.Email.ToLower() == email).SingleOrDefaultAsync();
    }
}
