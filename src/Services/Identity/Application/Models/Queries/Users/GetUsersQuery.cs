using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using Identity.Application.Models.Users;
using MediatR;

namespace Identity.Application.Queries.Users
{
    public class GetUsersQuery : Request, IRequest<OperationResult>
    {
        public UserFilter Filter { get; set; }
    }
}