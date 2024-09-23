using Common.Api.Infrastructure;
using Identity.Application.Types.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Auth;

public class GetProfileResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is UserModel value)
            result.Value = new
            {
                Id = value.Id.Encode(),
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                FullName = value.FullName,
                Role = value.Role,
                CreatedAt = value.CreatedAt,
                UpdatedAt = value.UpdatedAt
            };

        await next();
    }
}