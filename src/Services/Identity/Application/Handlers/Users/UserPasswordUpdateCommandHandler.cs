using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Commands.Users;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Handlers.Users
{
    public class UserPasswordUpdateCommandHandler : IRequestHandler<UpdateUserPasswordCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserPasswordUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            // Get
            var user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
            if (user == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: UserErrors.UserNotFoundError);

            if (new PasswordHasher().Check(user.PasswordHash, request.CurrentPassword))
                user.PasswordHash = new PasswordHasher().Hash(request.NewPassword);
            else
                return new OperationResult(OperationResultStatus.Unprocessable, value: AuthErrors.InvalidCredentialsError);

            _unitOfWork.Users.Update(user);

            return new OperationResult(OperationResultStatus.Ok, value: user);
        }
    }
}
