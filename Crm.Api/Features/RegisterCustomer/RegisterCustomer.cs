using Crm_Api.Contracts.Request;
using Crm_Api.Shared.Model;
using FluentValidation;
using MediatR;
using Shared.Contracts.Request;
using Shared.Contracts.Services;

namespace Crm_Api.Features.RegisterCustomer;

public record RegisterCustomerCommand(RegisterCustomerRequest RegisterData) : IRequest<Result<bool>>;
public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Result<bool>>
{
    private readonly ICQRSClient _cqrsClient;
    private readonly IEsoftLog<RegisterCustomerCommandHandler> _logger;
    private readonly IValidator<RegisterCustomerRequest> _validator;

    public RegisterCustomerCommandHandler(IEsoftLog<RegisterCustomerCommandHandler> logger, ICQRSClient cqrsClient, IValidator<RegisterCustomerRequest> validator)
    {
        _logger = logger;
        _cqrsClient = cqrsClient;
        _validator = validator;
    }

    public async Task<Result<bool>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validateResult = await _validator.ValidateAsync(request.RegisterData, cancellationToken);
            if (!validateResult.IsValid)
            {
                return Result.Fail<bool>(validateResult);
            }

            var reqData = request.RegisterData;
            await _cqrsClient.Publish(new CreateUserCommand(reqData.FirstName, reqData.LastName, reqData.Email, reqData.PhoneNumber, reqData.DateOfBirth, "Default"), cancellationToken);
            return Result.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when registering customer: {error}", ex.Message);
            return Result.Fail<bool>("Failed registering customer.");
        }
    }
}