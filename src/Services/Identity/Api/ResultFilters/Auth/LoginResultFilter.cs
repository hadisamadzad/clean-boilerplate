using Identity.Application.Types.Models.Base.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Auth;

public class LoginResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is LoginResult value)
            result.Value = new
            {
                Email = value.Email,
                FullName = value.FullName,
                AccessToken = value.AccessToken,
                RefreshToken = value.RefreshToken,
            };

        await next();
    }
}
