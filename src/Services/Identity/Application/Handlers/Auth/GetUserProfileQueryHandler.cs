using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Identity.Application.Interfaces;
using Identity.Application.Mappers;
using Identity.Application.Models.Responses;
using Communal.Application.Infrastructure.Operations;
using Identity.Application.Models.Queries.Auth;
using Identity.Application.Constants;

namespace Identity.Application.Handlers.Auth
{
    internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserProfileQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            // Get
            var user = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
            if (user == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: UserErrors.UserNotFoundError);

            // Mapping
            var response = user.MapToUserResponse();

            return new OperationResult(OperationResultStatus.Ok, value: response);
        }
    }
}