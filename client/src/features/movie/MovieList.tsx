import { Grid2 } from "@mui/material";
import { Movie } from "../../app/models/movie";
import MovieCard from "./MovieCard";

type Props = {
    movies: Movie[]
}

export default function MovieList({ movies }: Props) {
    return (
        <Grid2 container spacing={2} columns={10}>
            {movies.map(movie => (
                <Grid2 size={2} display="flex" key={movie.id}>
                    <MovieCard movie={movie} />
                </Grid2>
            ))}
        </Grid2>
    )
}