using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class UsersController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "لیست کاربران";
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن کاربر";
        return View();
    }

    public IActionResult Edit(Guid id)
    {
        ViewData["Title"] = "ویرایش کاربر";
        return View();
    }
}
