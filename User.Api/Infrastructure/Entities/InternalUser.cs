namespace User_Api.Infrastructure.Entities;

public class InternalUser
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string HashedPassword { get; set; }
    public byte[] Salt { get; set; }
}
