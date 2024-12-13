using Entities;

namespace WebAppLayer.Models;

public class ProductViewModel
{
    public List<Product> Products { get; set; } = new List<Product>(); // Lista de productos
    public Product? ProductSearch { get; set; } // Producto encontrado
    public Product? ProductEdit { get; set; } // Producto encontrado
    public string Error { get; set; } = "";// Mensajes de error
}
