using Microsoft.AspNetCore.Mvc;

namespace WebAppLayer.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole != "Admin")
            {
                return RedirectToAction("Login", "Account"); // Redirige si no es admin
            }
            return View();
        }
    }
}
