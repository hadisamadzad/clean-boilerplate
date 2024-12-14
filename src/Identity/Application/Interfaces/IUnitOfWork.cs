using Identity.Application.Interfaces.Repositories;

namespace Identity.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    Task<bool> CommitAsync();
}
