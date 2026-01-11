namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var newOrder = CreateOrder(request.Order);
        dbContext.Orders.Add(newOrder);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(newOrder.Id.Value);
    }

    public static Order CreateOrder(OrderDto order)
    {
        var billingAddress = Address.Of(
            order.BillingAddress.FirstName,
            order.BillingAddress.LastName,
            order.BillingAddress.Email,
            order.BillingAddress.AddressLine,
            order.BillingAddress.ZipCode,
            order.BillingAddress.City,
            order.BillingAddress.State
        );
        var deliveryAddress = Address.Of(
            order.DeliveryAddress.FirstName,
            order.DeliveryAddress.LastName,
            order.DeliveryAddress.Email,
            order.DeliveryAddress.AddressLine,
            order.DeliveryAddress.ZipCode,
            order.DeliveryAddress.City,
            order.DeliveryAddress.State
        );
        var payment = Payment.Of(
            order.Payment.CardName,
            order.Payment.CardNumber,
            order.Payment.Expiration,
            order.Payment.AddressLine,
            order.Payment.CVV,
            order.Payment.PaymentMethod
        );
        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            orderName: OrderName.Of(order.OrderName),
            customerId: CustomerId.Of(order.CustomerId),
            deliveryAddress: deliveryAddress,
            billingAddress: billingAddress,
            payment: payment
        );
        foreach (var orderItem in order.OrderItems)
        {
            newOrder.AddOrderItem(ProductId.Of(orderItem.ProductId), orderItem.UnitPrice, orderItem.Quantity);
        }
        return newOrder;
    }
}