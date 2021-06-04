using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Communal.Persistence.Extensions.Common;
using Identity.Application.Interfaces.Repositories;
using Identity.Application.Models.Users;
using Identity.Domain.Users;
using Identity.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IQueryable<User> _queryable;

        public UserRepository(AppDbContext context) : base(context)
        {
            _queryable = DbContext.Set<User>();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _queryable
                .Include(x => x.Roles)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _queryable
                .Include(x => x.Roles)
                .SingleOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<int> CountUsersByFilterAsync(UserFilter filter)
        {
            var query = _queryable;

            query = query.ApplyFilter(filter);

            return await query.CountAsync();
        }

        public async Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> ids)
        {
            var query = _queryable;
            query = query.AsNoTracking()
                .Include(x => x.Roles);

            // Filter by ids
            if (ids?.Any() == true)
                query = query.Where(x => ids.Contains(x.Id));

            query = query.ApplySort(UserSortBy.CreationDate);

            return await query.ToListAsync();
        }

        public async Task<List<User>> GetUsersByFilterAsync(UserFilter filter)
        {
            var query = _queryable;

            query = query.AsNoTracking();

            // Includes
            if (filter.Include != null)
                if (filter.Include.Role)
                    query = query.Include(x => x.Roles);

            query = query.ApplyFilter(filter);
            query = query.ApplySort(filter.SortBy);

            return await query.Paginate(filter.Page, filter.PageSize).ToListAsync();
        }
    }
}