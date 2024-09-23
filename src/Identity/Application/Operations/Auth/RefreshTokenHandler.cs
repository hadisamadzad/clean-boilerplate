using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Base.Auth;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class RefreshTokenHandler(IUnitOfWork unitOfWork) : IRequestHandler<RefreshTokenQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OperationResult> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        if (!JwtHelper.IsValidJwtRefreshToken(request.RefreshToken))
            return new OperationResult(OperationStatus.Unauthorized,
                value: AuthErrors.InvalidCredentialsError);

        var email = JwtHelper.GetEmail(request.RefreshToken);

        var user = await _unitOfWork.Users.GetUserByEmailAsync(email);
        if (user == null)
            return new OperationResult(OperationStatus.Unauthorized,
                value: UserErrors.UserNotFoundError);

        // Lockout check
        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unauthorized,
                value: AuthErrors.LockoutUserLoginError);

        var result = new TokenResult
        {
            AccessToken = user.CreateJwtAccessToken(),
        };

        return new OperationResult(OperationStatus.Completed, value: result);
    }
}
