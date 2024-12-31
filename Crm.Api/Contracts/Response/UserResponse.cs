namespace Shared.Contracts.Response;

public record UserResponse(string FirstName, string LastName, string Email, string PhoneNumber, DateOnly DateOfBirth);