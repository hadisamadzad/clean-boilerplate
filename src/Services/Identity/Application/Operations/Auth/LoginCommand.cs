using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record LoginCommand
(
    string Email,
    string Password
) : IRequest<OperationResult>;