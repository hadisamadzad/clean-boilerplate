using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record GetPasswordResetInfoQuery : IRequest<OperationResult>
{
    public required string Token { get; init; }
}