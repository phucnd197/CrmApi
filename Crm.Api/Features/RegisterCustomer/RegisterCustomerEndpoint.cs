using Crm_Api.Contracts.Request;
using Crm_Api.Shared.Model;
using FastEndpoints;
using MediatR;

namespace Crm_Api.Features.RegisterCustomer;
public record RegisterCustomerWebhookRequest()
{
    [FromBody]
    public WebhookData<RegisterCustomerRequest> RequestBody { get; set; }
    [FromHeader]
    public Guid ApiKey { get; set; }
}
public class RegisterCustomerEndpoint : Endpoint<RegisterCustomerWebhookRequest, Result<bool>>
{
    private readonly IMediator _mediator;

    public RegisterCustomerEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/users/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterCustomerWebhookRequest req, CancellationToken ct)
    {
        if (req.RequestBody.Event != "register-user")
        {
            await SendAsync(Result.Success(true), cancellation: ct);
            return;
        }
        var res = await _mediator.Send(new RegisterCustomerWebhookCommand(req.ApiKey, req.RequestBody.Body), ct);
        await SendAsync(res, cancellation: ct);
    }
}