using Communal.Application.Helpers;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Entities.Users;
using Identity.Application.Types.Models.Base.Auth;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class LoginHandler : IRequestHandler<LoginCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new LoginValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.ValidationFailed, validation.GetFirstError());

        // Get
        var user = await _unitOfWork.Users.GetUserByEmailAsync(request.Email);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable,
                value: UserErrors.UserNotFoundError);

        // Lockout check
        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.NotActivatedUserLoginError);

        // Login check via password
        var loggedIn = PasswordHelper.CheckPasswordHash(user.PasswordHash, request.Password);

        // Lockout history
        if (!loggedIn)
        {
            user.TryToLockout();
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CommitAsync();
            return new OperationResult(OperationStatus.Unprocessable,
                value: AuthErrors.InvalidLoginError);
        }

        /* Here user is authenticated */
        // Other updates
        if (user.State == UserState.InActive)
            user.Activate();

        user.LastLoginDate = DateTime.UtcNow;
        user.ResetLockoutHistory();
        _unitOfWork.Users.Update(user);

        _ = await _unitOfWork.CommitAsync();

        var result = new LoginResult
        {
            Email = user.Email,
            FullName = user.GetFullName(),
            AccessToken = user.CreateJwtAccessToken(),
            RefreshToken = user.CreateJwtRefreshToken()
        };

        return new OperationResult(OperationStatus.Completed, value: result);
    }
}