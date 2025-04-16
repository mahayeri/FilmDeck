using Api.Shared.Domain.Common.Models;
using Api.Shared.Persistence.Repositories;
using Api.Shared.RequestHelpers;
using MongoDB.Driver;

namespace Api.Shared.Domain.Contracts;

public class MongoRepository<TAggregateRoot, TId, TIdType>(IMongoDatabase database, string collectionName)
    : IRepository<TAggregateRoot, TId, TIdType>
        where TAggregateRoot : AggregateRoot<TId, TIdType>
        where TId : AggregateRootId<TIdType>
{
    private readonly IMongoCollection<TAggregateRoot> _collection
        = database.GetCollection<TAggregateRoot>(collectionName);
    protected IMongoCollection<TAggregateRoot> Collection => _collection;

    public async Task AddAsync(TAggregateRoot entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(TId id)
    {
        var filter = Builders<TAggregateRoot>.Filter.Eq(t => t.Id, id);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<TAggregateRoot>> GetAllAsync()
    {
        var filter = Builders<TAggregateRoot>.Filter.Empty;
        return await _collection.Find(filter).ToListAsync();
    }

    public async virtual Task<TAggregateRoot> GetByIdAsync(TId id)
    {
        var filter = Builders<TAggregateRoot>.Filter.Eq(t => t.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<PagedList<TAggregateRoot>> GetPagedAsync(
        FilterDefinition<TAggregateRoot> filter,
        SortDefinition<TAggregateRoot> sort,
        int pageNumber,
        int pageSize)
    {
        // get total count
        var totalCount = await _collection.CountDocumentsAsync(filter);

        // apply pagination
        var items = await _collection
            .Find(filter)
            .Sort(sort)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return new PagedList<TAggregateRoot>(items, pageNumber, pageSize, (int)totalCount);
    }
    public async Task<PagedList<TProjection>> GetPagedAsync<TProjection>(
        FilterDefinition<TAggregateRoot> filter,
        SortDefinition<TAggregateRoot> sort,
        ProjectionDefinition<TAggregateRoot, TProjection> projection,
        int pageNumber,
        int pageSize)
    {
        // Get total count
        var totalCount = await _collection.CountDocumentsAsync(filter);

        // Apply pagination and projection
        var items = await _collection.Find(filter)
            .Sort(sort)
            .Project(projection)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return new PagedList<TProjection>(items, pageNumber, pageSize, (int)totalCount);
    }
    public async Task UpdateAsync(TAggregateRoot entity)
    {
        var filter = Builders<TAggregateRoot>.Filter.Eq(t => t.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
    }
}
