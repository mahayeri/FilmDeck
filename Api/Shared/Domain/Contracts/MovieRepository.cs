using Api.Shared.Domain.MovieAggregate;
using Api.Shared.Domain.MovieAggregate.ValueObjects;
using Api.Shared.Persistence;
using Api.Shared.Persistence.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Shared.Domain.Contracts;

public class MovieRepository(FilmDeckDbContext dbContext, IOptions<FilmDeckDbSettings> options)
    : MongoRepository<Movie, MovieId, string>(dbContext.Database, options.Value.MoviesCollectionName)
        , IMovieRepository
{
    public async Task<IEnumerable<Movie>> GetByYearAsync(DateTime releaseDate)
    {
        var filter = Builders<Movie>.Filter.Eq(m => m.ReleaseDate, releaseDate);
        return await Collection.Find(filter).ToListAsync();
    }
    public async override Task<Movie> GetByIdAsync(MovieId id)
    {
        return await Collection.Find(m => m.Id.ToString() == id.Value).FirstOrDefaultAsync();
        // var objectId = id.ToObjectId();
        // var filter = Builders<Movie>.Filter.Eq(t => t.Id.Value, id.Value);
        // return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<TProjection> GetByIdAsync<TProjection>(
        ProjectionDefinition<Movie, TProjection> projection,
        MovieId id)
    {
        return await Collection
            .Find(m => m.Id.ToString() == id.Value)
            .Project(projection)
            .FirstOrDefaultAsync();
    }
}
