using Common.Application.Infrastructure.Errors;
using Microsoft.AspNetCore.Http;

namespace Common.Application.Infrastructure.Operations;

public static class OperationResultExtension
{
    public static IResult GetHttpResult(this OperationResult operation)
    {
        object response = operation.Value;
        if (response is ErrorModel errorModel)
            response = new ErrorResponse(errorModel);

        return operation.Status switch
        {
            OperationStatus.Completed => Results.Ok(response),
            OperationStatus.Ignored => Results.Ok(response),
            OperationStatus.ValidationFailed => Results.BadRequest(response),
            OperationStatus.NotFound => Results.NotFound(response),
            OperationStatus.Unauthorized => Results.UnprocessableEntity(response),
            OperationStatus.Unprocessable => Results.UnprocessableEntity(response),
            _ => Results.UnprocessableEntity(response)
        };
    }
}