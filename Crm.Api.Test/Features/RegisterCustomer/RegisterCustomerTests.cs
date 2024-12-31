using Crm_Api.Contracts.Request;
using Crm_Api.Features.RegisterCustomer;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Shared.Contracts.Services;

namespace Crm.Api.Test.Features.RegisterCustomer;
public class RegisterCustomerTests
{
    private readonly ICQRSClient _cqrsClient;
    private readonly IEsoftLog<RegisterCustomerCommandHandler> _logger;
    private readonly IValidator<RegisterCustomerRequest> _validator;

    public RegisterCustomerTests()
    {
        _cqrsClient = Substitute.For<ICQRSClient>();
        _logger = Substitute.For<IEsoftLog<RegisterCustomerCommandHandler>>();
        _validator = Substitute.For<IValidator<RegisterCustomerRequest>>();
    }

    [Fact]
    public async Task Handle_ValidData_ReturnsSuccess()
    {
        var cancellationToken = new CancellationToken();
        var model = TestDataGenerator.ValidModel();
        _validator.ValidateAsync(model, cancellationToken).Returns(new ValidationResult());
        var handler = new RegisterCustomerCommandHandler(_logger, _cqrsClient, _validator);

        var result = await handler.Handle(new RegisterCustomerCommand(model), cancellationToken);

        Assert.True(result.IsSuccess);
    }
}
