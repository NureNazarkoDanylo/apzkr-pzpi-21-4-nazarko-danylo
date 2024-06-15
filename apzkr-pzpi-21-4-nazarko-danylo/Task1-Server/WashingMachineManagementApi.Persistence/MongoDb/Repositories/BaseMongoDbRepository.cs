using MongoDB.Driver;
using WashingMachineManagementApi.Application.Common.IRepositories;
using WashingMachineManagementApi.Domain.Entities;

namespace WashingMachineManagementApi.Persistence.MongoDb.Repositories;

public abstract class BaseMongoDbRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TKey : IEquatable<TKey>
    where TEntity : EntityBase<TKey>
{
    protected IMongoCollection<TEntity> _collection;

    public BaseMongoDbRepository(MongoDbContext mongoDbContext, string collectionName)
    {
        _collection = mongoDbContext.Db.GetCollection<TEntity>(collectionName);
    }

    public IQueryable<TEntity> Queryable => _collection.AsQueryable();

    public async Task<TEntity> AddOneAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, null, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _collection.InsertManyAsync(entities, null, cancellationToken);
        return entities;
    }


    public async Task<TEntity> UpdateOneAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity, new ReplaceOptions(), cancellationToken);

        return entity;
    }

    public async Task DeleteOneAsync(TKey id, CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
        await _collection.DeleteOneAsync(filter);
    }
}
