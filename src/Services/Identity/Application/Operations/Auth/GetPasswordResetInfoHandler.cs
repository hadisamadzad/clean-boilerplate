using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class GetPasswordResetInfoHandler : IRequestHandler<GetPasswordResetInfoQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPasswordResetInfoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(GetPasswordResetInfoQuery request, CancellationToken cancellationToken)
    {
        var (succeeded, email) = PasswordResetTokenHelper.ReadPasswordResetToken(request.Token);

        if (!succeeded)
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.InvalidPasswordResetTokenError);

        var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
        if (user is null)
            throw new AggregateException($"Unable to read the valid password-reset token: {request.Token}");

        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.LockoutUserLoginError);

        if (!string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.InconsistentTokenError);

        return new OperationResult(OperationStatus.Completed, value: user.Email);
    }
}
