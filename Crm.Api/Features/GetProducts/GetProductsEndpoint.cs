using Crm_Api.Contracts.Response;
using Crm_Api.Shared.Model;
using FastEndpoints;
using MediatR;

namespace Crm_Api.Features.GetProducts;

public class GetProductsEndpoint : EndpointWithoutRequest<Result<IList<ProductResponse>>>
{
    private readonly IMediator _mediator;

    public GetProductsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await _mediator.Send(new GetProductsQuery(), ct);
        await SendAsync(res, cancellation: ct);
    }
}