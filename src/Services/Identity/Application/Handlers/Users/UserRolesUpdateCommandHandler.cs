using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Commands.Users;
using Identity.Application.Constants;
using Identity.Application.Interfaces;
using Identity.Domain.Users;
using MediatR;

namespace Identity.Application.Handlers.Users
{
    internal class UserRolesUpdateCommandHandler : IRequestHandler<UpdateUserRolesCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRolesUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            // Get
            var user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);

            // Validation
            if (user == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: UserErrors.UserNotFoundError);

            // Simple security
            if (user.Roles.Count == 0)
                return new OperationResult(OperationResultStatus.Unprocessable, value: AuthErrors.UnauthorizedRequestError);

            // Update
            if (request.Roles != null)
                user.Roles = request.Roles.Select(x => new UserRole
                {
                    UserId = request.UserId,
                    Role = x
                }).ToList();

            _unitOfWork.Users.Update(user);

            return new OperationResult(OperationResultStatus.Ok, value: user);
        }
    }
}
