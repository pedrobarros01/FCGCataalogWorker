using FCG.Catalog.Worker.Infrastructure.Data;
using FCG.Catalog.Worker.Infrastructure.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Threading.Channels;

namespace FCG.Catalog.Worker.Extensions;

public static class ProgramExtensions
{

    public static IServiceCollection ConfigureWorker(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
        //services.AddHostedService<LoggerWorker>();

        return services;
    }
    public static IServiceCollection ConfigureDomain(this IServiceCollection services)
    {
        return services;
    }
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {

        return services;
    }
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //var rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
        //if (rabbitMqSettings == null ||
        //string.IsNullOrWhiteSpace(rabbitMqSettings.Host) ||
        //string.IsNullOrWhiteSpace(rabbitMqSettings.Username) ||
        //string.IsNullOrWhiteSpace(rabbitMqSettings.Password) ||
        //string.IsNullOrWhiteSpace(rabbitMqSettings.KeyQueueOrderPlaced))
        //{
        //    throw new InvalidOperationException("RabbitMQ não configurado, verifique as ENVs do projeto");
        //}
        var databaseConnection = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
        if (databaseConnection == null ||
        string.IsNullOrWhiteSpace(databaseConnection.DefaultConnection))
        {
            throw new InvalidOperationException("String de conexão com o bancode não configurada, verifique as ENVs do projeto");
        }
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(databaseConnection.DefaultConnection));

        //services.AddMassTransit(
        //    x =>
        //    {
        //        x.AddConsumer<OrderPlacedConsumer>();
        //        x.UsingRabbitMq((context, cfg) =>
        //        {
        //            cfg.Host(
        //               host: rabbitMqSettings.Host,
        //               virtualHost: rabbitMqSettings.VirtualHost ?? "/",
        //               h =>
        //               {
        //                   h.Username(rabbitMqSettings.Username);
        //                   h.Password(rabbitMqSettings.Password);
        //               }
        //            );
        //            cfg.ReceiveEndpoint(rabbitMqSettings.KeyQueueOrderPlaced, e =>
        //            {
        //                e.ConfigureConsumer<OrderPlacedConsumer>(context);
        //            });

        //            cfg.Publish<PaymentProcessedEvent>(p => p.ExchangeType = "topic");
        //            cfg.ConfigureEndpoints(context);

        //        });
        //    }

        //);

        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IPaymentProcessedPublisher, PaymentProcessedPublisher>();
        //services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
        //services.AddScoped<IPaymentTransactionStatusRepository, PaymentTransactionStatusRepository>();
        //services.AddSingleton(Channel.CreateUnbounded<Log>());
        //services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>();
        return services;
    }
    public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FCGSettings>(configuration);
        return services;
    }
    public static IHost ApplyMigrations(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
        return app;
    }
}