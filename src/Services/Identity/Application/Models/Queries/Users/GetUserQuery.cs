using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace Identity.Application.Models.Queries.Users
{
    public class GetUserQuery : Request, IRequest<OperationResult>
    {
        public int UserId { get; set; }
    }
}