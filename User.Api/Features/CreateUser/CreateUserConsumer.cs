using MassTransit;
using MediatR;

namespace User_Api.Features.CreateUser;

public class CreateUserConsumer : IConsumer<CreateUserCommand>
{
    private readonly IMediator _mediator;

    public CreateUserConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        await _mediator.Send(context.Message, context.CancellationToken);
    }
}
