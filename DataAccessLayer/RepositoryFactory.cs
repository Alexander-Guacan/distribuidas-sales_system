using DataAccessLayer.Data;

namespace DataAccessLayer;

public class RepositoryFactory
{
    public static IRepository CreateRepository()
    {
        return new EntityFrameworkRepository(new ApplicationDbContext());
    }
}