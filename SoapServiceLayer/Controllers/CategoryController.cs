
using BusinessLogicLayer;
using Entities;
using ServiceContractLayer;

namespace SoapServiceLayer.Controllers;

public class CategoryController : ICategoryService
{
    public Category Create(Category category)
    {
        var categoryLogic = new CategoryLogic();
        var newCategory = categoryLogic.Create(category);
        return newCategory;
    }

    public bool Delete(int id)
    {
        var categoryLogic = new CategoryLogic();
        var isDeleted = categoryLogic.Delete(id);
        return isDeleted;
    }

    public Category RetrieveById(int id)
    {
        var categoryLogic = new CategoryLogic();
        var categoryRetrieved = categoryLogic.RetrieveById(id);
        return categoryRetrieved;
    }

    public bool Update(Category categoryToUpdate)
    {
        var categoryLogic = new CategoryLogic();
        var isUpdated = categoryLogic.Update(categoryToUpdate);
        return isUpdated;
    }
}