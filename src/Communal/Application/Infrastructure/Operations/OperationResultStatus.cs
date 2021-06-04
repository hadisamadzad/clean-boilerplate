namespace Communal.Application.Infrastructure.Operations
{
    public enum OperationResultStatus
    {
        Ok = 1,
        Created,
        InvalidRequest,
        NotFound,
        Unauthorized,
        Unprocessable
    }
}