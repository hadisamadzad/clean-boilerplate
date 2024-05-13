using Communal.Api.Infrastructure;
using Identity.Application.Types.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Users;

public class GetUserResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is UserModel value)
            result.Value = new
            {
                Id = value.Id.Encode(),
                Email = value.Email,
                Mobile = value.Mobile,
                Role = value.Role,
                FirstName = value.FirstName,
                LastName = value.LastName,
                FullName = value.FullName,
                CreatedAt = value.CreatedAt,
                UpdatedAt = value.UpdatedAt
            };

        await next();
    }
}