using Api.Shared.Domain.Common.Models;
using Api.Shared.Domain.MovieAggregate.Events;
using Api.Shared.Domain.MovieAggregate.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Shared.Domain.MovieAggregate;

public sealed class Movie : AggregateRoot<MovieId, string>
{
    public string Title { get; private set; }
    public double VoteAverage { get; private set; }
    public int VoteCount { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public int Runtime { get; private set; }
    public string BackdropPath { get; private set; }
    public string Overview { get; private set; }
    public string PosterPath { get; private set; }
    public string Tagline { get; private set; }
    public string Genres { get; private set; }
    public string ProductionCompanies { get; private set; }
    public string SpokenLanguages { get; private set; }
    public string Keywords { get; private set; }

    private Movie(
        MovieId id,
        string title,
        double voteAverage,
        int voteCount,
        DateTime releaseDate,
        int runtime,
        string backdropPath,
        string overview,
        string posterPath,
        string tagline,
        string genres,
        string productionCompanies,
        string spokenLanguages,
        string keywords) : base(id)
    {
        Title = title;
        VoteAverage = voteAverage;
        VoteCount = voteCount;
        ReleaseDate = releaseDate;
        Runtime = runtime;
        BackdropPath = backdropPath;
        Overview = overview;
        PosterPath = posterPath;
        Tagline = tagline;
        Genres = genres;
        ProductionCompanies = productionCompanies;
        SpokenLanguages = spokenLanguages;
        Keywords = keywords;
    }

    public static Movie Create(
        string title,
        double voteAverage,
        int voteCount,
        DateTime releaseDate,
        int runtime,
        string backdropPath,
        string overview,
        string posterPath,
        string tagline,
        string genres,
        string productionCompanies,
        string spokenLanguages,
        string keywords)
    {
        Movie movie = new(
            MovieId.CreateUnique(),
            title,
            voteAverage,
            voteCount,
            releaseDate,
            runtime,
            backdropPath,
            overview,
            posterPath,
            tagline,
            genres,
            productionCompanies,
            spokenLanguages,
            keywords);

        movie.AddDomainEvent(new MovieCreatedEvent(movie));

        return movie;
    }
}
