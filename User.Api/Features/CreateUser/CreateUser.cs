using MediatR;
using Shared.Contracts.Services;
using User_Api.Contracts.Response;
using User_Api.Infrastructure;
using User_Api.Infrastructure.Entities;
using User_Api.Shared;

namespace User_Api.Features.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string PhoneNumber, DateOnly DateOfBirth, string Password) : IRequest<UserResponse>;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IEsoftLog<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(ApplicationDbContext context, IEsoftLog<CreateUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var hashedPassword = Helper.HashPassword(request.Password, out var salt);
            var user = new InternalUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                HashedPassword = hashedPassword,
                Salt = salt,
            };

            _context.InternalUsers.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.DateOfBirth);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when creating user: {error}", ex.Message);
            return null;
        }
    }
}
