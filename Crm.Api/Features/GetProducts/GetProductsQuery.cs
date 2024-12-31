using Crm_Api.Contracts.Response;
using Crm_Api.Shared.Clients;
using Crm_Api.Shared.Model;
using MediatR;

namespace Crm_Api.Features.GetProducts;

public record GetProductsQuery : IRequest<Result<IList<ProductResponse>>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IList<ProductResponse>>>
{
    private readonly IPIMClient _client;

    public GetProductsQueryHandler(IPIMClient client)
    {
        _client = client;
    }

    public async Task<Result<IList<ProductResponse>>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        return Result.Success(await _client.GetAllProducts(cancellationToken));
    }
}