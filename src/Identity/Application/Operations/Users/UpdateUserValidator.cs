using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.Operations.Users;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        // User id
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithState(_ => Errors.InvalidInputValidationError);

        // First name
        RuleFor(x => x.FirstName)
            .Length(2, Types.Entities.Constants.Char80Length)
            .When(x => !string.IsNullOrEmpty(x.FirstName))
            .WithState(_ => Errors.InvalidFirstNameError);

        // Last name
        RuleFor(x => x.LastName)
            .Length(2, Types.Entities.Constants.Char80Length)
            .When(x => !string.IsNullOrEmpty(x.LastName))
            .WithState(_ => Errors.InvalidLastNameError);
    }
}
