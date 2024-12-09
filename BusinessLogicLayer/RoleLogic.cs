using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer;

public class RoleLogic
{
    // LEER
    public Role RetrieveById(int roleId)
    {
        Role? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Retrieve<Role>(r => r.RoleId == roleId);
        }

        return result;
    }
}
