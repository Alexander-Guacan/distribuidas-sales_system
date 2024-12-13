using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class EntityFrameworkRepository : IRepository
{
    private DbContext _dbContext;

    public EntityFrameworkRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
    {
        TEntity? result;

        try
        {
            _dbContext.Set<TEntity>().Add(toCreate);
            _dbContext.SaveChanges();
            result = toCreate;
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public bool Delete<TEntity>(TEntity toDelete) where TEntity : class
    {
        bool result;

        try
        {
            _dbContext.Entry(toDelete).State = EntityState.Deleted;
            result = _dbContext.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public void Dispose()
    {
        if (_dbContext != null)
        {
            _dbContext.Dispose();
        }
    }

    public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
    {
        List<TEntity>? result = null;

        try
        {
            result = _dbContext.Set<TEntity>().Where(criteria).ToList();
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
    {

        TEntity? result;

        try
        {
            result = _dbContext.Set<TEntity>().FirstOrDefault(criteria);
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
    {
        bool result;

        try
        {
            _dbContext.Entry(toUpdate).State = EntityState.Modified;
            result = _dbContext.SaveChanges() > 0;
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }
}
