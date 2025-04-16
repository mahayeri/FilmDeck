using Api.Shared.Domain.Common.Models;
using Api.Shared.RequestHelpers;
using MongoDB.Driver;

namespace Api.Shared.Persistence.Repositories;

public interface IRepository<TAggregateRoot, TId, TIdType>
    where TAggregateRoot : AggregateRoot<TId, TIdType>
    where TId : AggregateRootId<TIdType>
{
    Task<TAggregateRoot> GetByIdAsync(TId id);
    Task<IEnumerable<TAggregateRoot>> GetAllAsync();
    Task<PagedList<TAggregateRoot>> GetPagedAsync(
        FilterDefinition<TAggregateRoot> filter,
        SortDefinition<TAggregateRoot> sort,
        int pageNumber,
        int pageSize);
    Task<PagedList<TProjection>> GetPagedAsync<TProjection>(
        FilterDefinition<TAggregateRoot> filter,
        SortDefinition<TAggregateRoot> sort,
        ProjectionDefinition<TAggregateRoot, TProjection> projection,
        int pageNumber,
        int pageSize);
    Task AddAsync(TAggregateRoot entity);
    Task UpdateAsync(TAggregateRoot entity);
    Task DeleteAsync(TId id);
}
