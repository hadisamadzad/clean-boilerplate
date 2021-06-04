using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Constants;
using Identity.Application.Interfaces;
using Identity.Application.Mappers;
using Identity.Application.Models.Queries.Users;
using MediatR;

namespace Identity.Application.Handlers.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            // Get
            var entity = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
            if (entity == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: UserErrors.UserNotFoundError);

            // Mapping
            var response = entity.MapToUserResponse();

            return new OperationResult(OperationResultStatus.Ok, value: response);
        }
    }
}