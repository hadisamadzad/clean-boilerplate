using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record ResetPasswordCommand
(
    string Token,
    string NewPassword
) : IRequest<OperationResult>;
