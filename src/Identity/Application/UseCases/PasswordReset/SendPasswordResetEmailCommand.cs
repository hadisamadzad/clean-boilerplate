using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.PasswordReset;

public record SendPasswordResetEmailCommand(string Email) : IRequest<OperationResult>;