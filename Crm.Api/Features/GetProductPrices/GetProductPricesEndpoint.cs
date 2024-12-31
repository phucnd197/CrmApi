using Crm_Api.Contracts.Response;
using Crm_Api.Shared.Model;
using FastEndpoints;
using MediatR;

namespace Crm_Api.Features.GetProductPrices;

public class GetProductPricesEndpoint : EndpointWithoutRequest<Result<IList<PriceResponse>>>
{
    private readonly IMediator _mediator;

    public GetProductPricesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/products/{id}/prices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await _mediator.Send(new GetProductPricesQuery(Route<int>("id")), ct);
        await SendAsync(res, cancellation: ct);
    }
}
