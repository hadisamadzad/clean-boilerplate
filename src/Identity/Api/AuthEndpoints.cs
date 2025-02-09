using Common.Utilities.Operations;
using Identity.Application.Types.Models.Auth;
using Identity.Application.Types.Models.Users;
using Identity.Application.UseCases.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api;

public static class AuthEndpoints
{
    const string Route = "api/auth/";
    const string Tag = "Auth";

    // Models
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string Email, string Password);
    public record CheckRegistrationRequest();

    // Endpoints
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(Route).WithTags(Tag);

        // Ownership check endpoint
        group.MapGet("ownership-check", async (
            IMediator mediator) =>
            {
                return await mediator.Send(new GetOwnershipStatusQuery());
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not bool value)
                    return operation.Value;

                return new
                {
                    IsOwnershipDone = value
                };
            });

        // Endpoint for getting user profile
        group.MapGet("profile", async (
            IMediator mediator,
            [FromHeader] string requestedBy) =>
            {
                return await mediator.Send(new GetUserProfileQuery(requestedBy));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not UserModel value)
                    return operation.Value;

                return new
                {
                    UserId = value.UserId,
                    Email = value.Email,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    FullName = value.FullName,
                    Role = value.Role,
                    CreatedAt = value.CreatedAt,
                    UpdatedAt = value.UpdatedAt
                };
            });

        // Endpoint for registration
        group.MapPost("register", async (
            IMediator mediator,
            [FromBody] RegisterRequest request) =>
            {
                return await mediator.Send(new RegisterCommand
                (
                    Email: request.Email?.Trim(),
                    Password: request.Password?.Trim()
                ));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not RegisterResult value)
                    return operation.Value;

                return new
                {
                    Id = value.UserId
                };
            });

        // Endpoint for logging in
        group.MapPost("login", async (
            IMediator mediator,
            [FromBody] LoginRequest request) =>
            {
                return await mediator.Send(new LoginCommand
                (
                    Email: request.Email?.Trim(),
                    Password: request.Password?.Trim()
                ));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not LoginResult value)
                    return operation.Value;

                return new
                {
                    value.Email,
                    value.FullName,
                    value.AccessToken,
                    value.RefreshToken,
                };
            });

        // Endpoint for checking username
        group.MapGet("username-check", async (
            IMediator mediator,
            [FromQuery] string email) =>
            {
                return await mediator.Send(new CheckUsernameQuery(email));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not bool value)
                    return operation.Value;

                return new
                {
                    IsAvailable = value
                };
            });

        // Get access token by refresh token endpoint
        group.MapGet("access-token", async (
            IMediator mediator,
            [FromHeader] string refreshToken) =>
            {
                return await mediator.Send(new RefreshTokenQuery(refreshToken));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not TokenResult value)
                    return operation.Value;

                return new
                {
                    NewAccessToken = value.AccessToken
                };
            });
    }
}