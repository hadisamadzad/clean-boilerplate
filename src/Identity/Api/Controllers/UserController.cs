using Common.Api.Extensions.AspNetCore;
using Common.Api.Infrastructure;
using Identity.Api.Models.Users;
using Identity.Api.ResultFilters.Users;
using Identity.Application.Operations.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost(Routes.Users)]
    [CreateUserResultFilter]
    public async Task<IActionResult> AddUser(
        [FromHeader] string requestedBy,
        [FromBody] CreateUserRequest request)
    {
        // Operation
        var operation = await _mediator.Send(new CreateUserCommand
        {
            AdminUserId = requestedBy.Decode(),

            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        });

        return this.ReturnResponse(operation);
    }

    [HttpGet(Routes.Users + "{userId}")]
    [GetUserResultFilter]
    public async Task<IActionResult> GetUser([FromRoute] string userId)
    {
        // Operation
        var operation = await _mediator.Send(new GetUserByIdQuery
        {
            UserId = userId.Decode()
        });

        return this.ReturnResponse(operation);
    }

    [HttpPatch(Routes.Users + "{userId}")]
    [UpdateUserResultFilter]
    public async Task<IActionResult> UpdateUserInfo([FromRoute] string userId, [FromBody] UpdateUserRequest request)
    {
        // Operation
        var operation = await _mediator.Send(new UpdateUserCommand
        {
            UserId = userId.Decode(),
            FirstName = request.FirstName,
            LastName = request.LastName
        });

        return this.ReturnResponse(operation);
    }

    [HttpPatch(Routes.Users + "{userId}/state")]
    [UpdateUserResultFilter]
    public async Task<IActionResult> UpdateUserState([FromRoute] string userId, [FromBody] UpdateUserStateRequest request)
    {
        // Operation
        var operation = await _mediator.Send(new UpdateUserStateCommand
        {
            UserId = userId.Decode(),
            State = request.State
        });

        return this.ReturnResponse(operation);
    }

    [HttpPatch(Routes.Users + "{userId}/password")]
    [UpdateUserResultFilter]
    public async Task<IActionResult> UpdateUserPassword([FromRoute] string userId, [FromBody] UpdateUserPasswordRequest request)
    {
        // Operation
        var operation = await _mediator.Send(new UpdateUserPasswordCommand
        {
            UserId = userId.Decode(),
            CurrentPassword = request.CurrentPassword,
            NewPassword = request.NewPassword
        });

        return this.ReturnResponse(operation);
    }
}
