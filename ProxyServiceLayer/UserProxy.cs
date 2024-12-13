using Entities;
using ServiceContractLayer;

namespace ProxyServiceLayer;

public class UserProxy : Proxy, IUserService
{
    private readonly string _baseApiUrl = "/User";

    public async Task<User> CreateAsync(User newUser)
    {
        return await SendPost<User, User>($"{_baseApiUrl}", newUser);
    }

    public User Create(User product)
    {
        User? result = null;
        Task.Run(async () => result = await CreateAsync(product)).Wait();
        return result;
    }

    public async Task<User> RetrieveUserByIdAsync(int id)
    {
        return await SendGet<User>($"{_baseApiUrl}/{id}");
    }

    public User RetrieveById(int id)
    {
        User? result = null;
        Task.Run(async () => result = await RetrieveUserByIdAsync(id)).Wait();
        return result;
    }

    public async Task<bool> UpdateAsync(User productToUpdate)
    {
        return await SendPut<bool, User>($"{_baseApiUrl}", productToUpdate);
    }

    public bool Update(User productToUpdate)
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

    public async Task<List<User>> GetUsersAsync()
    {
        return await SendGet<List<User>>($"{_baseApiUrl}");
    }

    public List<User> GetUsers()
    {
        var result = new List<User>();
        Task.Run(async () => result = await GetUsersAsync()).Wait();
        return result;
    }
}