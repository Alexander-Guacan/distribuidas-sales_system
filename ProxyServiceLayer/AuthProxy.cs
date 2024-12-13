using Entities;
using ServiceContractLayer;

namespace ProxyServiceLayer;

public class AuthProxy : Proxy, IAuthService
{
    private readonly string _baseApiUrl = "/Auth";

    public async Task<User> LoginAsync(User user)
    {
        return await SendPost<User, User>($"{_baseApiUrl}/Login", user);
    }

    public User? Login(User user)
    {
        User? result = null;
        Task.Run(async () => result = await LoginAsync(user)).Wait();
        return result;
    }
}