using Microsoft.AspNetCore.Mvc;

namespace WebAppLayer.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == "Admin")
            {
                return View();
            }
            return RedirectToAction("Login", "Account"); // Redirige si el rol no tiene acceso
        }

        public IActionResult Manage()
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == "Admin" || userRole == "Viewer")
            {
                return View();
            }
            return RedirectToAction("Index", "Product"); // Redirige si no es admin ni viewer
        }
    }
}
