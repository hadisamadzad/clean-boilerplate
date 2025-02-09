using FluentValidation;
using Identity.Application.Constants.Errors;

namespace Identity.Application.UseCases.Users;

public class UpdateUserStateValidator : AbstractValidator<UpdateUserStateCommand>
{
    public UpdateUserStateValidator()
    {
        // User id
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithState(_ => Errors.InvalidId);

        // Email
        RuleFor(x => x.State)
            .IsInEnum()
            .WithState(_ => Errors.InvalidEmail);
    }
}
