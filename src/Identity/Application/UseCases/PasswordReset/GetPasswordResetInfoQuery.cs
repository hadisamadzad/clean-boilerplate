using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.PasswordReset;

public record GetPasswordResetInfoQuery(string Token) : IRequest<OperationResult>;