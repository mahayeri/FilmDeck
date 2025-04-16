import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import Movie from "../../features/movie/Movie";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import MovieDetails from "../../features/movie/MovieDetails";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'movie', element: <Movie /> },
            { path: 'movie/:id', element: <MovieDetails /> },
            { path: 'server-error', element: <ServerError /> },
            { path: 'not-found', element: <NotFound /> },
            { path: '*', element: <Navigate replace to='/not-found' /> },
        ]
    }
])