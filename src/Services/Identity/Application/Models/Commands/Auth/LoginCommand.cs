using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace Identity.Application.Models.Commands.Auth
{
    public class LoginCommand : Request, IRequest<OperationResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
