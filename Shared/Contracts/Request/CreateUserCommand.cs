namespace Shared.Contracts.Request;
public record CreateUserCommand(string FirstName, string LastName, string Email, string PhoneNumber, DateOnly DateOfBirth, string Password);
