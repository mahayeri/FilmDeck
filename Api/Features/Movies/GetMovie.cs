using Api.Shared.Domain.MovieAggregate;
using Api.Shared.Domain.MovieAggregate.ValueObjects;
using Api.Shared.Persistence.Repositories;
using Api.Shared.Slices;
using MediatR;
using MongoDB.Driver;

namespace Api.Features.Movies;

public sealed class GetMovie : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet(
            "api/movies/{id}",
            (string id,
                IMediator mediator) =>
            {
                return mediator.Send(new GetMovieQuery(id));
            }
        );
    }

    public sealed record GetMovieQuery(string Id) : IRequest<IResult>;

    public sealed class GetMovieHandler(IMovieRepository movieRepository) : IRequestHandler<GetMovieQuery, IResult>
    {
        public async Task<IResult> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            MovieId id = MovieId.Create(request.Id);

            var projection = Builders<Movie>.Projection
                .Expression(m => new MovieDto
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    PosterPath = m.PosterPath,
                    VoteAverage = m.VoteAverage,
                    BackdropPath = m.BackdropPath,
                    Genres = m.Genres,
                    Keywords = m.Keywords,
                    Overview = m.Overview,
                    ProductionCompanies = m.ProductionCompanies,
                    Runtime = m.Runtime,
                    SpokenLanguages = m.SpokenLanguages,
                    Tagline = m.Tagline,
                    VoteCount = m.VoteCount,
                    ReleaseDate = m.ReleaseDate
                });

            var movie = await movieRepository.GetByIdAsync(projection, id);

            return movie is null
                ? Results.NotFound()
                : Results.Ok(movie);
        }
    }

    public sealed class MovieDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required double VoteAverage { get; set; }
        public required int VoteCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public required int Runtime { get; set; }
        public required string BackdropPath { get; set; }
        public required string Overview { get; set; }
        public required string PosterPath { get; set; }
        public required string Tagline { get; set; }
        public required string Genres { get; set; }
        public required string ProductionCompanies { get; set; }
        public required string SpokenLanguages { get; set; }
        public required string Keywords { get; set; }
    }
}
