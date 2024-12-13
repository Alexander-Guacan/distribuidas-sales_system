using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProxyServiceLayer;
using System.Security.Claims;

namespace WebAppLayer.Controllers;

public class AccountController : Controller
{
    // Vista de login
    [HttpGet]
    [AllowAnonymous] // Permitir acceso sin autenticación
    public IActionResult Login()
    {
        return View();
    }

    // Manejar el login con validación del usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous] // Permitir acceso sin autenticación
    public async Task<IActionResult> Login(string email, string password)
    {
        var authProxy = new AuthProxy();

        // Simulación de autenticación mediante un servicio
        User? user = authProxy.Login(new User { Email = email, Password = password });

        if (user == null || user.Role == null)
        {
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // Crear las claims del usuario (email, rol, etc.)
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.RoleName) // Asignar el rol
        };

        // Crear el principal con las claims
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Mantener sesión después de cerrar navegador
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(2) // Duración de la sesión
        };

        // Registrar la autenticación del usuario
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        return RedirectToAction("Index", "Home"); // Redirigir a Home
    }

    // Cierra la sesión del usuario y limpia los datos
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Cerrar sesión
        return RedirectToAction("Login", "Account");
    }

    // Vista de acceso denegado
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}