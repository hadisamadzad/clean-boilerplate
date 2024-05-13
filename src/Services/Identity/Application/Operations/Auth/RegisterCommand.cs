using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record RegisterCommand
(
    string Email,
    string Password
) : IRequest<OperationResult>;