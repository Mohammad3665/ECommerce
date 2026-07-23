using ECommerce.AdminPanel.Models;
using ECommerce.Application.Features.Comments.Commands.ChangeCommentStatus;
using ECommerce.Application.Features.Comments.Commands.DeleteCommentByAdmin;
using ECommerce.Application.Features.Comments.Queries.GetAllComments;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class CommentsController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "مدیریت نظرات";
        var query = new GetAllCommentsQuery { PageNumber = 1, PageSize = 50 };
        var result = await sender.Send(query, ct);
        var comments = result.IsSuccess
            ? result.Data.Items.Select(c => new CommentListViewModel
            {
                Id = c.Id,
                AuthorName = c.UserFullName,
                Title = c.Title,
                Content = c.Content,
                TargetName = c.TargetName,
                IsApproved = c.IsApproved,
                ApprovedAt = c.ApprovedAt,
                CreatedAt = c.CreatedAt
            }).ToList()
            : [];
        return View(comments);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(Guid id, CancellationToken ct)
    {
        var command = new ChangeCommentStatusCommand(id, true);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در تایید نظر";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reject(Guid id, CancellationToken ct)
    {
        var command = new ChangeCommentStatusCommand(id, false);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در رد نظر";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var command = new DeleteCommentByAdminCommand(id);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در حذف نظر";

        return RedirectToAction(nameof(Index));
    }
}
