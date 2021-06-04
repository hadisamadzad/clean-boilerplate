using System.Threading.Tasks;
using Communal.Api.Extensions.AspNetCore;
using Communal.Api.Infrastructure;
using Identity.Api.Filters.Users;
using Identity.Api.Models.Requests.Users;
using Identity.Api.ResultFilters.Users;
using Identity.Application.Commands.Users;
using Identity.Application.Models.Commands.Users;
using Identity.Application.Models.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create
        [HttpPost(Routes.Users)]
        [CreateUserResultFilter]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            // Operation
            var operation = await _mediator.Send(new CreateUserCommand
            {
                Username = request.Username,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,

                Email = request.Email
            });

            return this.ReturnResponse(operation);
        }

        // Detail
        [HttpGet(Routes.Users + "{ueid}")]
        [GetUserResultFilter]
        public async Task<IActionResult> DetailUser([FromRoute] string ueid)
        {
            // Decode
            var userId = ueid.Decode();

            // Operation
            var operation = await _mediator.Send(new GetUserQuery
            {
                UserId = userId
            });

            return this.ReturnResponse(operation);
        }

        [HttpPatch(Routes.Users + "{ueid}")]
        [UpdateUserResultFilter]
        public async Task<IActionResult> UpdateUser([FromRoute] string ueid, [FromBody] UpdateUserRequest request)
        {
            // Decode
            var userId = ueid.Decode();

            // Operation
            var operation = await _mediator.Send(new UpdateUserCommand
            {
                UserId = userId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                State = request.State,
                Email = request.Email
            });

            return this.ReturnResponse(operation);
        }

        [HttpPatch(Routes.Users + "{ueid}/password")]
        [UpdateUserResultFilter]
        public async Task<IActionResult> UpdateUserPassword([FromRoute] string ueid, [FromBody] UpdateUserPasswordRequest request)
        {
            // Decode
            var userId = ueid.Decode();

            // Operation
            var operation = await _mediator.Send(new UpdateUserPasswordCommand
            {
                UserId = userId,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword
            });

            return Ok();
            return this.ReturnResponse(operation);
        }

        // Update Roles
        [HttpPatch(Routes.Users + "{ueid}/roles")]
        [UpdateUserRolesResultFilter]
        public async Task<IActionResult> UpdateUserRoles([FromRoute] string ueid, [FromBody] UpdateUserRolesRequest request)
        {
            // Decode
            var userId = ueid.Decode();

            // Operation
            var operation = await _mediator.Send(new UpdateUserRolesCommand
            {
                UserId = userId,
                Roles = request.Roles
            });

            return this.ReturnResponse(operation);
        }
    }
}