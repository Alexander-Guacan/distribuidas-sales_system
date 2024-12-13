using Entities;

namespace ServiceContractLayer;

public interface IAuthService
{
    public User? Login(User user);
}