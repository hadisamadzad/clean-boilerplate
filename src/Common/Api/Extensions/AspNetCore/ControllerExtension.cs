using Common.Application.Infrastructure.Errors;
using Common.Application.Infrastructure.Operations;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Extensions.AspNetCore;

public static class ControllerExtension
{
    public static IActionResult ReturnResponse(this ControllerBase controller, OperationResult operation)
    {
        object response = operation.Value;

        return operation.Status switch
        {
            OperationStatus.Completed => controller.Ok(response),
            OperationStatus.Ignored => controller.Ok(response),
            OperationStatus.Invalid => controller.BadRequest(response),
            OperationStatus.Unauthorized => controller.UnprocessableEntity(response),
            OperationStatus.Unprocessable => controller.UnprocessableEntity(response),
            _ => controller.UnprocessableEntity(response)
        };
    }
}
