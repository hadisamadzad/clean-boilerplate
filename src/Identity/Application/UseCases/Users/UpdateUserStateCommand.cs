﻿using Common.Application.Infrastructure.Operations;
using Identity.Application.Types.Entities;
using MediatR;

namespace Identity.Application.UseCases.Users;

public record UpdateUserStateCommand : IRequest<OperationResult>
{
    public string UserId { get; init; }

    public UserState State { get; init; }
}
