using System.Threading.Tasks;
using Identity.Application.Models.Responses.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.ResultFilters.Users
{
    public class GetUserResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is UserResponse value)
                result.Value = new
                {
                    value.Id,
                    value.Username,
                    value.FirstName,
                    value.LastName,
                    value.Fullname,
                    Roles = value.Roles.ConvertAll(x => new
                    {
                        x.Code,
                        x.Name
                    }),
                    value.Email,
                    value.CreatedAt,
                    value.UpdatedAt
                };

            await next();
        }
    }
}