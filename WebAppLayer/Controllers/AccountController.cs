using Microsoft.AspNetCore.Mvc;
using WebAppLayer.Models;

namespace WebAppLayer.Controllers
{
    public class AccountController : Controller
    {
        // Lista estática de usuarios para pruebas (se puede usar una base de datos en producción)
        private static readonly List<User> Users = new()
        {
            new User { Username = "admin", Password = "Admin123", Role = Role.Admin },
            new User { Username = "viewer", Password = "Viewer123", Role = Role.Employee },
            new User { Username = "client", Password = "Client123", Role = Role.Client }
        };

        // Vista de login
        public IActionResult Login()
        {
            return View();
        }

        // Maneja el login con la validación del usuario
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Guardar el rol como una cadena y el nombre de usuario en la sesión
            HttpContext.Session.SetString("Role", user.Role.ToString()); // Convertimos el Role a string
            HttpContext.Session.SetString("Username", user.Username);

            ViewBag.Role = user.Role;
            if (user.Role == Role.Admin)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (user.Role == Role.Employee)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (user.Role == Role.Client)
            {
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index", "Home");
        }

        // Cierra sesión y limpia la sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
