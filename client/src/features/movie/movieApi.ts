import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWithErrorHandling } from "../../app/api/baseApi";
import { Movie } from "../../app/models/movie";
import { Pagination } from "../../app/models/pagination";
import { MovieParams } from "../../app/models/movieParams";
import { MovieDetails } from "../../app/models/movieDetails";

export const movieApi = createApi({
    reducerPath: 'movieApi',
    baseQuery: baseQueryWithErrorHandling,
    endpoints: (builder) => ({
        fetchMoviesQuery: builder.query<{ items: Movie[], pagination: Pagination }, MovieParams>({
            query: (movieParams) => {
                return {
                    url: 'movies',
                    params: movieParams
                }
            },
            transformResponse: (items: Movie[], meta) => {
                const paginationHeader = meta?.response?.headers.get('Pagination');
                const pagination = paginationHeader ? JSON.parse(paginationHeader) : null;
                return { items, pagination };
            }
        }),
        fetchMovieDetailsQuery: builder.query<MovieDetails, string>({
            query: (movieId) => `movies/${movieId}`
        })
    })
})

export const { useFetchMoviesQueryQuery, useFetchMovieDetailsQueryQuery } = movieApi;