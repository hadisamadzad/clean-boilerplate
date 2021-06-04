using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace Identity.Application.Models.Queries.Auth
{
    public class RefreshTokenQuery : Request, IRequest<OperationResult>
    {
        public string RefreshToken { get; set; }
    }
}