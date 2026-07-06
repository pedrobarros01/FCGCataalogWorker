using FCG.Catalog.Worker.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace FCG.Catalog.Worker.Logger;

public class DatabaseLogger : ILogger
{
    private readonly Channel<LogWorker> _channel;
    private readonly string _category;

    public DatabaseLogger(Channel<LogWorker> channel, string category)
    {
        _channel = channel;
        _category = category;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine(logLevel);
        if (logLevel == LogLevel.Error)
            _channel.Writer.TryWrite(new LogWorker(logLevel.ToString(), _category, formatter(state, exception), exception?.ToString()));
        else
        {
            Console.WriteLine(formatter(state, exception));
        }
    }
}
