using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Identity.Application.Models.Auth;

namespace Identity.Api.Filters.Auth
{
    public class TokenResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is TokenResponse value)
                result.Value = new
                {
                    AccessToken = value.AccessToken
                };

            await next();
        }
    }
}