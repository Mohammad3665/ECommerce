using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class OrdersController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "لیست سفارشات";
        return View();
    }

    public IActionResult Details(long id)
    {
        ViewData["Title"] = "جزئیات سفارش";
        return View();
    }

    public IActionResult ChangeStatus(long id)
    {
        ViewData["Title"] = "تغییر وضعیت سفارش";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangeStatus(long id, string paymentStatus, string shippingStatus)
    {
        // TODO: Update order status in database
        return RedirectToAction(nameof(Details), new { id });
    }
}
