using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Users;

public record CreateUserCommand : IRequest<OperationResult>
{
    public required string Email { get; init; }
    public required string Password { get; init; }

    public string FirstName { get; init; }
    public string LastName { get; init; }
}