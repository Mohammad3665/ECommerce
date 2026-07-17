using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class SlidesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "مدیریت اسلایدرها";
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن اسلایدر";
        return View();
    }

    public IActionResult Edit(long id)
    {
        ViewData["Title"] = "ویرایش اسلایدر";
        return View();
    }
}
