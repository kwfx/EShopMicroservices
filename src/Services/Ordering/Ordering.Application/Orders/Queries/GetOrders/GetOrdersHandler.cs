
namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, PaginationResult<OrderDto>>
{
    private readonly int DefaultPageSize = 20;

    public async Task<PaginationResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        int pageSize = request.PageSize ?? DefaultPageSize;
        var orderQuery = dbContext.Orders.AsQueryable();
        var totalCount = await orderQuery.LongCountAsync(cancellationToken);
        var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
        var resultOrders = await orderQuery
            .OrderByDescending(o => o.OrderName.Value)
                .ThenByDescending(o => o.Id.Value)
            .AsNoTracking()
            .Skip(pageSize * (request.PageIndex - 1))
            .Take(pageSize)
            .Select(o => o.ToOrderDto())
            .ToListAsync(cancellationToken);

        return new PaginationResult<OrderDto>(request.PageIndex, resultOrders, pageSize, pageCount, totalCount);
    }
};

