using System.Linq.Expressions;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Persistence.PostgreSQL.Repositories;

public class BasePostgreSQLRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TKey : IEquatable<TKey>
    where TEntity : EntityBase<TKey>
{
    protected ApplicationDbContext _dbContext;

    public BasePostgreSQLRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Queryable => _dbContext.Set<TEntity>().AsQueryable();

    public async Task<TEntity> AddOneAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync();
        return entities;
    }


    public async Task<TEntity> UpdateOneAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteOneAsync(TKey id, CancellationToken cancellationToken)
    {
        // await _dbContext.Set<TEntity>().Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
        var entity = _dbContext.Set<TEntity>().Find(id);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
