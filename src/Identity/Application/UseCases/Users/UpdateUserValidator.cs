using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.UseCases.Users;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        // User id
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithState(_ => Errors.InvalidId);

        // First name
        RuleFor(x => x.FirstName)
            .Length(2, 80)
            .When(x => !string.IsNullOrEmpty(x.FirstName))
            .WithState(_ => Errors.InvalidFirstName);

        // Last name
        RuleFor(x => x.LastName)
            .Length(2, 80)
            .When(x => !string.IsNullOrEmpty(x.LastName))
            .WithState(_ => Errors.InvalidLastName);
    }
}
