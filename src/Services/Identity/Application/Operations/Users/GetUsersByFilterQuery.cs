using Communal.Application.Infrastructure.Operations;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.Operations.Users;

public record GetUsersByFilterQuery : IRequest<OperationResult>
{
    public required UserFilter Filter { get; init; }
}
