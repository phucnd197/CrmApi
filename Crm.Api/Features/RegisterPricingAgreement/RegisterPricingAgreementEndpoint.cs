using Crm_Api.Contracts.Request;
using Crm_Api.Shared.Model;
using FastEndpoints;
using MediatR;

namespace Crm_Api.Features.RegisterPricingAgreement;

public class RegisterPricingAgreementEndpoint : Endpoint<RegisterPricingAgreementRequest, Result<bool>>
{
    private readonly IMediator _mediator;

    public RegisterPricingAgreementEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users/agreements");
        AllowAnonymous();// TODO: may require authentication for this if the CRM needs authentication
    }

    public override async Task HandleAsync(RegisterPricingAgreementRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new RegisterPricingAgreementCommand(req), ct);
        await SendAsync(res, cancellation: ct);
    }
}
