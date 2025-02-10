using Common.Utilities.OperationResult;
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
            return OperationResult.Failure(OperationStatus.Unauthorized, Errors.InvalidCredentials);

        var email = JwtHelper.GetEmail(request.RefreshToken);

        var user = await unitOfWork.Users.GetUserByEmailAsync(email);
        if (user is null)
            return OperationResult.Failure(OperationStatus.Unauthorized, Errors.InvalidId);

        // Lockout check
        if (user.IsLockedOutOrNotActive())
            return OperationResult.Failure(OperationStatus.Unauthorized, Errors.LockedUser);

        var result = new TokenResult
        {
            AccessToken = user.CreateJwtAccessToken(),
        };

        return OperationResult.Success(result);
    }
}

// Model
public record RefreshTokenQuery(
    string RefreshToken) : IRequest<OperationResult>;