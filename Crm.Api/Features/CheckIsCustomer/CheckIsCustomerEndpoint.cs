using Crm_Api.Shared.Model;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Crm_Api.Features.CheckIsCustomer;

public class CheckIsCustomerRequest()
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
public class CheckIsCustomerEndpoint : Endpoint<CheckIsCustomerRequest, Result<bool>>
{
    private readonly IMediator _mediator;
    public CheckIsCustomerEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/customers/check");
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] CheckIsCustomerRequest req, CancellationToken ct)
    {
        var res = await _mediator.Send(new CheckIsCustomerQuery(req.FirstName, req.LastName, req.Email, req.PhoneNumber, req.DateOfBirth), ct);
        await SendAsync(res, cancellation: ct);
    }
}
