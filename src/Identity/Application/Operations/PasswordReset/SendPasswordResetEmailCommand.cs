using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.PasswordReset;

public record SendPasswordResetEmailCommand(string Email) : IRequest<OperationResult>;