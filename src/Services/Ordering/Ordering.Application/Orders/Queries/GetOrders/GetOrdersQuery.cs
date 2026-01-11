namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(int PageIndex, int? PageSize = 20) : IQuery<PaginationResult<OrderDto>>;
