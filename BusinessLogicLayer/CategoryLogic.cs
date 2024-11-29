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
            Category res = repository.Retrieve<Category>(c => c.CategoryId == category.CategoryId);

            if (res == null)
            {
                result = repository.Create(category);
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
}