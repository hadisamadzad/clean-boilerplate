using System.Linq;
using Identity.Application.Extensions;
using Identity.Application.Models.Users;
using Identity.Domain.Users;

namespace Identity.Persistence.Extensions
{
    public static class UserQueryableExtension
    {
        public static IQueryable<User> ApplyFilter(this IQueryable<User> query, UserFilter filter)
        {
            // Filter by keyword
            if (!filter.Keyword.IsNullOrEmpty())
                query = query.Where(x =>
                    x.FirstName.ToLower().Contains(filter.Keyword.ToLower().Trim()) ||
                    x.LastName.ToLower().Contains(filter.Keyword.ToLower().Trim()) ||
                    x.Username.ToLower().Contains(filter.Keyword.ToLower().Trim()));

            // Filter by email
            if (!filter.Email.IsNullOrEmpty())
                query = query.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower().Trim()));

            // Filter by statuses
            if (filter.States?.Any() == true)
                query = query.Where(x => filter.States.Contains(x.State));

            return query;
        }

        public static IQueryable<User> ApplySort(this IQueryable<User> query, UserSortBy? sortBy)
        {
            return sortBy switch
            {
                UserSortBy.CreationDate => query.OrderBy(x => x.CreatedAt),
                UserSortBy.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
                UserSortBy.LastLoginDate => query.OrderBy(x => x.LastLoginDate),
                UserSortBy.LastLoginDateDescending => query.OrderByDescending(x => x.LastLoginDate),
                _ => query.OrderByDescending(x => x.Id)
            };
        }
    }
}