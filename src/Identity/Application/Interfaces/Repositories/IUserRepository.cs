using Common.Interfaces;
using Identity.Application.Types.Entities;

namespace Identity.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<bool> AnyUsersAsync();
    Task<UserEntity> GetUserByIdAsync(string id);
    Task<UserEntity> GetUserByEmailAsync(string email);
}
