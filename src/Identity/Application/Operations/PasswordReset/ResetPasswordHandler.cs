﻿using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Operations.Auth;
using Identity.Application.Types.Configs;
using MediatR;
using Microsoft.Extensions.Options;

namespace Identity.Application.Operations.PasswordReset;

internal class ResetPasswordHandler(IUnitOfWork unitOfWork,
    IOptions<PasswordResetConfig> passwordResetConfig)
    : IRequestHandler<ResetPasswordCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PasswordResetConfig _passwordResetConfig = passwordResetConfig.Value;

    public async Task<OperationResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new ResetPasswordValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var (succeeded, email) = PasswordResetTokenHelper.ReadPasswordResetToken(request.Token);
        if (!succeeded)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.InvalidToken);

        var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.InvalidId);

        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unprocessable,
                value: Errors.LockedUser);

        user.PasswordHash = PasswordHelper.Hash(request.NewPassword);

        _ = await _unitOfWork.Users.UpdateAsync(user);

        return new OperationResult(OperationStatus.Completed, value: user.Id);
    }
}