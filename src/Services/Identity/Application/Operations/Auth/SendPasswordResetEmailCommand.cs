using Communal.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.Operations.Auth.Models;

public record SendPasswordResetEmailCommand : IRequest<OperationResult>
{
    public string Email { get; set; }
}
