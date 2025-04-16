import { Grid2, Typography } from "@mui/material";
import { MovieParams } from "../../app/models/movieParams";
import { useFetchMoviesQueryQuery } from "./movieApi"
import MovieList from "./MovieList";

export default function Movie() {
  const moviesParams: MovieParams = {
    pageNumber: 1,
    pageSize: 20,
    sortOrder: 'asc',
    searchTerm: '',
    sortColumn: 'voteAverage',
    genre: []
  }
  const { data, isLoading } = useFetchMoviesQueryQuery(moviesParams);

  if (isLoading || !data) return <div>Loading...</div>

  return (
    <Grid2 container spacing={2} p={2}>
      <Grid2 size={3}>Filters and sort column</Grid2>
      <Grid2 size={9}>
        {data.items && data.items.length > 0
          ? (
            <>
              <MovieList movies={data.items} />
            </>
          )
          : (
            <Typography variant="h5">There are no results for this filters.</Typography>
          )}
      </Grid2>
    </Grid2>
  )
}