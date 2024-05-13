using Identity.Application.Types.Models.Base.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Auth;

public class TokenResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is TokenResult value)
            result.Value = new
            {
                AccessToken = value.AccessToken
            };

        await next();
    }
}