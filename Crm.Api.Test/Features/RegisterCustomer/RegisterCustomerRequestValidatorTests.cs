using Crm.Api.Test.TestCommon;
using Crm_Api.Contracts.Request;
using Crm_Api.Features.RegisterCustomer;

namespace Crm.Api.Test.Features.RegisterCustomer;
internal static class TestDataGenerator
{
    public static RegisterCustomerRequest ValidModel()
    {
        return new RegisterCustomerRequest("Test", "Test", "test@gmail.com", "0397893330", DateOnly.ParseExact("18/05/1997", "dd/MM/yyyy"));
    }

    public static IEnumerable<object[]> InvalidEmail()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { Email = "" },
            nameof(RegisterCustomerRequest.Email)
        };
        yield return new object[]
        {
            model with { Email = null },
            nameof(RegisterCustomerRequest.Email)
        };
        yield return new object[]
        {
            model with { Email = "fail" },
            nameof(RegisterCustomerRequest.Email)
        };
    }

    public static IEnumerable<object[]> InvalidPhoneNumber()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { PhoneNumber = "" },
            nameof(RegisterCustomerRequest.PhoneNumber)
        };
        yield return new object[]
        {
            model with { PhoneNumber = null },
            nameof(RegisterCustomerRequest.PhoneNumber)
        };
        yield return new object[]
        {
            model with { PhoneNumber = "1" },
            nameof(RegisterCustomerRequest.PhoneNumber)
        };
        yield return new object[]
        {
            model with { PhoneNumber = "sdfsef" },
            nameof(RegisterCustomerRequest.PhoneNumber)
        };
    }

    public static IEnumerable<object[]> InvalidFirstName()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { FirstName = "" },
            nameof(RegisterCustomerRequest.FirstName)
        };
        yield return new object[]
        {
            model with { FirstName = null },
            nameof(RegisterCustomerRequest.FirstName)
        };
        yield return new object[]
        {
            model with { FirstName = "<script></script>" },
            nameof(RegisterCustomerRequest.FirstName)
        };
        yield return new object[]
        {
            model with { FirstName = Helper.RandomLetterOnlyString(151) },
            nameof(RegisterCustomerRequest.FirstName)
        };
    }

    public static IEnumerable<object[]> InvalidLastName()
    {
        var model = ValidModel();
        yield return new object[]
        {
            model with { LastName = "" },
            nameof(RegisterCustomerRequest.LastName)
        };
        yield return new object[]
        {
            model with { LastName = null },
            nameof(RegisterCustomerRequest.LastName)
        };
        yield return new object[]
        {
            model with { LastName = "<script></script>" },
            nameof(RegisterCustomerRequest.LastName)
        };
        yield return new object[]
        {
            model with { LastName = Helper.RandomLetterOnlyString(201) },
            nameof(RegisterCustomerRequest.LastName)
        };
    }
}
public class RegisterCustomerRequestValidatorTests
{
    [Fact]
    public async Task Validate_ValidModel_ReturnsValidResult()
    {
        var validModel = TestDataGenerator.ValidModel();
        var validator = new RegisterCustomerRequestValidator();

        var validateResult = await validator.ValidateAsync(validModel);

        Assert.True(validateResult.IsValid);
    }


    [Theory]
    [MemberData(nameof(TestDataGenerator.InvalidEmail), MemberType = typeof(TestDataGenerator))]
    [MemberData(nameof(TestDataGenerator.InvalidPhoneNumber), MemberType = typeof(TestDataGenerator))]
    [MemberData(nameof(TestDataGenerator.InvalidFirstName), MemberType = typeof(TestDataGenerator))]
    [MemberData(nameof(TestDataGenerator.InvalidLastName), MemberType = typeof(TestDataGenerator))]
    public async Task Validate_InvalidModel_ReturnsValidationError(RegisterCustomerRequest request, string invalidField)
    {
        var validator = new RegisterCustomerRequestValidator();

        var validateResult = await validator.ValidateAsync(request);

        Assert.False(validateResult.IsValid);
        Assert.Contains(validateResult.Errors, e => e.PropertyName == invalidField);
    }
}
