using Blog.Application.Types.Entities;
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
        string? CoverImageUrl);

    // Endpoints
    public static void MapArticleEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(Route).WithTags(Tag);

        // Endpoint for creating a article
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
    }
}