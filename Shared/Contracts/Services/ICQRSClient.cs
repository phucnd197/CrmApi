using MassTransit;
using MassTransit.Mediator;

namespace Shared.Contracts.Services;
public interface ICQRSClient : IPublishEndpoint
{
}
