using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace Identity.Application.Models.Commands.Users
{
    public class CreateUserCommand : Request, IRequest<OperationResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
