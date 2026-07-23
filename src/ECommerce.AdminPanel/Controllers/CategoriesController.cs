using ECommerce.AdminPanel.Models;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Features.Categories.Commands.CreateCategory;
using ECommerce.Application.Features.Categories.Commands.EditCategory;
using ECommerce.Application.Features.Categories.Commands.DeleteCategory;
using ECommerce.Application.Features.Categories.Queries.GetAllCategories;
using ECommerce.Application.Features.Categories.Queries.GetCategoryBySlug;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class CategoriesController(ISender sender, IFileService fileService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "مدیریت دسته‌بندی‌ها";
        var result = await sender.Send(new GetAllCategoriesQuery(), ct);
        var categories = result.IsSuccess ? result.Data.Adapt<List<CategoryListViewModel>>() : [];
        return View(categories);
    }

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        ViewData["Title"] = "افزودن دسته‌بندی";
        await LoadParentCategories(ct);
        return View(new CategoryFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن دسته‌بندی";
            await LoadParentCategories(ct);
            return View(model);
        }

        var imageUrl = model.ImageFile is not null
            ? await fileService.SaveFileAsync(model.ImageFile, model.EnglishName, "uploads/categories")
            : null;

        var command = new CreateCategoryCommand(
            model.Name, model.EnglishName, model.Description,
            imageUrl, model.ParentCategoryId);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            if (!string.IsNullOrEmpty(imageUrl)) fileService.DeleteFile(imageUrl);
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "افزودن دسته‌بندی";
            await LoadParentCategories(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string slug, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش دسته‌بندی";
        var result = await sender.Send(new GetCategoryBySlugQuery(slug), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data!;
        var model = new CategoryFormViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            EnglishName = dto.EnglishName,
            Slug = dto.Slug,
            Description = dto.Description,
            ExistingImageUrl = dto.ImageUrl,
            ParentCategoryId = dto.ParentCategoryId,
            IsActive = dto.IsActive
        };

        // Load parent categories excluding current one
        var allCategoriesResult = await sender.Send(new GetAllCategoriesQuery(), ct);
        var parentCategories = allCategoriesResult.IsSuccess
            ? allCategoriesResult.Data.Where(c => c.Id != dto.Id).Select(c => new ParentCategoryDto(c.Id, c.Name)).ToList()
            : [];
        ViewBag.ParentCategories = parentCategories;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش دسته‌بندی";
            await LoadParentCategoriesForEdit(ct, model.Id);
            return View(model);
        }

        var imageUrl = model.ExistingImageUrl;
        if (model.ImageFile is not null)
        {
            imageUrl = await fileService.SaveFileAsync(model.ImageFile, model.EnglishName, "uploads/categories");
        }

        var command = new EditCategoryCommand(
            model.Slug, model.Name, model.EnglishName, model.Description, imageUrl,
            model.ParentCategoryId, model.IsActive);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش دسته‌بندی";
            await LoadParentCategoriesForEdit(ct, model.Id);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string slug, CancellationToken ct)
    {
        var command = new DeleteCategoryCommand(slug);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در حذف دسته‌بندی";

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadParentCategories(CancellationToken ct)
    {
        var result = await sender.Send(new GetAllCategoriesQuery(), ct);
        var categories = result.IsSuccess ? result.Data : [];
        ViewBag.ParentCategories = categories.Select(c => new ParentCategoryDto(c.Id, c.Name)).ToList();
    }

    private async Task LoadParentCategoriesForEdit(CancellationToken ct, long currentId)
    {
        var result = await sender.Send(new GetAllCategoriesQuery(), ct);
        var categories = result.IsSuccess
            ? result.Data.Where(c => c.Id != currentId).Select(c => new ParentCategoryDto(c.Id, c.Name)).ToList()
            : [];
        ViewBag.ParentCategories = categories;
    }
}

public record ParentCategoryDto(long Id, string Name);
