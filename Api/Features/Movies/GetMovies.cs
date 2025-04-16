using Api.Shared.Domain.MovieAggregate;
using Api.Shared.Persistence.Repositories;
using Api.Shared.RequestHelpers;
using Api.Shared.Slices;
using MediatR;
using MongoDB.Driver;

namespace Api.Features.Movies;

public sealed class GetMovies : ISlice
{
    public void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet(
            "api/movies",
            async (
                IMediator mediator,
                [AsParameters] GetMoviesQuery query,
                HttpResponse response,
                CancellationToken cancellationToken) =>
            {
                var movies = await mediator.Send(query, cancellationToken);
                response.AddPaginationHeader(movies.Metadata);
                return Results.Ok(movies.ToList());
            }
        );
    }

    public sealed class GetMoviesQuery : IRequest<PagedList<MovieDto>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? Genre { get; set; }
    }

    public sealed class GetMoviesHandler(IMovieRepository movieRepository)
        : IRequestHandler<GetMoviesQuery, PagedList<MovieDto>>
    {
        public async Task<PagedList<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            // build the filters
            var filters = BuildFilter(request);

            // build the sort
            var sort = BuildSort(request);

            // define the projection
            var projection = Builders<Movie>.Projection
                .Expression(m => new MovieDto
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    PosterPath = m.PosterPath,
                    VoteAverage = m.VoteAverage
                });

            // get paginated results with projection    
            var pagedMovies = await movieRepository.GetPagedAsync(
                filters,
                sort,
                projection,
                request.PageNumber ?? 1,
                request.PageSize ?? 10);

            return pagedMovies;
        }

        private FilterDefinition<Movie> BuildFilter(GetMoviesQuery request)
        {
            var builder = Builders<Movie>.Filter;
            var filter = builder.Empty;

            // Apply search term
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                filter &= builder.Regex(m => m.Title, $"/{request.SearchTerm}/i");
            }

            // Apply genre filter
            if (!string.IsNullOrEmpty(request.Genre))
            {
                filter &= builder.Regex(m => m.Genres, $"/{request.Genre}/i");
            }

            return filter;
        }

        private SortDefinition<Movie> BuildSort(GetMoviesQuery request)
        {
            var builder = Builders<Movie>.Sort;
            var sort = builder.Ascending(m => m.Title); // Default sort

            if (!string.IsNullOrEmpty(request.SortColumn))
            {
                sort = request.SortOrder?.ToLower() == "desc"
                    ? builder.Descending(request.SortColumn)
                    : builder.Ascending(request.SortColumn);
            }

            return sort;
        }
    }
    public sealed class MovieDto
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public double VoteAverage { get; set; }
        public required string PosterPath { get; set; }
    }
}
