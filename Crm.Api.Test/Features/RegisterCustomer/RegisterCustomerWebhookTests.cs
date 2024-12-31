using Crm_Api.Features.RegisterCustomer;
using Crm_Api.Infrastructure;
using Crm_Api.Infrastructure.Entities;
using Crm_Api.Shared.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shared.Contracts.Services;

namespace Crm.Api.Test.Features.RegisterCustomer;
public class RegisterCustomerWebhookTests
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;
    private readonly IEsoftLog<RegisterCustomerCommandHandler> _logger;

    public RegisterCustomerWebhookTests()
    {
        _mediator = Substitute.For<IMediator>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("fake")
                .Options;
        _context = new ApplicationDbContext(options);
        _logger = Substitute.For<IEsoftLog<RegisterCustomerCommandHandler>>();
    }

    [Fact]
    public async Task Handle_ValidData_ReturnsSuccess()
    {
        var cancellationToken = new CancellationToken();
        var model = TestDataGenerator.ValidModel();
        var handler = new RegisterCustomerWebhookCommandHandler(_logger, _context, _mediator);
        var apiKey = Guid.NewGuid();
        _context.ApiKeys.Add(new ApiKey { Id = apiKey, Name = "Test" });
        _context.SaveChanges();
        _mediator.Send(Arg.Any<RegisterCustomerCommand>(), cancellationToken).Returns(Result.Success(true));

        var result = await handler.Handle(new RegisterCustomerWebhookCommand(apiKey, model), cancellationToken);

        Assert.True(result.IsSuccess);
    }
}
