using DataAccessLayer;
using Entities;

namespace Test;

class Program
{
    static void Main()
    {
        AddCategoryAndProduct();
    }

    static void AddCategoryAndProduct()
    {
        var category = new Category
        {
            CategoryName = "Test 0",
            Description = "Category description"
        };

        var product = new Product
        {
            ProductName = "Ruffles",
            UnitPrice = 0.5m,
            UnitsInStock = 100
        };

        category.Products.Add(product);

        using (var repository = RepositoryFactory.CreateRepository())
        {
            repository.Create(category);
        }

        Console.WriteLine($"Categoria: {category.CategoryId}, Producto: {product.ProductId}");
        Console.ReadLine();
    }
}