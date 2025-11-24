using System;
using Basket.API.Models;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        return new StoreBasketResult("");
    }
}

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Shopping cart shoud not be null");
        RuleFor(x => x.Cart.Username).NotNull().WithMessage("Username is required !");
    }
}

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);