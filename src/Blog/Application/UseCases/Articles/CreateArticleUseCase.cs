using Blog.Application.Constants.Errors;
using Blog.Application.Interfaces;
using Blog.Application.Types.Entities;
using Common.Helpers;
using Common.Utilities.OperationResult;
using FluentValidation;
using MediatR;

namespace Blog.Application.UseCases.Articles;

// Handler
internal class CreateArticleHandler(IRepositoryManager unitOfWork) : IRequestHandler<CreateArticleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        // Validation
        var validation = new CreateArticleValidator().Validate(request);
        if (!validation.IsValid)
            return OperationResult.Failure(OperationStatus.Invalid, validation.GetFirstError());

        // Factory
        var entity = new ArticleEntity
        {
            Id = UidHelper.GenerateNewId("article"),
            Title = request.Title,
            State = ArticleState.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await unitOfWork.Articles.InsertAsync(entity);

        return OperationResult.Success(entity);
    }
}

// Model
public record CreateArticleCommand(
    string Title) : IRequest<OperationResult>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}

// Model Validator
public class CreateArticleValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleValidator()
    {
        // Id
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .WithState(_ => Errors.InvalidId);
    }
}