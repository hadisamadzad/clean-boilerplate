using Blog.Application.Types.Entities;
using Blog.Application.UseCases.Subscribers;
using Common.Interfaces;
using Common.Utilities.OperationResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api;

public class SubscriberEndpoints : IEndpoint
{
    const string Route = "api/subscribers/";
    const string Tag = "Subscribers";

    // Endpoints
    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup(Route).WithTags(Tag);

        // Endpoint for creating a subscriber
        group.MapPost("/", async (
            IMediator mediator,
            [FromBody] CreateSubscriberRequest request) =>
            {
                return await mediator.Send(new CreateSubscriberCommand(request.Email));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                var value = (SubscriberEntity)operation.Value;
                return new
                {
                    Email = value.Email,
                };
            });

        // Endpoint for unsubscribing a subscriber
        group.MapDelete("/", async (
            IMediator mediator,
            [FromBody] UnsubscribeSubscriberRequest request) =>
            {
                return await mediator.Send(new UnsubscribeSubscriberCommand(request.Email));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                var value = (SubscriberEntity)operation.Value;
                return new
                {
                    Email = value.Email
                };
            });
    }
}

// Models
public record CreateSubscriberRequest(string Email);
public record UnsubscribeSubscriberRequest(string Email);