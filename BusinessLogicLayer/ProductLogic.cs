using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer;

public class ProductLogic
{
    public Product Create(Product newProduct)
    {
        Product? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            Product res = repository.Retrieve<Product>(p => p.ProductName == newProduct.ProductName);

            if (res == null)
            {
                result = repository.Create(newProduct);
            } // TODO: Throw product exist exception
        }

        return result;
    }

    public Product RetrieveById(int id)
    {
        Product? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Retrieve<Product>(p => p.ProductId == id);
        }

        return result;
    }

    public bool Update(Product productToUpdate)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            Product temp = repository.Retrieve<Product>(p => p.ProductName == productToUpdate.ProductName && p.ProductId != productToUpdate.ProductId);

            if (temp == null)
            {
                result = repository.Update(productToUpdate);
            } // TODO: throw exception if product cannot be modified
        }

        return result;
    }

    public bool Delete(int id)
    {
        var product = RetrieveById(id);
        if (product == null || product.UnitsInStock > 0) return false;

        using var repository = RepositoryFactory.CreateRepository();
        return repository.Delete(product);
    }

    public List<Product> FilterByCategoryId(int categoryId)
    {
        var result = new List<Product>();

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Filter<Product>(p => p.CategoryId == categoryId);
        }

        return result;
    }
}
