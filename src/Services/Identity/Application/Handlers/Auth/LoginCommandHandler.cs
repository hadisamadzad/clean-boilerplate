using System;
using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Models.Auth;
using Identity.Application.Models.Commands.Auth;
using Identity.Domain.Users;
using MediatR;

namespace Identity.Application.Handlers.Auth
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Get
            var user = await _unitOfWork.Users.GetUserByUsernameAsync(request.Username);
            if (user == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: UserErrors.UserNotFoundError);

            // Lockout check
            if (!user.CanLogin())
                return new OperationResult(OperationResultStatus.Unprocessable, value: AuthErrors.InvalidLoginError);

            // Login check via password
            var isLogin = new PasswordHasher().Check(user.PasswordHash, request.Password);

            // Lockout history
            if (!isLogin)
            {
                user.TryToLockout();
                _unitOfWork.Users.Update(user);
                await _unitOfWork.CommitAsync();
                return new OperationResult(OperationResultStatus.Unprocessable, value: AuthErrors.InvalidLoginError);
            }

            /* Here user is authenticated */
            // Other updates
            if (user.State == UserState.InActive)
                user.Activate();

            user.LastLoginDate = DateTime.UtcNow;
            user.ResetLockoutHistory();
            _unitOfWork.Users.Update(user);

            var result = new LoginResponse
            {
                Username = user.Username,
                Fullname = user.GetFullName(),
                AccessToken = user.CreateJwtAccessToken(),
                RefreshToken = user.CreateJwtRefreshToken()
            };

            return new OperationResult(OperationResultStatus.Ok, value: result);
        }
    }
}