
namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, PaginationResult<OrderDto>>
{
    private readonly int DefaultMaxPageSize = 20;

    public async Task<PaginationResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var encodedCursor = request.EncodedCursor;
        GetOrdersPaginationCursor? cursor = null;
        int pageIndex = 1;
        int maxPageSize = request?.PageSize ?? DefaultMaxPageSize;
        var orderQuery = dbContext.Orders.AsQueryable();
        var totalCount = await orderQuery.LongCountAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(encodedCursor))
        {
            cursor = PaginationCursor.Decode<GetOrdersPaginationCursor>(encodedCursor);
            pageIndex = cursor.PageIndex;
            orderQuery = orderQuery.Where(
                o => o.OrderName.Value.CompareTo(cursor.OrderName) < 0
                    || o.OrderName.Value == cursor.OrderName && o.Id.Value <= cursor.Id
                );
        }

        var resultOrders = await orderQuery
            .OrderByDescending(o => o.OrderName.Value)
                .ThenByDescending(o => o.Id.Value)
            .AsNoTracking()
            .Take(maxPageSize + 1)
            .Select(o => o.ToOrderDto())
            .ToListAsync(cancellationToken);

        string? nextPage = null;
        if (resultOrders.Count > maxPageSize)
        {
            var lastOrder = resultOrders.Last();
            resultOrders.RemoveAt(-1);
            var nextCursor = new GetOrdersPaginationCursor() { Id = (Guid)lastOrder.Id!, OrderName = lastOrder.OrderName, PageIndex = pageIndex };
            nextPage = nextCursor.Encode();
        }
        else
        {
            pageIndex = (int)Math.Ceiling((double)totalCount / maxPageSize);
        }
        var pageSize = resultOrders.Count;
        return new PaginationResult<OrderDto>(pageIndex, resultOrders, pageSize, nextPage, totalCount);
    }
};

