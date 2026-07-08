using FCG.Catalog.Worker.Application.DTO;
using FCG.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Application.Extensions;

public static class PaymentProcessedEventExtensions
{
    public static OrderUpdate MapToDTO(this PaymentProcessedEvent @event)
    {
        return new OrderUpdate(@event.OrderId, @event.UserId, @event.GameId, @event.Status == "Approved" ? 1 : 2);
    }
}
