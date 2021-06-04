using System.Linq;
using System.Threading.Tasks;
using Communal.Api.Infrastructure;
using Identity.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.Filters.Users
{
    public class UpdateUserRolesResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is User value)
                result.Value = new
                {
                    Eid = value.Id.Encode(),
                    Username = value.Username,
                    RoleCount = value.Roles.Count,
                    UpdatedAt = value.UpdatedAt
                };

            await next();
        }
    }
}