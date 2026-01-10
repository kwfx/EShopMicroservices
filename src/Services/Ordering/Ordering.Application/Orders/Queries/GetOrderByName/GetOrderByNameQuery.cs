namespace Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrdersByNameResponse>;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Order);

