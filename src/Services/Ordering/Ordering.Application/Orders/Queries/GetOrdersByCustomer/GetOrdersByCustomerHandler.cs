namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResponse>
{
    public async Task<GetOrdersByCustomerResponse> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
            .OrderByDescending(o => o.OrderName.Value)
            .Select(o => o.ToOrderDto())
            .ToListAsync(cancellationToken);
        return new GetOrdersByCustomerResponse(orders);
    }
};