using System.Collections.Generic;
using Communal.Application.Infrastructure.Pagination;
using Identity.Application.Models.Responses.Users;
using Identity.Domain.Users;

namespace Identity.Application.Models.Users
{
    public class UserFilter : PaginationFilter
    {
        protected UserFilter(int page, int pageSize) : base(page, pageSize)
        {
        }

        public string Keyword { get; set; }
        public string Email { get; set; }
        public List<UserState> States { get; set; }

        public UserIncludes Include { get; set; }
        public UserSortBy? SortBy { get; set; }
    }
}