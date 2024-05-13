using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Operations.Auth.Models;
using Identity.Application.Types.Models.Base.Auth;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class RefreshTokenHandler : IRequestHandler<RefreshTokenQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

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
