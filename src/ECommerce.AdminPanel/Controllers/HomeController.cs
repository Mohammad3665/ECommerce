using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "داشبورد";
        return View();
    }
}
