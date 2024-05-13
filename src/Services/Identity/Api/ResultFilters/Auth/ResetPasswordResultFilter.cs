using Communal.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Auth;

public class ResetPasswordResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is int value)
            result.Value = new
            {
                Id = value.Encode()
            };

        await next();
    }
}
