using ECommerce.AdminPanel.Models;
using ECommerce.Application.Features.Roles.Commands.CreateRole;
using ECommerce.Application.Features.Roles.Commands.EditRole;
using ECommerce.Application.Features.Roles.Queries.GetAllRoles;
using ECommerce.Application.Features.Roles.Queries.GetRoleBySlug;
using ECommerce.Application.Features.Permissions.Queries.GetAllPermissions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class RolesController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "لیست نقش‌ها";
        var result = await sender.Send(new GetAllRolesQuery(), ct);
        var roles = result.IsSuccess ? result.Data.Select(r => new RoleListViewModel
        {
            Id = r.Id,
            Name = r.Name,
            DisplayName = r.DisplayName,
            Description = r.Description,
            Level = r.Level,
            IsDefault = r.IsDefault
        }).ToList() : [];
        return View(roles);
    }

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        ViewData["Title"] = "افزودن نقش";
        await LoadPermissions(ct);
        return View(new RoleFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن نقش";
            await LoadPermissions(ct);
            return View(model);
        }

        var selectedPermissionIds = model.SelectedPermissionIds ?? [];
        var command = new CreateRoleCommand(
            model.Name, model.DisplayName, model.Description,
            model.Level, model.GrantAllPermissions, selectedPermissionIds);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "افزودن نقش";
            await LoadPermissions(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string slug, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش نقش";
        var result = await sender.Send(new GetRoleBySlugQuery(slug), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data!;
        var model = new RoleFormViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            DisplayName = dto.DisplayName,
            Slug = dto.Slug,
            Description = dto.Description,
            Level = dto.Level,
            IsDefault = dto.IsDefault,
            SelectedPermissionIds = dto.PermissionIds ?? []
        };
        await LoadPermissions(ct);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(RoleFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش نقش";
            await LoadPermissions(ct);
            return View(model);
        }

        var selectedPermissionIds = model.SelectedPermissionIds ?? [];
        var command = new EditRoleCommand(
            model.Slug, model.DisplayName, model.Description,
            model.Level, model.GrantAllPermissions, selectedPermissionIds);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش نقش";
            await LoadPermissions(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadPermissions(CancellationToken ct)
    {
        var result = await sender.Send(new GetAllPermissionsQuery(), ct);
        var permissions = result.IsSuccess ? result.Data.ToList() : [];
        ViewBag.Permissions = permissions;
        ViewBag.PermissionsByModule = permissions.GroupBy(p => p.Module)
            .ToDictionary(g => g.Key, g => g.ToList());
    }
}
