using System.Collections.Generic;

namespace Communal.Application.Infrastructure.Operations
{
    public class OperationResult
    {
        public readonly OperationResultStatus Status;
        public readonly bool IsPersistable;
        public readonly object Value;
        public readonly Dictionary<string, string> OperationValues;

        public OperationResult(OperationResultStatus status, object value,
            bool isPersistable = false, Dictionary<string, string> operationValues = null)
        {
            Status = status;
            Value = value;
            IsPersistable = isPersistable;
            OperationValues = operationValues;
        }

        public OperationResult(OperationResult operation, bool succeeded)
        {
            Status = succeeded ? OperationResultStatus.Ok : OperationResultStatus.Unprocessable;
            IsPersistable = operation.IsPersistable;
            Value = operation.Value;
            OperationValues = operation.OperationValues;
        }

        public bool Succeeded => IsSucceeded(Status);

        private bool IsSucceeded(OperationResultStatus status) => status switch
        {
            _ when
                status == OperationResultStatus.Ok ||
                status == OperationResultStatus.Created => true,
            _ when
                status == OperationResultStatus.InvalidRequest ||
                status == OperationResultStatus.NotFound ||
                status == OperationResultStatus.Unauthorized ||
                status == OperationResultStatus.Unprocessable => false,
            _ => false
        };
    }
}