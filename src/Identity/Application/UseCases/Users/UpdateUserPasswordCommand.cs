using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Users;

public record UpdateUserPasswordCommand(
    string AdminUserId,
    string UserId,
    string CurrentPassword,
    string NewPassword) : IRequest<OperationResult>;