using Communal.Api.Infrastructure;
using Identity.Application.Types.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Users;

public class CreateUserResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is User value)
            result.Value = new
            {
                Id = value.Id.Encode(),
                Email = value.Email
            };

        await next();
    }
}
