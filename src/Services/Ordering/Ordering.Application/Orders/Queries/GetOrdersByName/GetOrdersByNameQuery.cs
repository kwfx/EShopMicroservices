namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrdersByNameResponse>;

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

