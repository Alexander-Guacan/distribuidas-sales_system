using DataAccessLayer;
using Entities;

namespace BusinessLogicLayer;

public class AuditLogLogic
{
    //CREAR
    public AuditLog Create(AuditLog log)
    {
        AuditLog? result = null;

        using (var repository = RepositoryFactory.CreateRepository())
        {
            result = repository.Create(log);
        }

        return result;
    }
}
