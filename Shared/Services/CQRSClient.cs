using MassTransit;
using Shared.Contracts.Services;

namespace Shared.Services;

public class CQRSClient : ICQRSClient
{
    private readonly IPublishEndpoint _publishEndpoint;

    public CQRSClient(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
    {
        return _publishEndpoint.ConnectPublishObserver(observer);
    }

    public Task Publish<T>(T message, CancellationToken cancellationToken = new()) where T : class
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }

    public Task Publish<T>(T message, IPipe<PublishContext<T>> publishPipe,
        CancellationToken cancellationToken = new()) where T : class
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }

    public Task Publish<T>(T message, IPipe<PublishContext> publishPipe,
        CancellationToken cancellationToken = new()) where T : class
    {
        return _publishEndpoint.Publish(message, publishPipe, cancellationToken);
    }

    public Task Publish(object message, CancellationToken cancellationToken = new())
    {
        return _publishEndpoint.Publish(message, cancellationToken);
    }

    public Task Publish(object message, IPipe<PublishContext> publishPipe,
        CancellationToken cancellationToken = new())
    {
        return _publishEndpoint.Publish(message, publishPipe, cancellationToken);
    }

    public Task Publish(object message, Type messageType, CancellationToken cancellationToken = new())
    {
        return _publishEndpoint.Publish(message, messageType, cancellationToken);
    }

    public Task Publish(object message, Type messageType, IPipe<PublishContext> publishPipe,
        CancellationToken cancellationToken = new())
    {
        return _publishEndpoint.Publish(message, messageType, publishPipe, cancellationToken);
    }

    public Task Publish<T>(object values, CancellationToken cancellationToken = new()) where T : class
    {
        return _publishEndpoint.Publish(values, cancellationToken);
    }

    public Task Publish<T>(object values, IPipe<PublishContext<T>> publishPipe,
        CancellationToken cancellationToken = new()) where T : class
    {
        return _publishEndpoint.Publish(values, publishPipe, cancellationToken);
    }

    public Task Publish<T>(object values, IPipe<PublishContext> publishPipe,
        CancellationToken cancellationToken = new()) where T : class
    {
        return _publishEndpoint.Publish(values, publishPipe, cancellationToken);
    }
}