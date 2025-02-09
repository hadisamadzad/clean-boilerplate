using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Users;

public record CreateUserCommand : IRequest<OperationResult>
{
    public int AdminUserId { get; set; }
    public required string Email { get; init; }
    public required string Password { get; init; }

    public string FirstName { get; init; }
    public string LastName { get; init; }
}