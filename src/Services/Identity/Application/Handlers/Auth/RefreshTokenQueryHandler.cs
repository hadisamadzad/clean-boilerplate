using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Models.Auth;
using Identity.Application.Models.Queries.Auth;
using MediatR;

namespace Identity.Application.Handlers.Auth
{
    internal class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            // Destructuring
            var username = JwtHelper.GetUsername(request.RefreshToken);

            // Get
            var user = await _unitOfWork.Users.GetUserByUsernameAsync(username);
            if (user == null)
                return new OperationResult(OperationResultStatus.Unauthorized, value: UserErrors.UserNotFoundError);

            // Lockout check
            if (!user.CanLogin())
                return new OperationResult(OperationResultStatus.Unauthorized, value: AuthErrors.InvalidLoginError);

            var result = new TokenResponse
            {
                AccessToken = user.CreateJwtAccessToken(),
            };

            return new OperationResult(OperationResultStatus.Ok, value: result);
        }
    }
}