using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using Identity.Application.Infrastructure.PartialUpdates;
using Identity.Domain.Users;
using MediatR;

namespace Identity.Application.Commands.Users
{
    public class UpdateUserCommand : Request, IRequest<OperationResult>, IPartialUpdate
    {
        [Update(UpdateType.DontUpdate)]
        public int UserId { get; set; }

        public string Email { get; set; }
        public UserState State { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
