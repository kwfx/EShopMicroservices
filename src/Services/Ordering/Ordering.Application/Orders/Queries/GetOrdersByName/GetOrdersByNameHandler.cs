
namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResponse>
{
    public async Task<GetOrdersByNameResponse> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Where(o => o.OrderName.Value.Contains(request.OrderName))
            .OrderByDescending(o => o.OrderName.Value)
            .Select(o => o.ToOrderDto())
            .ToListAsync(cancellationToken);
        return new GetOrdersByNameResponse(orders);
    }
};

