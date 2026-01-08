namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid? Id,
    List<OrderItemDto> OrderItems,
    string OrderName,
    Guid CustomerId,
    OrderStatus Status,
    AddressDto DeliveryAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    decimal TotalPrice
);