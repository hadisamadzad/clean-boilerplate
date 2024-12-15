using Common.Api.Infrastructure;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Operations.Auth;
using Identity.Application.Types.Models.Base.Auth;
using Identity.Application.Types.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api;

public static class AuthApi
{
    public const string EndpointTag = "Auth";

    // Models
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string Email, string Password);
    public record CheckRegistrationRequest();

    // Endpoints
    public static void MapAuthEndpoints(this WebApplication app)
    {
        // Login endpoint
        app.MapGet(Routes.Auth + "profile", async (
            IMediator mediator,
            [FromHeader] string requestedBy) =>
            {
                var userId = requestedBy.Decode();
                return await mediator.Send(new GetUserProfileQuery(userId));
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
                    UserId = value.Id.Encode(),
                    Email = value.Email,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    FullName = value.FullName,
                    Role = value.Role,
                    CreatedAt = value.CreatedAt,
                    UpdatedAt = value.UpdatedAt
                };
            })
            .WithTags(EndpointTag);

        // Login endpoint
        app.MapPost(Routes.Auth + "login", async (
            IMediator mediator,
            [FromHeader] string requestedBy,
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
            })
            .WithTags(EndpointTag);

        // Register endpoint
        app.MapPost(Routes.Auth + "register", async (
            IMediator mediator,
            [FromHeader] string requestedBy,
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
                    Id = value.UserId.Encode()
                };
            })
            .WithTags(EndpointTag);

        // Registration check endpoint
        app.MapGet(Routes.Auth + "registration-check", async (
            IMediator mediator,
            [FromHeader] string requestedBy) =>
            {
                return await mediator.Send(new CheckRegistrationQuery());
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
                    IsRegistrationDone = value
                };
            })
            .WithTags(EndpointTag);

        // Username check endpoint
        app.MapGet(Routes.Auth + "username-check", async (
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
            })
            .WithTags(EndpointTag);

        // Get access token by refresh token endpoint
        app.MapGet(Routes.Auth + "access-token", async (
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
                    AccessToken = value.AccessToken
                };
            })
            .WithTags(EndpointTag);
    }
}