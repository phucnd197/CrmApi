namespace User_Api.Contracts.Response;

public record UserResponse(int Id, string FirstName, string LastName, string Email, string PhoneNumber, DateOnly DateOfBirth);

