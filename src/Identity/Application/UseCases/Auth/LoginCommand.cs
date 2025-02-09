using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Auth;

public record LoginCommand
(
    string Email,
    string Password
) : IRequest<OperationResult>;