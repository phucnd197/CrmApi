using Crm_Api.Contracts.Request;
using Crm_Api.Features.RegisterCustomer;
using Crm_Api.Shared.Clients;
using FluentValidation;
using FluentValidation.Results;

namespace Crm_Api.Features.RegisterPricingAgreement;

public class RegisterPricingAgreementRequestValidator : AbstractValidator<RegisterPricingAgreementRequest>
{
    public RegisterPricingAgreementRequestValidator(IPIMClient _pimClient)
    {
        RuleFor(x => x)
            .CustomAsync(async (model, context, cancellationToken) =>
            {
                var productPricings = await _pimClient.GetProductPrice(model.ProductId, cancellationToken);
                if (productPricings is null || productPricings.Count == 0 || !productPricings.Any(x => x.BasePrice == model.Pricing))
                {
                    context.AddFailure(new ValidationFailure(nameof(model.ProductId), "Product or price does not exist."));
                }
            });

        RuleFor(x => x.CustomerData)
            .SetValidator(new RegisterCustomerRequestValidator());
    }
}
