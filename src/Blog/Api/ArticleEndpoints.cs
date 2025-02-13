using Blog.Application.Types.Entities;
using Blog.Application.Types.Models.Articles;
using Blog.Application.UseCases.Articles;
using Common.Utilities.OperationResult;
using Common.Utilities.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api;

public static class ArticleEndpoints
{
    const string Route = "api/articles/";
    const string Tag = "Articles";

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

        // Endpoint for getting a list of articles
        group.MapGet("", async (
            IMediator mediator,
            [FromBody] GetArticlesByFilterRequest request) =>
            {
                return await mediator.Send(new GetArticlesByFilterQuery(new()
                {
                    Keyword = request.Keyword,
                    TagIds = request.TagIds ?? [],
                    Statuses = request.Statuses ?? [],
                    SortBy = request.SortBy ?? ArticleSortBy.CreatedAtNewest,

                    Page = request.Page,
                    PageSize = request.PageSize
                }));
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var operation = await next(context) as OperationResult;
                if (!operation!.Succeeded)
                    return operation.GetHttpResult();

                var value = (PaginatedList<ArticleModel>)operation.Value;
                return new
                {
                    Page = value.Page,
                    PageSize = value.PageSize,
                    TotalCount = value.TotalCount,
                    Results = value.Results.Select(x => new
                    {
                        ArticleId = x.Id,
                        AuthorId = x.AuthorId,
                        Title = x.Title,
                        Subtitle = x.Subtitle,
                        Summary = x.Summary,
                        Content = x.Content,
                        Slug = x.Slug,
                        ThumbnailUrl = x.ThumbnailUrl,
                        CoverImageUrl = x.CoverImageUrl,
                        TimeToReadInMinute = x.TimeToReadInMinute,
                        Likes = x.Likes,
                        TagIds = x.TagIds,
                        Status = x.Status,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                        PublishedAt = x.PublishedAt,
                        ArchivedAt = x.ArchivedAt
                    })
                };
            });
    }
}

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

public class GetArticlesByFilterRequest
{
    public string? Keyword { get; set; }
    public List<string>? TagIds { get; set; }
    public List<ArticleState>? Statuses { get; set; }
    public ArticleSortBy? SortBy { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }
}