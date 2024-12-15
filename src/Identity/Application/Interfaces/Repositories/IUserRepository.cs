using Identity.Application.Types.Entities;

namespace Identity.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<bool> AnyUsersAsync();
    Task<UserEntity> GetUserByIdAsync(int id);
    Task<UserEntity> GetUserByEmailAsync(string email);
}
