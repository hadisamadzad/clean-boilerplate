using Common.Interfaces;
using Identity.Application.Types.Entities;

namespace Identity.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<bool> AnyAsync();
    Task<UserEntity> GetByIdAsync(string id);
    Task<UserEntity> GetByEmailAsync(string email);
}
