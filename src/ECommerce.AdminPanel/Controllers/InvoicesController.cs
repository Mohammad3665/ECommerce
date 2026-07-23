using ECommerce.AdminPanel.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class InvoicesController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "لیست صورتحساب‌ها";
        // TODO: Send GetInvoicesQuery when available
        return View(new List<InvoiceListViewModel>());
    }

    public async Task<IActionResult> Details(long id, CancellationToken ct)
    {
        ViewData["Title"] = "صورتحساب";
        // TODO: Send GetInvoiceDetailQuery when available
        var model = new InvoiceDetailViewModel { Id = id };
        return View(model);
    }
}
