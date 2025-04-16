using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Shared.Persistence;

public sealed class FilmDeckDbContext
{
    public IMongoDatabase Database { get; }

    public FilmDeckDbContext(IOptions<FilmDeckDbSettings> filmDeckDbSettings)
    {
        var settings = filmDeckDbSettings.Value;

        var client = new MongoClient(settings.ConnectionStrings);
        Database = client.GetDatabase(settings.DatabaseName);
    }
}
