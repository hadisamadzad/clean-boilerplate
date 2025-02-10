using Common.Utilities.OperationResult;
using Identity.Application.UseCases.PasswordReset;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api;

public static class PasswordResetApi
{
    const string Route = "api/auth/";
    const string Tag = "PasswordReset";

    // Models
    public record SendPasswordResetEmailRequest(string Email);
    public record ResetPasswordRequest(string Token, string NewPassword);

    // Endpoints
    public static void MapPasswordResetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(Route).WithTags(Tag);

        // Endpoint for sending password reset email
        group.MapPost(Route + "password-reset", async (
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

                var value = (string)operation.Value;
                return new
                {
                    UserId = value
                };
            });

        // Endpoint for getting password reset info
        group.MapGet(Route + "password-reset", async (
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

                var value = (string)operation.Value;
                return new
                {
                    Email = value
                };
            });

        // Endpoint for resetting user password
        group.MapPatch(Route + "password-reset", async (
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

                var value = (string)operation.Value;
                return new
                {
                    UserId = value
                };
            });
    }
}