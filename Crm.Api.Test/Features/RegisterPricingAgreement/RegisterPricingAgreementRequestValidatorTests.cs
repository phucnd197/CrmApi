using Crm.Api.Test.TestCommon;
using Crm_Api.Contracts.Request;
using Crm_Api.Contracts.Response;
using Crm_Api.Features.RegisterPricingAgreement;
using Crm_Api.Shared.Clients;
using NSubstitute;

namespace Crm.Api.Test.Features.RegisterPricingAgreement;
internal static class TestDataGenerator
{
    public static RegisterPricingAgreementRequest ValidModel()
    {
        return new RegisterPricingAgreementRequest(new RegisterCustomerRequest("Test", "Test", "test@gmail.com", "0397893330", DateOnly.ParseExact("18/05/1997", "dd/MM/yyyy")), 1, 100);
    }

    public static IEnumerable<object[]> InvalidEmail()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { Email = "" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.Email)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { Email = null } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.Email)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { Email = "fail" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.Email)
        };
    }

    public static IEnumerable<object[]> InvalidPhoneNumber()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { PhoneNumber = "" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.PhoneNumber)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { PhoneNumber = null } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.PhoneNumber)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { PhoneNumber = "1" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.PhoneNumber)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { PhoneNumber = "sdfsef" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.PhoneNumber)
        };
    }

    public static IEnumerable<object[]> InvalidFirstName()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { FirstName = "" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.FirstName)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { FirstName = null } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.FirstName)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { FirstName = "<script></script>" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.FirstName)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { FirstName = Helper.RandomLetterOnlyString(151) } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.FirstName)
        };
    }

    public static IEnumerable<object[]> InvalidLastName()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { LastName = "" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.LastName)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { LastName = null } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.LastName)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { LastName = "<script></script>" } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.LastName)
        };
        yield return new object[]
        {
            model with { CustomerData = model.CustomerData with { LastName = Helper.RandomLetterOnlyString(201) } },
            nameof(RegisterPricingAgreementRequest.CustomerData)+"."+nameof(RegisterPricingAgreementRequest.CustomerData.LastName)
        };
    }
}
public class RegisterPricingAgreementRequestValidatorTests
{
    private readonly IPIMClient _client;
    public RegisterPricingAgreementRequestValidatorTests()
    {
        _client = Substitute.For<IPIMClient>();
    }

    [Fact]
    public async Task Validate_ValidModel_ReturnsValidResult()
    {
        var model = TestDataGenerator.ValidModel();
        var cancellationToken = new CancellationToken();
        _client.GetProductPrice(1, cancellationToken).Returns(new[]
        {
            new PriceResponse(1, 100, "USD", DateOnly.FromDateTime(DateTime.UtcNow), DateOnly.FromDateTime(DateTime.UtcNow.AddYears(2))),
        });
        var validator = new RegisterPricingAgreementRequestValidator(_client);

        var validateResult = await validator.ValidateAsync(model, cancellationToken);

        Assert.True(validateResult.IsValid);
    }

    [Fact]
    public async Task Validate_InvalidProductId_ReturnsValidResult()
    {
        var model = TestDataGenerator.ValidModel();
        var cancellationToken = new CancellationToken();
        _client.GetProductPrice(1, cancellationToken).Returns(Array.Empty<PriceResponse>());
        var validator = new RegisterPricingAgreementRequestValidator(_client);

        var validateResult = await validator.ValidateAsync(model, cancellationToken);

        Assert.False(validateResult.IsValid);
    }


    [Theory]
    [MemberData(nameof(TestDataGenerator.InvalidEmail), MemberType = typeof(TestDataGenerator))]
    [MemberData(nameof(TestDataGenerator.InvalidPhoneNumber), MemberType = typeof(TestDataGenerator))]
    [MemberData(nameof(TestDataGenerator.InvalidFirstName), MemberType = typeof(TestDataGenerator))]
    [MemberData(nameof(TestDataGenerator.InvalidLastName), MemberType = typeof(TestDataGenerator))]
    public async Task Validate_InvalidModel_ReturnsValidationError(RegisterPricingAgreementRequest request, string invalidField)
    {
        var validator = new RegisterPricingAgreementRequestValidator(_client);
        var cancellationToken = new CancellationToken();
        _client.GetProductPrice(1, cancellationToken).Returns(new[]
        {
            new PriceResponse(1, 100, "USD", DateOnly.FromDateTime(DateTime.UtcNow), DateOnly.FromDateTime(DateTime.UtcNow.AddYears(2))),
        });
        var validateResult = await validator.ValidateAsync(request, cancellationToken);

        Assert.False(validateResult.IsValid);
        Assert.Contains(validateResult.Errors, e => e.PropertyName == invalidField);
    }
}
