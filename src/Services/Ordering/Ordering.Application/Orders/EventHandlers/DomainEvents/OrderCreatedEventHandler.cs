namespace Ordering.Application.Orders.EventHandlers.DomainEvents;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint, IFeatureManager featureManager) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("New Order has been created, Id = {OrderId}", notification.Order.Id);
        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            await publishEndpoint.Publish(notification.Order.ToOrderDto(), cancellationToken);
        }
        return;
    }
}