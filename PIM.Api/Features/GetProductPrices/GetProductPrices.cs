using MediatR;
using Microsoft.EntityFrameworkCore;
using PIM_Api.Infrastructure;
using PIM_Api.Infrastructure.Entities;

namespace PIM_Api.Features.GetProductPrices;

public record GetProductPricesQuery(int Id) : IRequest<IList<Price>>;
public class GetProductPricesQueryHandler : IRequestHandler<GetProductPricesQuery, IList<Price>>
{
    private readonly ApplicationDbContext _context;

    public GetProductPricesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Price>> Handle(GetProductPricesQuery request, CancellationToken cancellationToken)
    {
        var now = DateOnly.FromDateTime(DateTime.Today);
        return await _context.Prices.AsNoTracking()
            .Where(x => x.ProductId == request.Id && x.EffectiveDate <= now && x.ExpirationDate >= now)
            .ToListAsync(cancellationToken);
    }
}

