using Blog.Application.Constants.Errors;
using Blog.Application.Interfaces;
using Blog.Application.Types.Models.Articles;
using Common.Utilities.OperationResult;
using MediatR;

namespace Blog.Application.UseCases.Articles;

// Handler
internal class DeleteArticleHandler(IRepositoryManager unitOfWork) :
    IRequestHandler<DeleteArticleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteArticleCommand request, CancellationToken cancel)
    {
        var entity = await unitOfWork.Articles.GetArticleByIdAsync(request.ArticleId);
        if (entity is null)
            return OperationResult.Failure(OperationStatus.Unprocessable, Errors.ArticleNotFound);

        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;

        _ = await unitOfWork.Articles.UpdateAsync(entity);

        return OperationResult.Success(entity.MapToModel());
    }
}

// Model
public record DeleteArticleCommand(string ArticleId) : IRequest<OperationResult>;