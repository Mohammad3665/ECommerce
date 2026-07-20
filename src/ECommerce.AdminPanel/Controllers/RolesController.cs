using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class RolesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "لیست نقش‌ها";
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن نقش";
        return View();
    }

    public IActionResult Edit(Guid id)
    {
        ViewData["Title"] = "ویرایش نقش";
        return View();
    }
}
