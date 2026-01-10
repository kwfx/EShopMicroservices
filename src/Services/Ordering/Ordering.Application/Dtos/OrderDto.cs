namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid? Id,
    IEnumerable<OrderItemDto> OrderItems,
    string OrderName,
    Guid CustomerId,
    OrderStatus Status,
    AddressDto DeliveryAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    decimal TotalPrice,
    DateTime CreatedAt,
    DateTime LastModified,
    string? CreatedBy,
    string? LastModifiedBy
);