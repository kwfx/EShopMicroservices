namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order shoud have at least one order item");
        RuleFor(x => x.Order.BillingAddress).NotNull().WithMessage("Order billing address should be specified");
        RuleFor(x => x.Order.DeliveryAddress).NotNull().WithMessage("Order delivery address should be specified");
        RuleFor(x => x.Order.Payment).NotNull().WithMessage("Order payment should be specified");
    }
}