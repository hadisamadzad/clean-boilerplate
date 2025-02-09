using Common.Api.Infrastructure;
using Common.Application.Infrastructure.Operations;
using Identity.Application.UseCases.PasswordReset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api;

public static class PasswordResetApi
{
    public const string EndpointTag = "PasswordReset";

    // Models
    public record SendPasswordResetEmailRequest(string Email);
    public record ResetPasswordRequest(string Token, string NewPassword);

    // Endpoints
    public static void MapPasswordResetEndpoints(this WebApplication app)
    {
        // Get password reset info
        app.MapGet(Routes.Auth + "password-reset", async (
            IMediator mediator,
            [FromQuery] string token) =>
            {
                return await mediator.Send(new GetPasswordResetInfoQuery(token));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not string value)
                    return operation.Value;

                return new
                {
                    Email = value
                };
            })
            .WithTags(EndpointTag);

        // Send password reset email
        app.MapPost(Routes.Auth + "password-reset", async (
            IMediator mediator,
            [FromBody] SendPasswordResetEmailRequest request) =>
            {
                return await mediator.Send(new SendPasswordResetEmailCommand(request.Email));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not int value)
                    return operation.Value;

                return new
                {
                    UserId = value.Encode()
                };
            })
            .WithTags(EndpointTag);

        // Reset password
        app.MapPatch(Routes.Auth + "password-reset", async (
            IMediator mediator,
            [FromBody] ResetPasswordRequest request) =>
            {
                return await mediator.Send(new ResetPasswordCommand(request.Token, request.NewPassword));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                if (operation.Value is not int value)
                    return operation.Value;

                return new
                {
                    UserId = value.Encode()
                };
            })
            .WithTags(EndpointTag);
    }
}