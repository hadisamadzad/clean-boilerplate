using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth;

public record SendPasswordResetEmailCommand : IRequest<OperationResult>
{
    public string Email { get; set; }
}
