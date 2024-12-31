using Crm_Api.Contracts.Request;
using Crm_Api.Infrastructure;
using Crm_Api.Shared.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts.Services;

namespace Crm_Api.Features.RegisterCustomer;

public record RegisterCustomerWebhookCommand(Guid ApiKey, RegisterCustomerRequest RegisterData) : IRequest<Result<bool>>;
public class RegisterCustomerWebhookCommandHandler : IRequestHandler<RegisterCustomerWebhookCommand, Result<bool>>
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;
    private readonly IEsoftLog<RegisterCustomerCommandHandler> _logger;

    public RegisterCustomerWebhookCommandHandler(
        IEsoftLog<RegisterCustomerCommandHandler> logger,
        ApplicationDbContext context,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _context = context;
    }

    public async Task<Result<bool>> Handle(RegisterCustomerWebhookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ApiKey == Guid.Empty || await _context.ApiKeys.AnyAsync(x => x.Id != request.ApiKey, cancellationToken))
            {
                return Result.Fail<bool>("Authorization failed", 403);
            }

            return await _mediator.Send(new RegisterCustomerCommand(request.RegisterData), cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when registering customer: {error}", ex.Message);
            return Result.Fail<bool>("Failed registering customer.");
        }
    }
}