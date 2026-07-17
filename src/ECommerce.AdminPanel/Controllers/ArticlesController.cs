using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class ArticlesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "مدیریت مقالات";
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن مقاله";
        return View();
    }

    public IActionResult Edit(long id)
    {
        ViewData["Title"] = "ویرایش مقاله";
        return View();
    }
}
