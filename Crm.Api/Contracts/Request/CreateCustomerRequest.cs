namespace Crm_Api.Contracts.Request;

public record CreateCustomerRequest(string FirstName, string LastName, string Email, string PhoneNumber, DateOnly DateOfBirth);
