using Microsoft.Extensions.Logging;

namespace Shared.Contracts.Services;

public interface IEsoftLog<T> : ILogger<T>
{
}