using System.ComponentModel.DataAnnotations;
using Communal.Api.Extensions.AspNetCore;
using Communal.Api.Infrastructure;
using Identity.Api.Models.Auth;
using Identity.Api.ResultFilters.Auth;
using Identity.Application.Operations.Auth;
using Identity.Application.Operations.Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    // NOTE Endpoint remove as we don't want to give public access
    //[HttpPost(Routes.Auth + "register")]
    //[RegisterResultFilter]
    //public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    //{
    //    var operation = await _mediator.Send(new RegisterCommand
    //    (
    //        Email: request.Email,
    //        Password: request.Password
    //    ));
    //
    //    return this.ReturnResponse(operation);
    //}

    [HttpPost(Routes.Auth + "login")]
    [LoginResultFilter]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var operation = await _mediator.Send(new LoginCommand
        (
            Email: request.Email,
            Password: request.Password
        ));

        return this.ReturnResponse(operation);
    }

    [HttpGet(Routes.Auth + "profile")]
    [GetProfileResultFilter]
    public async Task<IActionResult> GetProfile([FromHeader, Required] string RequestedBy)
    {
        var userId = RequestedBy.Decode();

        // Operation
        var operation = await _mediator.Send(new GetUserProfileQuery(RequestedBy: userId));

        return this.ReturnResponse(operation);
    }

    [HttpGet(Routes.Auth + "username-check")]
    [CheckUsernameResultFilter]
    public async Task<IActionResult> CheckUsername([FromQuery] string email)
    {
        // Operation
        var operation = await _mediator.Send(new CheckUsernameQuery
        {
            Email = email
        });

        return this.ReturnResponse(operation);
    }

    [HttpPatch(Routes.Auth + "activation")]
    [ActivateUserResultFilter]
    public async Task<IActionResult> Activate([FromBody] ActivateRequest request)
    {
        // Operation
        var operation = await _mediator.Send(new ActivateCommand
        {
            ActivationToken = request.ActivationToken
        });

        return this.ReturnResponse(operation);
    }

    [HttpGet(Routes.Auth + "access-token")]
    [TokenResultFilter]
    public async Task<IActionResult> GetAccessToken([FromHeader] string refresh)
    {
        var operation = await _mediator.Send(new RefreshTokenQuery
        {
            RefreshToken = refresh
        });

        return this.ReturnResponse(operation);
    }

    [HttpPost(Routes.Auth + "password-reset")]
    [SendPasswordResetEmailResultFilter]
    public async Task<IActionResult> SendPasswordResetEmail([FromBody] SendPasswordResetEmailRequest request)
    {
        var operation = await _mediator.Send(new SendPasswordResetEmailCommand
        {
            Email = request.Email
        });

        return this.ReturnResponse(operation);
    }

    [HttpGet(Routes.Auth + "password-reset")]
    [GetPasswordResetInfoResultFilter]
    public async Task<IActionResult> GetPasswordResetInfo([FromQuery, Required] string token)
    {
        var operation = await _mediator.Send(new GetPasswordResetInfoQuery
        {
            Token = token
        });

        return this.ReturnResponse(operation);
    }

    [HttpPatch(Routes.Auth + "password-reset")]
    [ResetPasswordResultFilter]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var operation = await _mediator.Send(new ResetPasswordCommand
        (
            Token: request.Token,
            NewPassword: request.NewPassword
        ));

        return this.ReturnResponse(operation);
    }
}