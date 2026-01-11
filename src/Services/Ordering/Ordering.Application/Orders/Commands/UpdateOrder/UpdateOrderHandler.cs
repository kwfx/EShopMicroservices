namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var dbOrder = await dbContext.Orders.FindAsync(OrderId.Of((Guid)request.Order.Id!), cancellationToken);
        if (dbOrder is null)
        {
            throw new OrderNotFoundException(request.Order.Id ?? Guid.Empty);
        }
        UpdateOrder(dbOrder, request.Order);
        dbContext.Orders.Update(dbOrder);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    public void UpdateOrder(Order order, OrderDto newValues)
    {
        var billingAddress = Address.Of(
            newValues.BillingAddress.FirstName,
            newValues.BillingAddress.LastName,
            newValues.BillingAddress.Email,
            newValues.BillingAddress.AddressLine,
            newValues.BillingAddress.ZipCode,
            newValues.BillingAddress.City,
            newValues.BillingAddress.State
        );
        var deliveryAddress = Address.Of(
            newValues.DeliveryAddress.FirstName,
            newValues.DeliveryAddress.LastName,
            newValues.DeliveryAddress.Email,
            newValues.DeliveryAddress.AddressLine,
            newValues.DeliveryAddress.ZipCode,
            newValues.DeliveryAddress.City,
            newValues.DeliveryAddress.State
        );
        var payment = Payment.Of(
            newValues.Payment.CardName,
            newValues.Payment.CardNumber,
            newValues.Payment.Expiration,
            newValues.Payment.AddressLine,
            newValues.Payment.CVV,
            newValues.Payment.PaymentMethod
        );
        order.Update(
            OrderName.Of(newValues.OrderName),
            deliveryAddress,
            billingAddress,
            payment,
            status: newValues.Status
        );
        return;
    }
}