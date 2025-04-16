import { Box, Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";
import { Movie } from "../../app/models/movie";
import StarRoundedIcon from '@mui/icons-material/StarRounded';
import { toRating } from "../../lib/utils";
import { Link } from "react-router-dom";

type Props = {
    movie: Movie
}
export default function MovieCard({ movie }: Props) {
    return (
        <Card
            sx={{
                width: 162,
                borderRadius: 2,
                display: 'flex',
                flexDirection: 'column',
            }}>
            <CardActionArea component={Link} to={movie.id}>
                <CardMedia
                    sx={{ height: 243, backgroundSize: 'cover' }}
                    image={`/images/${movie.posterPath}`}
                    title={movie.title} />
            </CardActionArea>
            <CardContent>
                <Box display='flex' flexDirection='row' alignItems='center' mb={1}>
                    <StarRoundedIcon color="secondary" />
                    <Typography
                        variant="body2"
                        sx={{ color: 'secondary.main', fontSize: 16 }}>
                        {toRating(movie.voteAverage)}
                    </Typography>
                </Box>
                <CardActionArea component={Link} to={movie.id}>
                    <Typography
                        gutterBottom
                        sx={{ fontWeight: 'bold', fontSize: 16, textDecoration: 'none', color: "primary" }}
                        variant="h5">
                        {movie.title}
                    </Typography>
                </CardActionArea>
            </CardContent>
        </Card>
    )
}