using Common.Application.Helpers;
using Common.Application.Infrastructure.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Base.Auth;
using MediatR;

namespace Identity.Application.Operations.Auth;

internal class LoginHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginCommand, OperationResult>
{
    public async Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new LoginValidator().Validate(request);
        if (!validation.IsValid)
            return new OperationResult(OperationStatus.Invalid, validation.GetFirstError());

        // Get
        var user = await unitOfWork.Users.GetUserByEmailAsync(request.Email);
        if (user is null)
            return new OperationResult(OperationStatus.Unprocessable, Errors.InvalidId);

        // Lockout check
        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unprocessable, Errors.InvalidCredentials);

        // Login check via password
        var loggedIn = PasswordHelper.CheckPasswordHash(user.PasswordHash, request.Password);

        // Lockout history
        if (!loggedIn)
        {
            user.TryToLockout();
            _ = await unitOfWork.Users.UpdateAsync(user);
            await unitOfWork.CommitAsync();
            return new OperationResult(OperationStatus.Unprocessable, Errors.InvalidCredentials);
        }

        /* Here user is authenticated */
        user.LastLoginDate = DateTime.UtcNow;
        user.ResetLockoutHistory();
        _ = await unitOfWork.Users.UpdateAsync(user);

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