using System.Threading.Tasks;
using Contracts.ServiceBus.Rpc.Identity;
using Contracts.ServiceBus.Rpc.Identity.Models;
using Identity.Application.Models.Queries.Users;
using Identity.Application.Models.Responses.Users;
using MassTransit;
using MediatR;

namespace Identity.ServiceBus.Consumers
{
    public class GetUserBusRequestConsumer : IConsumer<GetUserBusRequest>
    {
        private readonly IMediator _mediator;

        public GetUserBusRequestConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GetUserBusRequest> context)
        {
            // Payload
            var userId = context.Message.UserId;

            // Operation
            var operation = await _mediator.Send(new GetUserQuery { UserId = userId });

            var value = operation.Value as UserResponse;

            // Response
            await context.RespondAsync(new GetUserBusResponse
            {
                User = new UserBusModel
                {
                    Id = value.Id,
                    Username = value.Username,
                    Email = value.Email,
                    FirstName = value.FirstName,
                    LastName = value.LastName
                }
            });
        }
    }
}