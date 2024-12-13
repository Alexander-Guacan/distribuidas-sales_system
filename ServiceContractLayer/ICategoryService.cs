using Entities;

namespace ServiceContractLayer;

public interface ICategoryService
{
    public Category Create(Category service);
    public Category RetrieveById(int id);
    public bool Update(Category categoryToUpdate);
    public bool Delete(int id);
    public List<Category> GetCategories();
}