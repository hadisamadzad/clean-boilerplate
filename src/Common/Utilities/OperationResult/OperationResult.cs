namespace Common.Utilities.OperationResult;

public record OperationResult(OperationStatus Status, object Value)
{
    public bool Succeeded => Status is OperationStatus.Completed or OperationStatus.Ignored;

    public static OperationResult Success(object value) => new(OperationStatus.Completed, value);
    public static OperationResult Failure(OperationStatus status, object value) => new(status, value);
}

public enum OperationStatus
{
    Completed = 1,
    Ignored,
    Invalid,
    Unauthorized,
    Unprocessable
}