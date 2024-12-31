namespace Crm_Api.Contracts.Response;

public record PriceResponse(
    int Id,
    decimal BasePrice,
    string Currency,
    DateOnly EffectiveDate,
    DateOnly ExpirationDate);