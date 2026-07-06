using ECommerce.Application.Dtos.Slides;
using ECommerce.Application.Features.Slides.Commands.CreateSlide;
using ECommerce.Application.Features.Slides.Commands.DeleteSlide;
using ECommerce.Application.Features.Slides.Commands.EditSlide;
using ECommerce.Application.Features.Slides.Commands.ToggleSlideStatus;
using ECommerce.Application.Features.Slides.Queries.GetAdminSlides;
using ECommerce.Application.Features.Slides.Queries.GetSlideById;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class SlidesController(ISender sender, ILogger<SlidesController> logger, IFileService fileService) : AdminBaseController
{
    [HttpGet]
    [HasPermission("slides.read")]
    public async Task<IActionResult> AdminGetAll([FromQuery] GetAdminSlidesQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{id:long}")]
    [HasPermission("slides.read")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var query = new GetSlideByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission("slides.create")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateSlideRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string? relativeUrl = null;
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishTitle.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/slides");
        }
        var command = new CreateSlideCommand(
            Title: request.Title,
            EnglishTitle: request.EnglishTitle,
            Description: request.Description,
            ImageUrl: relativeUrl!,
            Link: request.Link,
            DisplayOrder: request.DisplayOrder,
            IsActive: request.IsActive
        );

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{id:long}")]
    [HasPermission("slides.update")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit(long id, [FromForm] EditSlideRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string? relativeUrl = null;
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishTitle.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/slides");
        }
        var command = new EditSlideCommand(
            Id: id,
            Title: request.Title,
            EnglishTitle: request.EnglishTitle,
            Description: request.Description,
            ImageUrl: relativeUrl,
            Link: request.Link,
            DisplayOrder: request.DisplayOrder
        );
        var result = await sender.Send(command, cancellationToken);
        if (result.IsFailure && !string.IsNullOrEmpty(relativeUrl))
        {
            fileService.DeleteFile(relativeUrl);
        }
        return result.ToActionResult(logger);
    }

    [HttpDelete("{id:long}")]
    [HasPermission("slides.delete")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var command = new DeleteSlideCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{id:long}")]
    [HasPermission("slides.update")]
    public async Task<IActionResult> ToggleStatus(long id, [FromBody] bool isActive, CancellationToken cancellationToken)
    {
        var command = new ToggleSlideStatusCommand(id, isActive);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}