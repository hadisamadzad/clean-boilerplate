using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace Identity.Application.Commands.Users
{
    public class UpdateUserPasswordCommand : Request, IRequest<OperationResult>
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
