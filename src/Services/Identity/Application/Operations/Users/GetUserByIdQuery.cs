using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Users;

public record GetUserByIdQuery : IRequest<OperationResult>
{
    public required int UserId { get; init; }
}