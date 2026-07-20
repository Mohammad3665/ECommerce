using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class CommentsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "مدیریت نظرات";
        return View();
    }
}
