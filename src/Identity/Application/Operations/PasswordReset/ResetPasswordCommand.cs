using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.PasswordReset;

public record ResetPasswordCommand(string Token, string NewPassword)
    : IRequest<OperationResult>;
