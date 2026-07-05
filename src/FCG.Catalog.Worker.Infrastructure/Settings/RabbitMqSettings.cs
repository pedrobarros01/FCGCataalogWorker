using System;
using System.Collections.Generic;
using System.Text;
namespace FCG.Catalog.Worker.Infrastructure.Settings;

public class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = "/";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string KeyQueuePaymentProcessed { get; set; } = "payment.processed";
}