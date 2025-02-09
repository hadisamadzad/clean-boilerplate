using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Users;

public record GetUserByIdQuery : IRequest<OperationResult>
{
    public required string UserId { get; init; }
}