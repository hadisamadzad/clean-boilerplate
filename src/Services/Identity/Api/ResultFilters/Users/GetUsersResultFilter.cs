using Communal.Api.Infrastructure;
using Communal.Application.Infrastructure.Pagination;
using Identity.Application.Types.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Users;

public class GetUsersResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is PaginatedList<UserModel> value)
            result.Value = new
            {
                value.Page,
                value.PageSize,
                value.TotalCount,
                Data = value.Data.Select(x => new
                {
                    Id = x.Id.Encode(),
                    Email = x.Email,
                    FullName = x.FullName,
                    Role = nameof(x.Role),
                    State = nameof(x.State),
                    UpdatedAt = x.UpdatedAt
                })
            };

        await next();
    }
}