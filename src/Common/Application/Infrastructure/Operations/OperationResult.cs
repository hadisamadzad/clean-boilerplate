namespace Common.Application.Infrastructure.Operations;

public class OperationResult(OperationStatus status, object value)
{
    public readonly OperationStatus Status = status;
    public readonly object Value = value;

    public bool Succeeded => IsSucceeded(Status);

    private static bool IsSucceeded(OperationStatus status) => status switch
    {
        _ when
            status == OperationStatus.Completed ||
            status == OperationStatus.Ignored => true,
        _ when
            status == OperationStatus.ValidationFailed ||
            status == OperationStatus.NotFound ||
            status == OperationStatus.Unauthorized ||
            status == OperationStatus.Unprocessable => false,
        _ => false
    };
}

public enum OperationStatus
{
    Completed = 1,
    Ignored,
    ValidationFailed,
    NotFound,
    Unauthorized,
    Unprocessable
}