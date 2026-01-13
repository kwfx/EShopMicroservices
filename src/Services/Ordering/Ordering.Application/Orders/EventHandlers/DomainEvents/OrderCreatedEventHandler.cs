using Microsoft.Extensions.Logging;

namespace Ordering.Application.Orders.EventHandlers.DomainEvents;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("New Order has been created, Id = {OrderId}", notification.Order.Id);
        return;
    }
}