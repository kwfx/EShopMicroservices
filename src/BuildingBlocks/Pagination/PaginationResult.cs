namespace BuildingBlocks.Pagination;

public class PaginationResult<T>(int pageIndex, IEnumerable<T> items, int pageSize, int pageCount, long count) where T : class
{
    public IEnumerable<T> Items { get; } = items;
    public int PageSize { get; } = pageSize;
    public int PageIndex { get; } = pageIndex;
    public int PageCount { get; } = pageCount;
    public long Count { get; } = count;
}