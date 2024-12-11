using System.Security.Claims;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace NWind.MVCPLS.Controllers;

public class AccountController : Controller
{
    // GET: Account/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var admin = new User
        {
            Email = "adguacan@espe.edu.ec",
            Password = "admin&780",
            Role = new Role
            {
                RoleName = "Admin"
            }
        };

        // Simulación de autenticación (reemplazar con lógica real)
        if (email != admin.Email && password != admin.Password)
        {
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        // Crear claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, email),
            new(ClaimTypes.Role, admin.Role.RoleName) // Rol del usuario
        };

        // Crear la identidad del usuario
        var identity = new ClaimsIdentity(claims, "CookieAuth");
        var principal = new ClaimsPrincipal(identity);

        // Autenticar al usuario
        await HttpContext.SignInAsync("CookieAuth", principal);

        return RedirectToAction(admin.Role.RoleName, "Panel");
    }

    // GET: Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("Login", "Account");
    }

    // GET: Account/AccessDenied
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}