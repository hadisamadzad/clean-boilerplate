using Identity.Application.Types.Entities.Users;

namespace Identity.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
}
