﻿using Common.Application.Infrastructure.Operations;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.UseCases.Auth;

// Handler
internal class GetOwnershipStatusHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOwnershipStatusQuery, OperationResult>
{
    public async Task<OperationResult> Handle(GetOwnershipStatusQuery request, CancellationToken cancellationToken)
    {
        // Check initial registration
        var isAlreadyOwned = await unitOfWork.Users.AnyUsersAsync();

        return new OperationResult(OperationStatus.Completed, value: isAlreadyOwned);
    }
}

// Model
public record GetOwnershipStatusQuery() : IRequest<OperationResult>;