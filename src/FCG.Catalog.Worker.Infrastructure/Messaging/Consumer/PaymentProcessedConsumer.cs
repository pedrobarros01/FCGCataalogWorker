using FCG.Catalog.Worker.Application.DTO;
using FCG.Catalog.Worker.Application.Extensions;
using FCG.Catalog.Worker.Application.Interfaces;
using FCG.Shared.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Infrastructure.Messaging.Consumer;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    public readonly IProcessingQueue<OrderUpdate> _channel;

    public PaymentProcessedConsumer(IProcessingQueue<OrderUpdate> channel)
    {
        _channel = channel;
    }

    public Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        _channel.Enqueue(context.Message.MapToDTO());
        return Task.CompletedTask;
    }
}
