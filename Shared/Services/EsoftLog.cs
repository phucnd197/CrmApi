using Microsoft.Extensions.Logging;
using Shared.Contracts.Services;

namespace Shared.Services;

public class EsoftLog<T> : IEsoftLog<T>
{
    private readonly ILogger<T> _logger;

    public EsoftLog(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        _logger.Log(logLevel, eventId, state, exception, formatter);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return _logger.IsEnabled(logLevel);
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return _logger.BeginScope(state);
    }
}