using System.Diagnostics;
using Entities;
using Microsoft.AspNetCore.Mvc;
using NWind.MVCPLS.Models;
using NWindProxyService;

namespace NWind.MVCPLS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(int id)
    {
        var proxy = new ProductProxy();
        var products = proxy.FilterByCategoryId(id);
        return View("ProductList", products);
    }
    
    [HttpGet]
    public IActionResult Details(int id) {
        var proxy = new ProductProxy();
        var model = proxy.RetrieveById(id);
        return View(model);
    }
    
    public IActionResult CUD(int id = 0) {
        var proxy = new ProductProxy();
        var model = new Product();
        
        if (id != 0) {
            model = proxy.RetrieveById(id);
        }

        return View(model);
    }
    
    [HttpPost]
    public IActionResult CUD(Product newProduct, string createBtn, string updateBtn, string deleteBtn) {
        Product product;
        var proxy = new ProductProxy();
        ActionResult result = View();

        if (createBtn != null) {
            product = proxy.Create(newProduct);

            if (product != null) {
                result = RedirectToAction("CUD", new { id = product.ProductId });
            }
        } else if (updateBtn != null) {
            var isUpdate = proxy.Update(newProduct);

            if (isUpdate) {
                result = Content("El producto ha sido actualizado");
            } 
        } else if (deleteBtn != null) {
            var isDelete = proxy.Delete(newProduct.ProductId);

            if (isDelete) {
                result = Content("El producto ha sido eliminado");
            }
        }

        return result;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
