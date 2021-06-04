using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Application.Models.Users;
using Identity.Domain.Users;

namespace Identity.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<int> CountUsersByFilterAsync(UserFilter filter);
        Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids);
        Task<List<User>> GetUsersByFilterAsync(UserFilter filter);
    }
}