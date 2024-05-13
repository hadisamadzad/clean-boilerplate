using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record CheckUsernameQuery : IRequest<OperationResult>
{
    public required string Email { get; init; }
}