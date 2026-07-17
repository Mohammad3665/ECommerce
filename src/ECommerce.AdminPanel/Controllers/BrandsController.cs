using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class BrandsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "مدیریت برندها";
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن برند";
        return View();
    }

    public IActionResult Edit(long id)
    {
        ViewData["Title"] = "ویرایش برند";
        return View();
    }
}
