export type MovieParams = {
    pageNumber: number,
    pageSize: number,
    searchTerm?: string,
    sortColumn?: string,
    sortOrder: string,
    genre: string[],
}