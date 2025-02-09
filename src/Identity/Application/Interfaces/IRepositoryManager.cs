using Identity.Application.Interfaces.Repositories;

namespace Identity.Application.Interfaces;

public interface IRepositoryManager
{
    IUserRepository Users { get; }

    Task<bool> CommitAsync();
}
