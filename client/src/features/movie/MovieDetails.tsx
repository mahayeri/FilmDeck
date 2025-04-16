import { useParams } from "react-router-dom"
import { useFetchMovieDetailsQueryQuery } from "./movieApi";
import { Box, Grid2, Link, Typography } from "@mui/material";

export default function MovieDetails() {
    const { id } = useParams();
    const { data: movie, isLoading } = useFetchMovieDetailsQueryQuery(id ? id : '');

    if (isLoading || !movie) return <div>Loading...</div>

    return (
        <Grid2 spacing={0} p={0}>
            <Box sx={{
                backgroundImage: `url(/images/${movie.backdropPath})`,
                borderBottom: '1px solid var(--primaryColor)',
                backgroundPosition: 'left calc((50vw - 170px) - 340px) top',
                backgroundSize: 'cover',
                backgroundRepeat: 'no-repeat',
                width: '100%',
            }}>
                <Box sx={{
                    width: '100%',
                    padding: '5rem',
                    //padding: '5rem',
                    //display: 'flex',
                    //flexWrap: 'nowrap',
                    backgroundImage: ' linear-gradient(to right, rgba(31.5, 10.5, 10.5, 1) calc((50vw - 170px) - 340px), rgba(31.5, 10.5, 10.5, 0.84) 50%, rgba(31.5, 10.5, 10.5, 0.84) 100%)'
                }}>
                    <Grid2 container spacing={2}>
                        <Grid2 size={4} display='flex' flexDirection='column' alignContent='start' alignItems='center'>
                            <img src={`/images/${movie.posterPath}`} alt={movie.title}
                                style={{ maxWidth: '300px', borderRadius: '10px' }} />
                        </Grid2>
                        <Grid2 size={8} display='flex' flexDirection='column' alignItems='flex-start' alignContent='center'>
                            <Typography
                                variant="h2"
                                sx={{ fontSize: '2rem', fontWeight: '600' }}
                            >
                                {movie.title} ({movie.releaseDate.slice(0, 4)})
                            </Typography>
                            <Box sx={{ display: 'flex', flexGrow: 'row' }}>
                                {movie.genres.split(',').map((genre, index, array) => (
                                    <Link underline="hover">{genre}{index < array.length - 1 && ', '}</Link>
                                ))}
                            </Box>
                        </Grid2>
                    </Grid2>
                </Box>
            </Box>
        </Grid2>
    )
}