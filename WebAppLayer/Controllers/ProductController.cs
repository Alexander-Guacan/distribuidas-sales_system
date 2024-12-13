using Microsoft.AspNetCore.Mvc;
using Entities;
using Microsoft.AspNetCore.Authorization;
using ProxyServiceLayer;
using WebAppLayer.Models;

namespace WebAppLayer.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly ProductViewModel _productViewModel;

    public ProductController()
    {
        _productViewModel = new ProductViewModel();
    }

    // Acción GET: Los roles 'Client', 'Employee' y 'Admin' tienen acceso
    [HttpGet]
    public IActionResult Index()
    {
        // No necesitas validar los roles aquí porque todos los usuarios autenticados tienen acceso.
        var productProxy = new ProductProxy();
        var products = productProxy.GetProducts();
        _productViewModel.Products = products;
        return View(_productViewModel);
    }

    // Acción POST: Solo los roles 'Admin' y 'Employee' pueden agregar productos
    [Authorize(Roles = "Admin,Employee")]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult Create(Product product)
    {
        var productProxy = new ProductProxy();
        var result = productProxy.Create(product);
        _productViewModel.Error = result == null ? "Producto no pudo ser guardado" : "";
        return RedirectToAction("Index");
    }

    // Acción GET: Solo los roles 'Admin' y 'Employee' pueden gestionar productos

    [HttpGet]
    public IActionResult Search(int productId)
    {
        var productProxy = new ProductProxy();
        var productFound = productProxy.RetrieveById(productId);
        _productViewModel.Products = productProxy.GetProducts();
        _productViewModel.ProductSearch = productFound; // Asignar el producto encontrado
        _productViewModel.Error = productFound == null ? "Product not found" : ""; // Error si el producto no se encuentra

        return View("Index", _productViewModel); // Pasar el modelo a la vista
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Employee")]
    public IActionResult Delete(int id)
    {
        var productProxy = new ProductProxy();
        bool isDeleted = productProxy.Delete(id);
        _productViewModel.Error = isDeleted ? "" : "Product cannot be deleted";

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Employee")]
    public IActionResult Update(Product product) {
        var productProxy = new ProductProxy();
        bool isUpdated = productProxy.Update(product);
        _productViewModel.Error = isUpdated ? "" : "Product cannot be updated";

        return RedirectToAction("Index");
    }
}
