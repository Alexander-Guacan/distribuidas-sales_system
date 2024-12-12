using Entities;
using ServiceLayerContract;

namespace ProxyServiceLayer;

public class CategoryProxy : Proxy, ICategoryService
{
    private readonly string _baseApiUrl = "/Category";

    public async Task<Category> CreateAsync(Category newCategory)
    {
        return await SendPost<Category, Category>($"{_baseApiUrl}", newCategory);
    }

    public Category Create(Category product)
    {
        Category? result = null;
        Task.Run(async () => result = await CreateAsync(product)).Wait();
        return result;
    }

    public async Task<Category> RetrieveCategoryByIdAsync(int id)
    {
        return await SendGet<Category>($"{_baseApiUrl}/{id}");
    }

    public Category RetrieveById(int id)
    {
        Category? result = null;
        Task.Run(async () => result = await RetrieveCategoryByIdAsync(id)).Wait();
        return result;
    }

    public async Task<bool> UpdateAsync(Category productToUpdate)
    {
        return await SendPut<bool, Category>($"{_baseApiUrl}", productToUpdate);
    }

    public bool Update(Category productToUpdate)
    {
        bool result = false;
        Task.Run(async () => result = await UpdateAsync(productToUpdate)).Wait();
        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await SendDelete<bool>($"{_baseApiUrl}/{id}");
    }

    public bool Delete(int id)
    {
        bool result = false;
        Task.Run(async () => result = await DeleteAsync(id)).Wait();
        return result;
    }
}