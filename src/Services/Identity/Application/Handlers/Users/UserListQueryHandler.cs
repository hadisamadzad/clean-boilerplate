using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Communal.Application.Infrastructure.Operations;
using Communal.Application.Infrastructure.Pagination;
using Identity.Application.Interfaces;
using Identity.Application.Mappers;
using Identity.Application.Models.Responses.Users;
using Identity.Application.Queries.Users;
using MediatR;

namespace Identity.Application.Handlers.Users
{
    public class UserListQueryHandler : IRequestHandler<GetUsersQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            // Get
            request.Filter.Include = new UserIncludes { Role = true };

            var entities = await _unitOfWork.Users.GetUsersByFilterAsync(request.Filter);
            var count = await _unitOfWork.Users.CountUsersByFilterAsync(request.Filter);

            // Mapper
            var response = entities.MapToUserResponses();

            var result = new PaginatedList<UserResponse>
            {
                Page = request.Filter.Page,
                PageSize = request.Filter.PageSize,
                Data = response.ToList(),
                TotalCount = count
            };

            return new OperationResult(OperationResultStatus.Ok, value: result);
        }
    }
}