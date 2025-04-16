namespace Api.Shared.RequestHelpers;

public sealed record PaginationMetadata(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages);

