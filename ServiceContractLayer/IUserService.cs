using CoreWCF;
using Entities;

namespace ServiceContractLayer;

[ServiceContract]
public interface IUserService
{
    public User Create(User service);

    public User RetrieveById(int id);

    public bool Update(User userToUpdate);

    public bool Delete(int id);
}