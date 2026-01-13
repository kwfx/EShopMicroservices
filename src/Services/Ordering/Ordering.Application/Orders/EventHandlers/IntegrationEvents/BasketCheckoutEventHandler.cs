namespace Ordering.Application.Orders.EventHandlers.IntegrationEvents;

public class BasketCheckoutEventHandler(ISender sender) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var values = context.Message;
        var address = new AddressDto(
            values.FirstName, values.LastName,
            values.EmailAddress, values.AddressLine,
            values.ZipCode,
            values.ZipCode,
            values.State
        );
        var payment = new PaymentDto(values.CardName,
            values.CardNumber,
            values.Expiration,
            values.AddressLine,
            values.CVV,
            values.PaymentMethod
        );
        var createOrderCommand = new CreateOrderCommand(new OrderDto(
            Guid.NewGuid(),
            [],
            $"order {values.UserName}",
            values.CustomerId,
            OrderStatus.Pending,
            address,
            address,
            payment,
            values.TotalPrice,
            DateTime.UtcNow,
            DateTime.UtcNow,
            values.UserName, values.UserName));
        await sender.Send(createOrderCommand);
    }
}

