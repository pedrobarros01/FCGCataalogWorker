using FCG.Catalog.Worker.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace FCG.Catalog.Worker.Logger;

public class LoggerWorker : BackgroundService
{
    private readonly Channel<LogWorker> _channel;
    private readonly IServiceScopeFactory _factory;

    public LoggerWorker(Channel<LogWorker> channel, IServiceScopeFactory factory)
    {
        _channel = channel;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var log in _channel.Reader.ReadAllAsync(stoppingToken))
        {
            using var scope = _factory.CreateScope();
            var context = scope.ServiceProvider.GetService<LoggerDbContext>();
            if (context == null) throw new Exception("Contexto não encontrado");
            context.LogWorker.Add(log);
            await context.SaveChangesAsync();


        }
    }
}
