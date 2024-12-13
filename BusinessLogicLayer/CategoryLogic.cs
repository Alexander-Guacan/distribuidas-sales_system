using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer;

public class CategoryLogic
{
    public Category Create(Category category)
    {
        Category? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            Category res = repository.Retrieve<Category>(c => c.CategoryName == category.CategoryName);

            if (res == null)
            {
                result = repository.Create(category);
            } else {
                throw new Exception("This category is already registered, try another name.");
            }
        }

        return result;
    }

    public Category RetrieveById(int id)
    {
        Category? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Retrieve<Category>(p => p.CategoryId == id);
        }

        return result;
    }

    public bool Update(Category categoryToUpdate)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            Category temp = repository.Retrieve<Category>(p => p.CategoryName == categoryToUpdate.CategoryName && p.CategoryId != categoryToUpdate.CategoryId);

            if (temp == null)
            {
                result = repository.Update(categoryToUpdate);
            } // TODO: throw exception if category cannot be modified
        }

        return result;
    }

    public bool Delete(int id)
    {
        bool result = false;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            var isCategoryUsed = repository.Filter<Product>(product => product.CategoryId == id).Count > 0;

            if (!isCategoryUsed) {
                var category = RetrieveById(id);
                result = repository.Delete(category);
            }
        }

        return result;
    }

    public List<Category> GetCategories()
    {
        var result = new List<Category>();

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Filter<Category>(c => c != null);
        }

        return result;
    }
}