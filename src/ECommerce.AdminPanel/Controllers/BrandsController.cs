using ECommerce.AdminPanel.Models;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Features.Brands.Commands.CreateBrand;
using ECommerce.Application.Features.Brands.Commands.EditBrand;
using ECommerce.Application.Features.Brands.Commands.DeleteBrand;
using ECommerce.Application.Features.Brands.Queries.GetAllBrands;
using ECommerce.Application.Features.Brands.Queries.GetBrandBySlug;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class BrandsController(ISender sender, IFileService fileService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "مدیریت برندها";
        var result = await sender.Send(new GetAllBrandsQuery(), ct);
        var brands = result.IsSuccess ? result.Data.Adapt<List<BrandListViewModel>>() : [];
        return View(brands);
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن برند";
        return View(new BrandFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن برند";
            return View(model);
        }

        var imageUrl = model.LogoFile is not null
            ? await fileService.SaveFileAsync(model.LogoFile, model.EnglishName, "uploads/brands")
            : string.Empty;

        var command = new CreateBrandCommand(
            model.Name, model.EnglishName, model.Description,
            imageUrl, model.IsActive);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            if (!string.IsNullOrEmpty(imageUrl)) fileService.DeleteFile(imageUrl);
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "افزودن برند";
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string slug, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش برند";
        var result = await sender.Send(new GetBrandBySlugQuery(slug), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data;
        var model = new BrandFormViewModel
        {
            Id = dto!.Id,
            Name = dto.Name,
            EnglishName = dto.EnglishName,
            Slug = dto.Slug,
            Description = dto.Description,
            ExistingLogoUrl = dto.LogoImageUrl
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BrandFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش برند";
            return View(model);
        }

        var imageUrl = model.ExistingLogoUrl ?? string.Empty;
        if (model.LogoFile is not null)
        {
            imageUrl = await fileService.SaveFileAsync(model.LogoFile, model.EnglishName, "uploads/brands");
        }

        var command = new EditBrandCommand(
            model.Slug, model.Name, model.EnglishName, model.Description, imageUrl, model.IsActive);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش برند";
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string slug, CancellationToken ct)
    {
        var command = new DeleteBrandCommand(slug);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در حذف برند";

        return RedirectToAction(nameof(Index));
    }
}
