namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(string EncodedCursor, int? PageSize = 20) : IQuery<PaginationResult<OrderDto>>;

public class GetOrdersPaginationCursor : PaginationCursor
{
    public Guid Id { get; set; }
    public string OrderName { get; set; } = default!;


}