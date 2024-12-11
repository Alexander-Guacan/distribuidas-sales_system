using Microsoft.AspNetCore.Mvc;

namespace NWind.MVCPLS.Controllers;

public class ProductController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}