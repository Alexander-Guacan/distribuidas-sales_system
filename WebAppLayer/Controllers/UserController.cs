using Microsoft.AspNetCore.Mvc;
using Entities;
using Microsoft.AspNetCore.Authorization;
using ProxyServiceLayer;
using WebAppLayer.Models;

namespace WebAppLayer.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly UserViewModel _UserViewModel;

    public UserController()
    {
        _UserViewModel = new UserViewModel();
    }

    // Acción GET: Los roles 'Client', ' y 'Admin' tienen acceso
    [HttpGet]
    public IActionResult Index()
    {
        // No necesitas validar los roles aquí porque todos los usuarios autenticados tienen acceso.
        var UserProxy = new UserProxy();
        var Users = UserProxy.GetUsers();
        _UserViewModel.Users = Users;
        return View(_UserViewModel);
    }

    // Acción POST: Solo los roles 'Admin' y ' pueden agregar Useros
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User User)
    {
        var UserProxy = new UserProxy();
        var result = UserProxy.Create(User);
        _UserViewModel.Error = result == null ? "Usero no pudo ser guardado" : "";
        return RedirectToAction("Index");
    }

    // Acción GET: Solo los roles 'Admin' y ' pueden gestionar Useros

    [HttpGet]
    public IActionResult Search(int UserId)
    {
        var UserProxy = new UserProxy();
        var UserFound = UserProxy.RetrieveById(UserId);
        _UserViewModel.Users = UserProxy.GetUsers();
        _UserViewModel.UserSearch = UserFound; // Asignar el User encontrado
        _UserViewModel.Error = UserFound == null ? "User not found" : ""; // Error si el Usero no se encuentra

        return View("Index", _UserViewModel); // Pasar el modelo a la vista
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var UserProxy = new UserProxy();
        bool isDeleted = UserProxy.Delete(id);
        _UserViewModel.Error = isDeleted ? "" : "User cannot be deleted";

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Update(User User) {
        var UserProxy = new UserProxy();
        bool isUpdated = UserProxy.Update(User);
        _UserViewModel.Error = isUpdated ? "" : "User cannot be updated";

        return RedirectToAction("Index");
    }
}

