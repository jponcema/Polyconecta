namespace PolyConecta.Api.Common;

/// <summary>
/// Paginated list result wrapper for API responses.
/// </summary>
/// <typeparam name="TData">Item data type</typeparam>
public class PagedResult<TData>
{
    public IReadOnlyList<TData> Items { get; set; } = Array.Empty<TData>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / (PageSize > 0 ? PageSize : 1));
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;

    public PagedResult(IReadOnlyList<TData> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
