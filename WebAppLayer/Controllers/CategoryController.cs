using Microsoft.AspNetCore.Mvc;
using Entities;
using Microsoft.AspNetCore.Authorization;
using ProxyServiceLayer;
using WebAppLayer.Models;

namespace WebAppLayer.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly CategoryViewModel _CategoryViewModel;

    public CategoryController()
    {
        _CategoryViewModel = new CategoryViewModel();
    }

    // Acción GET: Los roles 'Client', 'Employee' y 'Admin' tienen acceso
    [HttpGet]
    public IActionResult Index()
    {
        // No necesitas validar los roles aquí porque todos los usuarios autenticados tienen acceso.
        var CategoryProxy = new CategoryProxy();
        var Categories = CategoryProxy.GetCategories();
        _CategoryViewModel.Categories = Categories;
        return View(_CategoryViewModel);
    }

    // Acción POST: Solo los roles 'Admin' y 'Employee' pueden agregar Categoryos
    [Authorize(Roles = "Admin,Employee")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category Category)
    {
        var CategoryProxy = new CategoryProxy();
        var result = CategoryProxy.Create(Category);
        _CategoryViewModel.Error = result == null ? "Categoryo no pudo ser guardado" : "";
        return RedirectToAction("Index");
    }

    // Acción GET: Solo los roles 'Admin' y 'Employee' pueden gestionar Categoryos

    [HttpGet]
    public IActionResult Search(int CategoryId)
    {
        var CategoryProxy = new CategoryProxy();
        var CategoryFound = CategoryProxy.RetrieveById(CategoryId);
        _CategoryViewModel.Categories = CategoryProxy.GetCategories();
        _CategoryViewModel.CategorySearch = CategoryFound; // Asignar el Category encontrado
        _CategoryViewModel.Error = CategoryFound == null ? "Category not found" : ""; // Error si el Categoryo no se encuentra

        return View("Index", _CategoryViewModel); // Pasar el modelo a la vista
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Employee")]
    public IActionResult Delete(int id)
    {
        var CategoryProxy = new CategoryProxy();
        bool isDeleted = CategoryProxy.Delete(id);
        _CategoryViewModel.Error = isDeleted ? "" : "Category cannot be deleted";

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Employee")]
    public IActionResult Update(Category Category) {
        var CategoryProxy = new CategoryProxy();
        bool isUpdated = CategoryProxy.Update(Category);
        _CategoryViewModel.Error = isUpdated ? "" : "Category cannot be updated";

        return RedirectToAction("Index");
    }
}

