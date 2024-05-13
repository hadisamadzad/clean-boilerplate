using Communal.Persistence.Extensions.Common;
using Identity.Application.Interfaces.Repositories;
using Identity.Application.Types.Entities.Users;
using Identity.Application.Types.Models.Users;
using Identity.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Identity.Database.Repositories.Users;

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
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        email = email.ToLower();
        return await _queryable
            .SingleOrDefaultAsync(x => x.Email.ToLower() == email);
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
        query = query.AsNoTracking();

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
        //if (filter.Include != null)

        query = query.ApplyFilter(filter);
        query = query.ApplySort(filter.SortBy);

        return await query.Paginate(filter.Page, filter.PageSize).ToListAsync();
    }
}
