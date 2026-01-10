namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResponse>;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

