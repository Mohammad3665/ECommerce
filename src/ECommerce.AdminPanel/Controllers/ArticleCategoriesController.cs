using ECommerce.AdminPanel.Models;
using ECommerce.Application.Features.ArticleCategories.Commands.CreateArticleCategory;
using ECommerce.Application.Features.ArticleCategories.Commands.EditArticleCategory;
using ECommerce.Application.Features.ArticleCategories.Commands.DeleteArticleCategory;
using ECommerce.Application.Features.ArticleCategories.Queries.GetAllArticleCategories;
using ECommerce.Application.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class ArticleCategoriesController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "مدیریت دسته‌بندی مقالات";
        var result = await sender.Send(new GetAllArticleCategoriesQuery(), ct);
        var categories = result.IsSuccess ? result.Data.Adapt<List<ArticleCategoryListViewModel>>() : [];
        return View(categories);
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن دسته‌بندی مقاله";
        return View(new ArticleCategoryFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArticleCategoryFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن دسته‌بندی مقاله";
            return View(model);
        }

        var command = new CreateArticleCategoryCommand(model.Name, model.EnglishName);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "افزودن دسته‌بندی مقاله";
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string slug, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش دسته‌بندی مقاله";
        var result = await sender.Send(new GetArticleCategoryBySlugQuery(slug), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data!;
        var model = new ArticleCategoryFormViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            EnglishName = dto.EnglishName,
            Slug = dto.Slug
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ArticleCategoryFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش دسته‌بندی مقاله";
            return View(model);
        }

        var command = new EditArticleCategoryCommand(model.Slug, model.Name, model.EnglishName);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش دسته‌بندی مقاله";
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string slug, CancellationToken ct)
    {
        var command = new DeleteArticleCategoryCommand(slug);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در حذف دسته‌بندی";

        return RedirectToAction(nameof(Index));
    }
}
