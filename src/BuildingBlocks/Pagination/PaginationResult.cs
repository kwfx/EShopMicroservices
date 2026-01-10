namespace BuildingBlocks.Pagination;

public class PaginationResult<T>(int pageIndex, IEnumerable<T> items, int pageSize, string? nextPage, long count) where T : class
{
    public IEnumerable<T> Items { get; } = items;
    public int PageSize { get; } = pageSize;
    public string? NextPage { get; } = nextPage;
    public int PageIndex { get; } = pageIndex;
    public long Count { get; } = count;
}