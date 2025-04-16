namespace Api.Shared.Persistence;

public class FilmDeckDbSettings
{
    public string ConnectionStrings { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string MoviesCollectionName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
}
