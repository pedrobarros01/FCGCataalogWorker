using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Infrastructure.Settings;

public class FCGSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public RabbitMqSettings RabbitMQ { get; set; } = new();
}
