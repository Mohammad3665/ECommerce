using ECommerce.Application.Dtos.Articles;
using ECommerce.Application.Features.Articles.Commands.ChangeArticleStatus;
using ECommerce.Application.Features.Articles.Commands.CreateArticle;
using ECommerce.Application.Features.Articles.Commands.DeleteArticle;
using ECommerce.Application.Features.Articles.Commands.EditArticle;
using ECommerce.Application.Features.Articles.Queries.AdminGetArticleBySlug;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class ArticlesController(ISender sender, ILogger<ArticlesController> logger, IFileService fileService) : AdminBaseController
{
    [HttpGet("{slug}")]
    [HasPermission("articles.read")]
    public async Task<IActionResult> AdminGet(string slug, CancellationToken cancellationToken)
    {
        var query = new AdminGetArticleBySlugQuery(slug);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission("articles.create")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateArticleRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string relativeUrl = null;
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishTitle.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/articles");
        }

        var command = new CreateArticleCommand(
            Title: request.Title,
            EnglishTitle: request.EnglishTitle,
            Content: request.Content,
            Summary: request.Summary,
            Status: request.Status,
            ArticleCategoryId: request.ArticleCategoryId,
            AuthorId: Guid.Empty,
            ImageUrl: relativeUrl
        );

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("articles.update")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit(string slug, [FromForm] EditArticleRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string? relativeUrl = null;
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishTitle.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/articles");
        }

        var command = new EditArticleCommand(
            Slug: slug,
            Title: request.Title,
            EnglishTitle: request.EnglishTitle,
            Content: request.Content,
            Summary: request.Summary,
            ArticleCategoryId: request.ArticleCategoryId,
            ImageUrl: relativeUrl
        );

        var result = await sender.Send(command, cancellationToken);
        if (result.IsFailure && !string.IsNullOrEmpty(relativeUrl))
        {
            fileService.DeleteFile(relativeUrl);
        }
        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission("articles.delete")]
    public async Task<IActionResult> Delete(string slug, CancellationToken cancellationToken)
    {
        var command = new DeleteArticleCommand(slug);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("articles.update")]
    public async Task<IActionResult> ChangeStatus(string slug, [FromBody] string status, CancellationToken cancellationToken)
    {
        var command = new ChangeArticleStatusCommand(slug, status);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}