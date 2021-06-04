using System.Linq;
using System.Threading.Tasks;
using Communal.Api.Infrastructure;
using Communal.Application.Infrastructure.Pagination;
using Identity.Application.Models.Responses.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.Filters.Users
{
    public class GetUsersResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is PaginatedList<UserResponse> value)
                result.Value = new
                {
                    value.Page,
                    value.PageSize,
                    value.TotalCount,
                    Data = value.Data.Select(x => new
                    {
                        Eid = x.Id.Encode(),
                        Username = x.Username,
                        Fullname = x.Fullname,
                        RoleTitles = x.Roles.Select(x => x.Name),
                        Email = x.Email,
                        State = nameof(x.State),
                        UpdatedAt = x.UpdatedAt
                    })
                };

            await next();
        }
    }
}
