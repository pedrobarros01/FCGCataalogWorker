using FCG.Catalog.Worker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace FCG.Catalog.Worker.Infrastructure.Queue;

public class ProcessingQueue<T> : IProcessingQueue<T>
{
    private readonly Channel<T> _channel = Channel.CreateUnbounded<T>();

    public int CountItems() => _channel.Reader.Count;

    public ValueTask Enqueue(T item) => _channel.Writer.WriteAsync(item);

    public ValueTask<T> DequeueAsync(CancellationToken cancellationToken) => _channel.Reader.ReadAsync(cancellationToken);
}