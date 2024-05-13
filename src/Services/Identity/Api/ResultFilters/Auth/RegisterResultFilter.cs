using Communal.Api.Infrastructure;
using Identity.Application.Types.Models.Base.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Auth;

public class RegisterResultFilter : ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result?.Value is RegisterResult value)
            result.Value = new
            {
                Id = value.UserId.Encode(),
                Email = value.Email,
                //ActivationToken = value.ActivationToken,
            };

        await next();
    }
}