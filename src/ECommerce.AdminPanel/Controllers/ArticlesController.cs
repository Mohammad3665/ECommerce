using ECommerce.AdminPanel.Models;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Features.Articles.Commands.CreateArticle;
using ECommerce.Application.Features.Articles.Commands.EditArticle;
using ECommerce.Application.Features.Articles.Commands.DeleteArticle;
using ECommerce.Application.Features.Articles.Commands.ChangeArticleStatus;
using ECommerce.Application.Features.Articles.Queries.GetAllArticles;
using ECommerce.Application.Features.Articles.Queries.AdminGetArticleBySlug;
using ECommerce.Application.Features.ArticleCategories.Queries.GetAllArticleCategories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class ArticlesController(ISender sender, IFileService fileService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "مدیریت مقالات";
        var result = await sender.Send(new GetAllArticlesQuery(), ct);
        var articles = result.IsSuccess ? result.Data.Adapt<List<ArticleListViewModel>>() : [];
        return View(articles);
    }

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        ViewData["Title"] = "افزودن مقاله";
        await LoadArticleCategories(ct);
        return View(new ArticleFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArticleFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن مقاله";
            await LoadArticleCategories(ct);
            return View(model);
        }

        var imageUrl = model.ImageFile is not null
            ? await fileService.SaveFileAsync(model.ImageFile, model.EnglishTitle, "uploads/articles")
            : null;

        // TODO: Get AuthorId from current user context
        var command = new CreateArticleCommand(
            model.Title, model.EnglishTitle, model.Content, model.Summary,
            model.Status.ToString(), model.ArticleCategoryId,
            Guid.Empty, imageUrl);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            if (!string.IsNullOrEmpty(imageUrl)) fileService.DeleteFile(imageUrl);
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "افزودن مقاله";
            await LoadArticleCategories(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string slug, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش مقاله";
        var result = await sender.Send(new AdminGetArticleBySlugQuery(slug), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data;
        var model = new ArticleFormViewModel
        {
            Id = dto!.Id,
            Title = dto.Title,
            EnglishTitle = dto.EnglishTitle,
            Slug = dto.Slug,
            Content = dto.Content,
            Summary = dto.Summary,
            ArticleCategoryId = dto.ArticleCategoryId,
            Status = (int)dto.Status,
            ExistingImageUrl = dto.ImageUrl
        };

        await LoadArticleCategories(ct);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ArticleFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش مقاله";
            await LoadArticleCategories(ct);
            return View(model);
        }

        var imageUrl = model.ExistingImageUrl;
        if (model.ImageFile is not null)
        {
            imageUrl = await fileService.SaveFileAsync(model.ImageFile, model.EnglishTitle, "uploads/articles");
        }

        var command = new EditArticleCommand(
            model.Slug, model.Title, model.EnglishTitle, model.Content,
            model.Summary, model.ArticleCategoryId, imageUrl);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش مقاله";
            await LoadArticleCategories(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string slug, CancellationToken ct)
    {
        var command = new DeleteArticleCommand(slug);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در حذف مقاله";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(string slug, string status, CancellationToken ct)
    {
        var command = new ChangeArticleStatusCommand(slug, status);
        await sender.Send(command, ct);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadArticleCategories(CancellationToken ct)
    {
        var result = await sender.Send(new GetAllArticleCategoriesQuery(), ct);
        var categories = result.IsSuccess ? result.Data.ToList() : [];
        ViewBag.ArticleCategories = categories;
    }
}
