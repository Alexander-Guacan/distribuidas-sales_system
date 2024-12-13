using Entities;

namespace ServiceContractLayer;

public interface IProductService
{
    public Product Create(Product product);
    public Product RetrieveById(int id);
    public bool Update(Product productToUpdate);
    public bool Delete(int id);
    public List<Product> FilterByCategoryId(int categoryId);
    public List<Product> GetProducts();
}
