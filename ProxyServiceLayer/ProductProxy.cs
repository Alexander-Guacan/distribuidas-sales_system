using ServiceContractLayer;
using Entities;

namespace ProxyServiceLayer;

public class ProductProxy : Proxy, IProductService
{
    private readonly string _baseApiUrl = "/Product";

    public async Task<Product> CreateAsync(Product newProduct)
    {
        return await SendPost<Product, Product>($"{_baseApiUrl}", newProduct);
    }

    public Product Create(Product product)
    {
        Product? result = null;
        Task.Run(async () => result = await CreateAsync(product)).Wait();
        return result;
    }

    public async Task<Product> RetrieveProductByIdAsync(int id)
    {
        return await SendGet<Product>($"{_baseApiUrl}/{id}");
    }

    public Product RetrieveById(int id)
    {
        Product? result = null;
        Task.Run(async () => result = await RetrieveProductByIdAsync(id)).Wait();
        return result;
    }

    public async Task<bool> UpdateAsync(Product productToUpdate)
    {
        return await SendPut<bool, Product>($"{_baseApiUrl}", productToUpdate);
    }

    public bool Update(Product productToUpdate)
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

    public async Task<List<Product>> FilterByCategoryIdAsync(int id)
    {
        return await SendGet<List<Product>>($"{_baseApiUrl}/CategoryId/{id}");
    }

    public List<Product> FilterByCategoryId(int categoryId)
    {
        var result = new List<Product>();
        Task.Run(async () => result = await FilterByCategoryIdAsync(categoryId)).Wait();
        return result;
    }
}
