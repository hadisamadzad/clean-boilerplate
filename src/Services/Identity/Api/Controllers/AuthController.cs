using System.Threading.Tasks;
using Communal.Api.Extensions.AspNetCore;
using Identity.Api.Filters.Auth;
using Identity.Api.Models.Requests.Auth;
using Identity.Api.ResultFilters.Auth;
using Identity.Application.Models.Commands.Auth;
using Identity.Application.Models.Queries.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Routes.Auth + "login")]
        [LoginResultFilter]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var operation = await _mediator.Send(new LoginCommand
            {
                Username = request.Username,
                Password = request.Password,
            });

            return this.ReturnResponse(operation);
        }

        [HttpGet(Routes.Auth + "token")]
        [TokenResultFilter]
        public async Task<IActionResult> GetAccessToken([FromHeader] string refresh)
        {
            var operation = await _mediator.Send(new RefreshTokenQuery
            {
                RefreshToken = refresh
            });

            return this.ReturnResponse(operation);
        }

        [HttpGet(Routes.Auth + "profile")]
        [GetProfileResultFilter]
        public async Task<IActionResult> Profile()
        {
            // Operation
            var operation = await _mediator.Send(new GetUserProfileQuery(Request.GetRequestInfo()));

            return this.ReturnResponse(operation);
        }
    }
}