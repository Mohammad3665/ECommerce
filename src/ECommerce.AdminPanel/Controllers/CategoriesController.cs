using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class CategoriesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "مدیریت دسته‌بندی‌ها";
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن دسته‌بندی";
        return View();
    }

    public IActionResult Edit(long id)
    {
        ViewData["Title"] = "ویرایش دسته‌بندی";
        return View();
    }
}
