using Crm_Api.Contracts.Request;
using Crm_Api.Features.RegisterPricingAgreement;
using Crm_Api.Infrastructure;
using Crm_Api.Infrastructure.Entities;
using Crm_Api.Shared.Clients;
using FluentValidation;
using FluentValidation.Results;
using Medallion.Threading;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shared.Contracts.Services;

namespace Crm.Api.Test.Features.RegisterPricingAgreement;
public class RegisterPricingAgreementTests
{
    private readonly ApplicationDbContext _context;
    private readonly ICRMClient _crmClient;
    private readonly IEsoftLog<RegisterPricingAgreementCommandHandler> _logger;
    private readonly IValidator<RegisterPricingAgreementRequest> _validator;
    private readonly IDistributedLockProvider _lockProvider;

    public RegisterPricingAgreementTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("fake")
                .Options;
        _context = new ApplicationDbContext(options);
        _crmClient = Substitute.For<ICRMClient>();
        _logger = Substitute.For<IEsoftLog<RegisterPricingAgreementCommandHandler>>();
        _validator = Substitute.For<IValidator<RegisterPricingAgreementRequest>>();
        _lockProvider = Substitute.For<IDistributedLockProvider>();
    }

    [Fact]
    public async Task ValidData_CustomerExistedNoOfficialCustomer_ReturnsSuccess()
    {
        var cancellationToken = new CancellationToken();
        var model = TestDataGenerator.ValidModel();
        _validator.ValidateAsync(model, cancellationToken).Returns(new ValidationResult());
        var customerData = model.CustomerData;
        _crmClient.GetCustomerId(customerData.FirstName, customerData.LastName, customerData.Email, customerData.PhoneNumber, customerData.DateOfBirth)
            .Returns(1);

        var handler = new RegisterPricingAgreementCommandHandler(_context, _validator, _logger, _crmClient, _lockProvider);

        var result = await handler.Handle(new RegisterPricingAgreementCommand(model), cancellationToken);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ValidData_NoCustomerExistedNoOfficialCustomer_ReturnsSuccess()
    {
        var cancellationToken = new CancellationToken();
        var model = TestDataGenerator.ValidModel();
        _validator.ValidateAsync(model, cancellationToken).Returns(new ValidationResult());
        var customerData = model.CustomerData;
        _crmClient.CreateCustomer(new CreateCustomerRequest(customerData.FirstName, customerData.LastName, customerData.Email, customerData.PhoneNumber, customerData.DateOfBirth))
            .Returns(1);

        var handler = new RegisterPricingAgreementCommandHandler(_context, _validator, _logger, _crmClient, _lockProvider);

        var result = await handler.Handle(new RegisterPricingAgreementCommand(model), cancellationToken);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ValidData_NoCustomerExistedHasOfficialCustomer_ReturnsSuccess()
    {
        var cancellationToken = new CancellationToken();
        var model = TestDataGenerator.ValidModel();
        _validator.ValidateAsync(model, cancellationToken).Returns(new ValidationResult());
        var customerData = model.CustomerData;
        _crmClient.CreateCustomer(new CreateCustomerRequest(customerData.FirstName, customerData.LastName, customerData.Email, customerData.PhoneNumber, customerData.DateOfBirth))
            .Returns(1);

        _context.OfficialCustomers.Add(new OfficialCustomer { CustomerId = 1 });
        _context.SaveChanges();

        var handler = new RegisterPricingAgreementCommandHandler(_context, _validator, _logger, _crmClient, _lockProvider);

        var result = await handler.Handle(new RegisterPricingAgreementCommand(model), cancellationToken);

        Assert.True(result.IsSuccess);
    }
}
