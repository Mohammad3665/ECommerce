using ECommerce.AdminPanel.Models;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Features.Slides.Commands.CreateSlide;
using ECommerce.Application.Features.Slides.Commands.EditSlide;
using ECommerce.Application.Features.Slides.Commands.DeleteSlide;
using ECommerce.Application.Features.Slides.Commands.ToggleSlideStatus;
using ECommerce.Application.Features.Slides.Queries.GetSlideById;
using ECommerce.Application.Features.Slides.Queries.GetAdminSlides;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class SlidesController(ISender sender, IFileService fileService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "مدیریت اسلایدرها";
        var query = new GetAdminSlidesQuery { PageNumber = 1, PageSize = 50 };
        var result = await sender.Send(query, ct);
        var slides = result.IsSuccess ? result.Data.Items.Adapt<List<SlideListViewModel>>() : [];
        return View(slides);
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن اسلایدر";
        return View(new SlideFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SlideFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن اسلایدر";
            return View(model);
        }

        var imageUrl = model.ImageFile is not null
            ? await fileService.SaveFileAsync(model.ImageFile, model.EnglishTitle, "uploads/slides")
            : string.Empty;

        var command = new CreateSlideCommand(
            model.Title, model.EnglishTitle, model.Description,
            imageUrl, model.Link, model.DisplayOrder, model.IsActive);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            if (!string.IsNullOrEmpty(imageUrl)) fileService.DeleteFile(imageUrl);
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "افزودن اسلایدر";
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(long id, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش اسلایدر";
        var result = await sender.Send(new GetSlideByIdQuery(id), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data;
        var model = new SlideFormViewModel
        {
            Id = dto!.Id,
            Title = dto.Title,
            EnglishTitle = dto.EnglishTitle,
            Description = dto.Description,
            Link = dto.Link,
            DisplayOrder = dto.DisplayOrder,
            ExistingImageUrl = dto.ImageUrl
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SlideFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش اسلایدر";
            return View(model);
        }

        var imageUrl = model.ExistingImageUrl;
        if (model.ImageFile is not null)
        {
            imageUrl = await fileService.SaveFileAsync(model.ImageFile, model.EnglishTitle, "uploads/slides");
        }

        var command = new EditSlideCommand(
            model.Id, model.Title, model.EnglishTitle, model.Description,
            imageUrl, model.Link, model.DisplayOrder, model.IsActive);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش اسلایدر";
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        var command = new DeleteSlideCommand(id);
        await sender.Send(command, ct);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(long id, bool isActive, CancellationToken ct)
    {
        var command = new ToggleSlideStatusCommand(id, isActive);
        await sender.Send(command, ct);
        return RedirectToAction(nameof(Index));
    }
}
