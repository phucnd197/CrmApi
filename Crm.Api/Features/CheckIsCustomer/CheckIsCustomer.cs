using Crm_Api.Infrastructure;
using Crm_Api.Shared.Clients;
using Crm_Api.Shared.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Crm_Api.Features.CheckIsCustomer;

public record CheckIsCustomerQuery(string FirstName, string LastName, string Email, string PhoneNumber, DateOnly DateOfBirth) : IRequest<Result<bool>>;
public class CheckIsCustomerQueryHandler : IRequestHandler<CheckIsCustomerQuery, Result<bool>>
{
    private readonly ApplicationDbContext _context;
    private readonly ICRMClient _crmClient;

    public CheckIsCustomerQueryHandler(ApplicationDbContext context, ICRMClient crmClient)
    {
        _context = context;
        _crmClient = crmClient;
    }

    public async Task<Result<bool>> Handle(CheckIsCustomerQuery request, CancellationToken cancellationToken)
    {
        var customerId = await _crmClient.GetCustomerId(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.DateOfBirth);
        if (customerId == null)
        {
            return Result.Fail<bool>("Customer does not exist in CRM", 404);
        }

        return Result.Success(await _context.OfficialCustomers.AnyAsync(x => x.CustomerId == customerId));
    }
}
