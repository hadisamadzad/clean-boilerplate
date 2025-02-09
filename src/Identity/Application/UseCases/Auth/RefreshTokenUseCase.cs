using Common.Utilities.Operations;
using Identity.Application.Constants.Errors;
using Identity.Application.Helpers;
using Identity.Application.Interfaces;
using Identity.Application.Types.Models.Auth;
using MediatR;

namespace Identity.Application.UseCases.Auth;

// Handler
internal class RefreshTokenHandler(IRepositoryManager unitOfWork) : IRequestHandler<RefreshTokenQuery, OperationResult>
{
    public async Task<OperationResult> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        if (!JwtHelper.IsValidJwtRefreshToken(request.RefreshToken))
            return new OperationResult(OperationStatus.Unauthorized, Errors.InvalidCredentials);

        var email = JwtHelper.GetEmail(request.RefreshToken);

        var user = await unitOfWork.Users.GetUserByEmailAsync(email);
        if (user is null)
            return new OperationResult(OperationStatus.Unauthorized, Errors.InvalidId);

        // Lockout check
        if (user.IsLockedOutOrNotActive())
            return new OperationResult(OperationStatus.Unauthorized, Errors.LockedUser);

        var result = new TokenResult
        {
            AccessToken = user.CreateJwtAccessToken(),
        };

        return new OperationResult(OperationStatus.Completed, Value: result);
    }
}

// Model
public record RefreshTokenQuery(
    string RefreshToken) : IRequest<OperationResult>;