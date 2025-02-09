using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.PasswordReset;

public record ResetPasswordCommand(string Token, string NewPassword)
    : IRequest<OperationResult>;
