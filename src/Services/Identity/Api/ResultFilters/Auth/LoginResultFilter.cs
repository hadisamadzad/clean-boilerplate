using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Identity.Application.Models.Auth;

namespace Identity.Api.Filters.Auth
{
    public class LoginResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is LoginResponse value)
                result.Value = new
                {
                    Username = value.Username,
                    FullName = value.Fullname,
                    AccessToken = value.AccessToken,
                    RefreshToken = value.RefreshToken,
                };

            await next();
        }
    }
}