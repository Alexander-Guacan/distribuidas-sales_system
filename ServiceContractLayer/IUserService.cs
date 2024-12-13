using Entities;

namespace ServiceContractLayer;

public interface IUserService
{
    public User Create(User service);
    public User RetrieveById(int id);
    public bool Update(User userToUpdate);
    public bool Delete(int id);
    public List<User> GetUsers();
}