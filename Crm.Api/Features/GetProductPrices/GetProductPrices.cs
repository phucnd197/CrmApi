using Crm_Api.Contracts.Response;
using Crm_Api.Shared.Clients;
using Crm_Api.Shared.Model;
using MediatR;

namespace Crm_Api.Features.GetProductPrices;

public record GetProductPricesQuery(int ProductId) : IRequest<Result<IList<PriceResponse>>>;
public class GetProductPricesQueryHandler : IRequestHandler<GetProductPricesQuery, Result<IList<PriceResponse>>>
{
    private readonly IPIMClient _client;

    public async Task<Result<IList<PriceResponse>>> Handle(GetProductPricesQuery request, CancellationToken cancellationToken)
    {
        return Result.Success<IList<PriceResponse>>(await _client.GetProductPrice(request.ProductId, cancellationToken));
    }
}

