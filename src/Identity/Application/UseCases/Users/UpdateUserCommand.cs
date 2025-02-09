using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Users;

public record UpdateUserCommand : IRequest<OperationResult>
{
    public int UserId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}