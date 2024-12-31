using Crm_Api.Contracts.Request;
using Crm_Api.Infrastructure;
using Crm_Api.Infrastructure.Entities;
using Crm_Api.Shared.Clients;
using Crm_Api.Shared.Model;
using FluentValidation;
using Medallion.Threading;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Services;

namespace Crm_Api.Features.RegisterPricingAgreement;

public record RegisterPricingAgreementCommand(RegisterPricingAgreementRequest RequestData) : IRequest<Result<bool>>;
public class RegisterPricingAgreementCommandHandler : IRequestHandler<RegisterPricingAgreementCommand, Result<bool>>
{
    private readonly ApplicationDbContext _context;
    private readonly ICRMClient _crmClient;
    private readonly IEsoftLog<RegisterPricingAgreementCommandHandler> _logger;
    private readonly IValidator<RegisterPricingAgreementRequest> _validator;
    private readonly IDistributedLockProvider _lockProvider;

    public RegisterPricingAgreementCommandHandler(ApplicationDbContext context, IValidator<RegisterPricingAgreementRequest> validator, IEsoftLog<RegisterPricingAgreementCommandHandler> logger, ICRMClient crmClient, IDistributedLockProvider lockProvider)
    {
        _context = context;
        _validator = validator;
        _logger = logger;
        _crmClient = crmClient;
        _lockProvider = lockProvider;
    }

    public async Task<Result<bool>> Handle(RegisterPricingAgreementCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validateResult = await _validator.ValidateAsync(request.RequestData, cancellationToken);
            if (!validateResult.IsValid)
            {
                return Result.Fail<bool>(validateResult);
            }

            var customerData = request.RequestData.CustomerData;
            var customerId = await _crmClient.GetCustomerId(customerData.FirstName, customerData.LastName, customerData.Email, customerData.PhoneNumber, customerData.DateOfBirth);
            if (customerId is null)
            {
                customerId = await _crmClient.CreateCustomer(new CreateCustomerRequest(customerData.FirstName, customerData.LastName, customerData.Email, customerData.PhoneNumber, customerData.DateOfBirth));
            }

            await _crmClient.RegisterAgreement(customerId.Value, new AgreementRegistrationData(request.RequestData.ProductId, request.RequestData.Pricing));
            await SaveOfficialCustomerAsync(customerId.Value, cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when registering price for customer: {error}", ex.Message);
            return Result.Fail<bool>("Something went wrong, please try again.");
        }
    }

    private async Task SaveOfficialCustomerAsync(int customerId, CancellationToken cancellationToken)
    {
        // lock to prevent data being added twice for the same customer
        await using var @lock = await _lockProvider.AcquireLockAsync("OfficialCustomer", cancellationToken: cancellationToken);
        if (!await _context.OfficialCustomers
                .AsNoTracking()
                .AnyAsync(x => x.CustomerId == customerId, cancellationToken: cancellationToken))
        {
            await _context.OfficialCustomers.AddAsync(new OfficialCustomer { CustomerId = customerId }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
