using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Application.Common.IRepositories;

public interface IBaseRepository<TEntity, TKey> 
    where TKey : IEquatable<TKey>
    where TEntity : EntityBase<TKey>
{
    public IQueryable<TEntity> Queryable { get; }

    Task<TEntity> AddOneAsync(TEntity entity, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task<TEntity> UpdateOneAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteOneAsync(TKey id, CancellationToken cancellationToken);
}
