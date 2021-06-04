using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace Identity.Application.Models.Queries.Auth
{
    public class GetUserProfileQuery : Request, IRequest<OperationResult>
    {
        public GetUserProfileQuery(RequestInfo info) : base(info)
        {
        }
    }
}