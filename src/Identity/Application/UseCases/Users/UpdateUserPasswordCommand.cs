﻿using Common.Application.Infrastructure.Operations;
using MediatR;

namespace Identity.Application.UseCases.Users;

public record UpdateUserPasswordCommand : IRequest<OperationResult>
{
    public string UserId { get; init; }
    public string CurrentPassword { get; init; }
    public string NewPassword { get; init; }
}