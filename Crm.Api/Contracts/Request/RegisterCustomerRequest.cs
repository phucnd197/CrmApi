namespace Crm_Api.Contracts.Request;

public record Document(string Name, byte[]? Contents, string? Url);
public record RegisterCustomerRequest(string FirstName, string LastName, string Email, string PhoneNumber, DateOnly DateOfBirth);
