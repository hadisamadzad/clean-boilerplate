using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Auth;

public record RefreshTokenQuery(string RefreshToken) : IRequest<OperationResult>;