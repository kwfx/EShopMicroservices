namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this List<Order> orders)
    {
        return orders.Select(ToOrderDto);
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto(
                Id: order.Id.Value,
                OrderName: order.OrderName.Value,
                CustomerId: order.CustomerId.Value,
                Status: order.Status,
                Payment: new PaymentDto(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.AddressLine, order.Payment.CVV, order.Payment.PaymentMethod),
                BillingAddress: new AddressDto(
                    order.BillingAddress.FirstName, order.BillingAddress.LastName,
                    order.BillingAddress.Email, order.BillingAddress.AddressLine,
                    order.BillingAddress.ZipCode, order.BillingAddress.City, order.BillingAddress.State
                ),
                DeliveryAddress: new AddressDto(
                    order.DeliveryAddress.FirstName, order.DeliveryAddress.LastName,
                    order.DeliveryAddress.Email, order.DeliveryAddress.AddressLine,
                    order.DeliveryAddress.ZipCode, order.DeliveryAddress.City, order.DeliveryAddress.State
                ),
                TotalPrice: order.TotalPrice,
                OrderItems: order.OrderItems.Select(item => new OrderItemDto(item.OrderId.Value, item.ProductId.Value, item.UnitPrice, item.Quantity)),
                CreatedAt: order.CreatedAt,
                LastModified: order.LastModified,
                CreatedBy: order.CreatedBy,
                LastModifiedBy: order.LastModifiedBy
            );
    }
}