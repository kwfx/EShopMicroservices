namespace Ordering.Application.Dtos;

public record PaymentDto(
    string? CardName,
    string CardNumber,
    string Expiration,
    string AddressLine,
    string CVV,
    string PaymentMethod
);
