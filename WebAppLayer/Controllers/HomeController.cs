using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppLayer.Controllers;

[Authorize] // Requiere autenticación para acceder a este controlador
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}