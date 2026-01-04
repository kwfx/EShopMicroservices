namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public OrderName OrderName { get; private set; } = default!;
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Draft;
    public Address DeliveryAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public decimal TotalPrice
    {
        get
        {
            return OrderItems.Sum(item => item.TotalPrice);
        }
        set { }
    }

    public static Order Create(OrderId id, OrderName orderName, CustomerId customerId, Address deliveryAddress, Address billingAddress, Payment payment)
    {
        var order = new Order()
        {
            Id = id,
            OrderName = orderName,
            CustomerId = customerId,
            DeliveryAddress = deliveryAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(OrderName orderName, Address deliveryAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        OrderName = orderName;
        DeliveryAddress = deliveryAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;
        this.AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void AddOrderItem(ProductId productId, decimal unitPrice, decimal quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(unitPrice);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        _orderItems.Add(new(Id, productId, unitPrice, quantity));
    }

    public void RemoveOrderItem(ProductId productId)
    {
        var toRemove = _orderItems.FirstOrDefault(item => item.ProductId == productId);
        if (toRemove is not null)
        {
            _orderItems.Remove(toRemove);
        }
    }

    public void RemoveOrderItem(OrderItemId id)
    {
        var toDelete = _orderItems.FirstOrDefault(item => item.Id.Value == id.Value);
        if (toDelete != null)
        {
            _orderItems.Remove(toDelete);
        }
    }
}