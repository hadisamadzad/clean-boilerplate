namespace Common.Utilities.Operations;

public record OperationResult(OperationStatus Status, object Value)
{
    public readonly OperationStatus Status = Status;
    public readonly object Value = Value;

    public bool Succeeded => IsSucceeded(Status);

    private static bool IsSucceeded(OperationStatus status) => status switch
    {
        _ when
            status == OperationStatus.Completed ||
            status == OperationStatus.Ignored => true,
        _ when
            status == OperationStatus.Invalid ||
            status == OperationStatus.Unauthorized ||
            status == OperationStatus.Unprocessable => false,
        _ => false
    };
}

public enum OperationStatus
{
    Completed = 1,
    Invalid,
    Ignored,
    Unauthorized,
    Unprocessable
}