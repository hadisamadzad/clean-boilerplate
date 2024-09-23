using Identity.Application.Types.Entities.Users;
using Identity.Application.Types.Models.Users;

namespace Identity.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task<int> CountUsersByFilterAsync(UserFilter filter);
    Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids);
    Task<List<User>> GetUsersByFilterAsync(UserFilter filter);
}
