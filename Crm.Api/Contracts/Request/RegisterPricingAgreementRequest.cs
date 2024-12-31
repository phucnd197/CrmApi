namespace Crm_Api.Contracts.Request;

public record RegisterPricingAgreementRequest(RegisterCustomerRequest CustomerData, int ProductId, decimal Pricing);
