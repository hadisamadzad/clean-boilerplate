using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Commands.Users;
using Identity.Application.Constants;
using Identity.Application.Infrastructure.PartialUpdates;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Handlers.Users
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Get
            var user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
            if (user == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: UserErrors.UserNotFoundError);

            // Update
            var payload = request.GetUpdatedProperties();
            user = user.UpdatePartially(payload);

            _unitOfWork.Users.Update(user);

            return new OperationResult(OperationResultStatus.Ok, value: user);
        }
    }
}
