using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Models.Commands.Users;
using Identity.Application.Specifications.Users;
using MediatR;

namespace Identity.Application.Handlers.Users
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Checking same username
            var isExist = await _unitOfWork.Users
                .ExistsAsync(new DuplicateUserSpecification(request.Username).ToExpression());
            if (isExist)
                return new OperationResult(OperationResultStatus.Unprocessable, value: UserErrors.DuplicateUsernameError);

            // Factory
            var entity = UserHelper.CreateUser(request);

            _unitOfWork.Users.Add(entity);

            return new OperationResult(OperationResultStatus.Created, value: entity);
        }
    }
}