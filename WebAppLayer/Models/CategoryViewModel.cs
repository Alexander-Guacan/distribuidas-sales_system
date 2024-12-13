using Entities;

namespace WebAppLayer.Models;

public class CategoryViewModel
{
    public List<Category> Categories { get; set; } = new List<Category>(); // Lista de Category
    public Category? CategorySearch { get; set; } // Category encontrado
    public Category? CategoryEdit { get; set; } // Category encontrado
    public string Error { get; set; } = "";// Mensajes de error
}
