using MediatR;
using Microsoft.EntityFrameworkCore;
using User_Api.Contracts.Response;
using User_Api.Infrastructure;

namespace User_Api.Features.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<UserResponse?>;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
{
    private readonly ApplicationDbContext _context;

    public GetUserByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.InternalUsers
            .AsNoTracking()
            .Select(x => new UserResponse(x.Id, x.FirstName, x.LastName, x.Email, x.PhoneNumber, x.DateOfBirth))
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    }
}
