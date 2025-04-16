using Api.Shared.Domain.MovieAggregate;
using Api.Shared.Domain.MovieAggregate.ValueObjects;
using MongoDB.Driver;

namespace Api.Shared.Persistence.Repositories;

public interface IMovieRepository : IRepository<Movie, MovieId, string>
{
    Task<IEnumerable<Movie>> GetByYearAsync(DateTime releaseDate);
    Task<TProjection> GetByIdAsync<TProjection>(
        ProjectionDefinition<Movie, TProjection> projection,
        MovieId id);
}
