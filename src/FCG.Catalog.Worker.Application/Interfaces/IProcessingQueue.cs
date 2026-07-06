using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Application.Interfaces;

public interface IProcessingQueue<T>
{
    ValueTask Enqueue(T item);
    ValueTask<T> DequeueAsync(CancellationToken cancellationToken);

    int CountItems();
}