namespace Api.Shared.RequestHelpers;

public class PagedList<T> : List<T>
{
    public PaginationMetadata Metadata { get; set; }
    public PagedList(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Metadata = new(
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalCount: totalCount,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize));

        AddRange(items);
    }
}