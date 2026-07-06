using FCG.Catalog.Worker.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace FCG.Catalog.Worker.Logger;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly Channel<LogWorker> _channel;

    public DatabaseLoggerProvider(Channel<LogWorker> channel)
    {
        _channel = channel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_channel, categoryName);
    }

    public void Dispose()
    {

    }
}
