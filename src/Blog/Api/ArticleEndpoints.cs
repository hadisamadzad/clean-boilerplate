using Blog.Application.Types.Entities;
using Blog.Application.Types.Models.Articles;
using Blog.Application.UseCases.Articles;
using Common.Utilities.OperationResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api;

public static class ArticleEndpoints
{
    const string Route = "api/articles/";
    const string Tag = "Articles";

    // Models
    public record CreateArticleRequest(
        string Title,
        string? Subtitle,
        string? Summary,
        string? Content,
        string? Slug,
        string? ThumbnailUrl,
        string? CoverImageUrl,
        ICollection<string> TagIds);

    // Endpoints
    public static void MapArticleEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(Route).WithTags(Tag);

        // Endpoint for creating an article
        group.MapPost("", async (
            IMediator mediator,
            [FromHeader] string requestedBy,
            [FromBody] CreateArticleRequest request) =>
            {
                return await mediator.Send(new CreateArticleCommand
                {
                    AuthorId = requestedBy,
                    Title = request.Title,
                    Subtitle = request.Subtitle ?? string.Empty,
                    Summary = request.Summary ?? string.Empty,
                    Content = request.Content ?? string.Empty,
                    Slug = request.Slug ?? string.Empty,
                    ThumbnailUrl = request.ThumbnailUrl ?? string.Empty,
                    CoverImageUrl = request.CoverImageUrl ?? string.Empty,
                    TagIds = request.TagIds
                });
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                var value = (ArticleEntity)operation.Value;
                return new
                {
                    ArticleId = value.Id,
                };
            });

        // Endpoint for getting an article
        group.MapGet("{articleId}", async (
            IMediator mediator,
            [FromRoute] string articleId) =>
            {
                return await mediator.Send(new GetArticleByIdQuery(articleId));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                var value = (ArticleModel)operation.Value;
                return new
                {
                    ArticleId = value.Id,
                    AuthorId = value.AuthorId,
                    Title = value.Title,
                    Subtitle = value.Subtitle,
                    Summary = value.Summary,
                    Content = value.Content,
                    Slug = value.Slug,
                    ThumbnailUrl = value.ThumbnailUrl,
                    CoverImageUrl = value.CoverImageUrl,
                    TimeToReadInMinute = value.TimeToReadInMinute,
                    Likes = value.Likes,
                    TagIds = value.TagIds,
                    Status = value.Status,
                    CreatedAt = value.CreatedAt,
                    UpdatedAt = value.UpdatedAt,
                    PublishedAt = value.PublishedAt,
                    ArchivedAt = value.ArchivedAt
                };
            });
    }
}