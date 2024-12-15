using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.PasswordReset;

public record GetPasswordResetInfoQuery(string Token) : IRequest<OperationResult>;