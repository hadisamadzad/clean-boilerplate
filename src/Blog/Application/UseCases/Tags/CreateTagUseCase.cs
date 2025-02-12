using Blog.Application.Constants.Errors;
using Blog.Application.Helpers;
using Blog.Application.Interfaces;
using Blog.Application.Types.Entities;
using Common.Helpers;
using Common.Utilities.OperationResult;
using FluentValidation;
using MediatR;

namespace Blog.Application.UseCases.Tags;

// Handler
internal class CreateTagHandler(IRepositoryManager unitOfWork) :
    IRequestHandler<CreateTagCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateTagCommand request, CancellationToken cancel)
    {
        // Validation
        var validation = new CreateTagValidator().Validate(request);
        if (!validation.IsValid)
            return OperationResult.Failure(OperationStatus.Invalid, validation.GetFirstError());

        var slug = string.IsNullOrWhiteSpace(request.Slug) ?
            SlugHelper.GenerateSlug(request.Name) : request.Slug;

        var entity = new TagEntity
        {
            Id = UidHelper.GenerateNewId("tag"),
            Name = request.Name.ToLower(),
            Slug = slug,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await unitOfWork.Tags.InsertAsync(entity);

        return OperationResult.Success(entity);
    }
}

// Model
public record CreateTagCommand(string Name, string Slug) : IRequest<OperationResult>;

// Model Validator
public class CreateTagValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagValidator()
    {
        // Name
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(30)
            .WithState(_ => Errors.InvalidTagName);

        // Slug
        RuleFor(x => x.Slug)
            .MaximumLength(30)
            .When(x => !string.IsNullOrEmpty(x.Slug))
            .WithState(_ => Errors.InvalidTagSlug);
    }
}