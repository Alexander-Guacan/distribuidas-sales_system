using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer;

public class PermissionLogic
{
    // LEER
    public Permission RetrieveById(int permissionId)
    {
        Permission? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Retrieve<Permission>(p => p.PermissionId == permissionId);
        }

        return result;
    }
}
