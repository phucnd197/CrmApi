using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PIM_Api.Infrastructure;
using PIM_Api.Infrastructure.Entities;

namespace PIM_Api.Features.GetAllProducts;

public record GetAllProductsQuery : IRequest<IList<Product>>;
public class GetAllProducsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<Product>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public GetAllProducsQueryHandler(ApplicationDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<IList<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = "Products_List";
        if (_memoryCache.TryGetValue<IList<Product>>(cacheKey, out var cachedResult))
        {
            return cachedResult!;
        }
        var res = await _context.Products.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        if (res.Count > 0)
        {
            _memoryCache.Set(cacheKey, res);
        }
        return res;
    }
}
