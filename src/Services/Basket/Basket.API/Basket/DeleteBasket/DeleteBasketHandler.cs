using FluentValidation;

namespace Basket.API.Basket.DeleteBasket;

public class DeleteBasketHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("You have to specify the username");
    }
}

public record DeleteBasketResult(bool IsSuccess);