using ECommerce.AdminPanel.Models;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Features.Authentication.Commands.CreateUserByAdmin;
using ECommerce.Application.Features.Users.Commands.ToggleUserStatus;
using ECommerce.Application.Features.Users.Commands.EditUserProfile;
using ECommerce.Application.Features.Users.Queries.GetAllUsers;
using ECommerce.Application.Features.Users.Queries.GetUserById;
using ECommerce.Application.Features.Roles.Queries.GetAllRoles;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class UsersController(ISender sender, ICurrentUserService currentUserService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "لیست کاربران";
        var result = await sender.Send(new GetAllUsersQuery(), ct);
        var users = result.IsSuccess ? result.Data.Adapt<List<UserListViewModel>>() : [];
        return View(users);
    }

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        ViewData["Title"] = "افزودن کاربر";
        await LoadRoles(ct);
        return View(new UserFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن کاربر";
            await LoadRoles(ct);
            return View(model);
        }

        var adminId = currentUserService.UserId ?? Guid.Empty;
        var command = new CreateUserByAdminCommand(
            model.FullName, model.Email, model.PhoneNumber,
            model.Password, model.RoleName ?? string.Empty, adminId);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ایجاد کاربر");
            ViewData["Title"] = "افزودن کاربر";
            await LoadRoles(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش کاربر";
        var result = await sender.Send(new GetUserByIdQuery(id), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data!;
        var model = new UserFormViewModel
        {
            Id = dto.Id,
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            IsActive = dto.IsActive
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش کاربر";
            return View(model);
        }

        var command = new EditUserProfileCommand(model.Id, model.FullName, model.PhoneNumber);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش کاربر";
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(Guid id, bool isActive, CancellationToken ct)
    {
        var command = new ToggleUserStatusCommand(id, isActive);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در تغییر وضعیت";

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadRoles(CancellationToken ct)
    {
        var result = await sender.Send(new GetAllRolesQuery(), ct);
        ViewBag.Roles = result.IsSuccess
            ? result.Data.Select(r => new { r.Name, r.DisplayName }).ToList()
            : [];
    }
}
