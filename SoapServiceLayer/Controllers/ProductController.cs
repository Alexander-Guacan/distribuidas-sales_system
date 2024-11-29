using BusinessLogicLayer;
using Entities;
using ServiceLayerContract;

namespace SoapServiceLayer.Controllers;

public class ProductController : IProductService
{
    public Product Create(Product product)
    {
        var productLogic = new ProductLogic();
        var newProduct = productLogic.Create(product);
        return newProduct;
    }

    public bool Delete(int id)
    {
        var productLogic = new ProductLogic();
        var isDeleted = productLogic.Delete(id);
        return isDeleted;
    }

    public List<Product> FilterByCategoryId(int categoryId)
    {
        var productLogic = new ProductLogic();
        var productsFiltered = productLogic.FilterByCategoryId(categoryId);
        return productsFiltered;
    }

    public Product RetrieveById(int id)
    {
        var productLogic = new ProductLogic();
        var productRetrieved = productLogic.RetrieveById(id);
        return productRetrieved;
    }

    public bool Update(Product productToUpdate)
    {
        var productLogic = new ProductLogic();
        var isUpdated = productLogic.Update(productToUpdate);
        return isUpdated;
    }
}