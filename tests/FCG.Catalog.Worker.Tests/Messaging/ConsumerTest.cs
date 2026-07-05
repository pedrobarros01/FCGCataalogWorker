using FCG.Catalog.Worker.Application.DTO;
using FCG.Catalog.Worker.Domain.Enums;
using FCG.Catalog.Worker.Infrastructure.Messaging.Consumer;
using FCG.Catalog.Worker.Infrastructure.Queue;
using FCG.Shared.Events;
using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Messaging;

public class ConsumerTest
{
    [Fact]
    public void Consumer_Should_InsertItemInQueue()
    {
        
        Random random = new Random();
        var channel = new ProcessingQueue<OrderUpdate>();
        var consumer = new PaymentProcessedConsumer(channel);
        var message = new PaymentProcessedEvent(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            random.Next(1, 3)
        );
        var contextConsumer = new Mock<ConsumeContext<PaymentProcessedEvent>>();
        contextConsumer
        .Setup(x => x.Message)
        .Returns(message);
        consumer.Consume(contextConsumer.Object);
        Assert.Equal(1, channel.CountItems());

    }
}
