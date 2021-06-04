using System.Linq;
using FluentValidation.Results;

namespace Identity.Application.Helpers
{
    public static class ValidationHelper
    {
        public static string GetFirstErrorMessage(this ValidationResult result)
        {
            return result.Errors.FirstOrDefault()?.ErrorMessage;
        }
    }
}
