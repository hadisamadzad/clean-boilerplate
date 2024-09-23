using Common.Application.Infrastructure.Operations;
using Identity.Application.Types.Entities.Users;
using MediatR;

namespace Identity.Application.Operations.Users;

public record UpdateUserStateCommand : IRequest<OperationResult>
{
    public int UserId { get; init; }

    public UserState State { get; init; }
}
