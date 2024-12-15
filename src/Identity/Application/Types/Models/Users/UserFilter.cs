using Common.Application.Infrastructure.Pagination;
using Identity.Application.Types.Entities;

namespace Identity.Application.Types.Models.Users;

public record UserFilter : PaginationFilter
{
    public string Keyword { get; init; }
    public string Email { get; init; }
    public List<UserState> States { get; init; }

    public UserIncludes Include { get; init; }
    public UserSortBy? SortBy { get; init; }
}
