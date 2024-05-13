using Communal.Application.Infrastructure.Operations;
using Communal.Application.Infrastructure.Pagination;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Users;
using MediatR;

namespace Identity.Application.Operations.Users;

public class GetUsersByFilterHandler : IRequestHandler<GetUsersByFilterQuery, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersByFilterHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
    {
        // Get
        //request.Filter.Include = new UserIncludes();

        var entities = await _unitOfWork.Users.GetUsersByFilterAsync(request.Filter);
        var count = await _unitOfWork.Users.CountUsersByFilterAsync(request.Filter);

        // Mapper
        var response = entities.MapToUserModels();

        var result = new PaginatedList<UserModel>
        {
            Page = request.Filter.Page,
            PageSize = request.Filter.PageSize,
            Data = response.ToList(),
            TotalCount = count
        };

        return new OperationResult(OperationStatus.Completed, value: result);
    }
}
