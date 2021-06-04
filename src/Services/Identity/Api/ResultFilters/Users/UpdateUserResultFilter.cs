using System.Threading.Tasks;
using Communal.Api.Infrastructure;
using Identity.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.Filters.Users
{
    public class UpdateUserResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is User value)
                result.Value = new
                {
                    Id = value.Id.Encode(),
                    Username = value.Username,
                    UpdatedAt = value.UpdatedAt
                };

            await next();
        }
    }
}
