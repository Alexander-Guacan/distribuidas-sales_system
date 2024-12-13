using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Entities;

namespace WebAppLayer.Controllers
{
    public class ProductController : Controller
    {
        // Acción GET: Los roles 'Client', 'Employee' y 'Admin' tienen acceso
        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == "Client" || userRole == "Employee" || userRole == "Admin")
            {
                return View();
            }
            return RedirectToAction("Login", "Account"); // Redirige si el rol no tiene acceso
        }

        // Acción POST: Solo los roles 'Admin' y 'Employee' pueden hacer un POST
        [HttpPost]
        public IActionResult Index(Product product)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == "Client")
            {
                return RedirectToAction("Index"); // Redirige si el rol es Client y trata de hacer un POST
            }

            if (userRole == "Admin" || userRole == "Employee")
            {
                // Lógica para agregar un producto, por ejemplo
                // _productService.AddProduct(product);

                return RedirectToAction("Manage"); // Redirige a la gestión de productos si el POST es exitoso
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
