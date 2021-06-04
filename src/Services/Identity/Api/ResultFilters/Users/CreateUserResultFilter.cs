using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Identity.Domain.Users;

namespace Identity.Api.Filters.Users
{
    public class CreateUserResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is User value)
                result.Value = new
                {
                    Id = value.Id,
                    Username = value.Username
                };

            await next();
        }
    }
}