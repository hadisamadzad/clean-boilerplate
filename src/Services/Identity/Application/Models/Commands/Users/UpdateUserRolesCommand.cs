using System.Collections.Generic;
using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using Identity.Domain.Roles;
using MediatR;

namespace Identity.Application.Commands.Users
{
    public class UpdateUserRolesCommand : Request, IRequest<OperationResult>
    {
        public int UserId { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
