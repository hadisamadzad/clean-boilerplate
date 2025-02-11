using Identity.Application.Interfaces;
using Identity.Application.Interfaces.Repositories;
using Identity.Infrastructure.Database.Repositories;
using MongoDB.Driver;

namespace Identity.Infrastructure.Database;

public class RepositoryManager(IMongoDatabase mongoDatabase) : IRepositoryManager
{
    public IUserRepository Users { get; }
        = new UserRepository(mongoDatabase, "identity.users");

    public async Task<bool> CommitAsync()
    {
        var task = new Task<bool>(() => { return true; });
        return await task;
    }
}
