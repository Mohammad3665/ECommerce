using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "لیست محصولات";
        return View();
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "افزودن محصول";
        return View();
    }

    public IActionResult Edit(long id)
    {
        ViewData["Title"] = "ویرایش محصول";
        return View();
    }
}
