using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record RefreshTokenQuery : IRequest<OperationResult>
{
    public required string RefreshToken { get; init; }
}