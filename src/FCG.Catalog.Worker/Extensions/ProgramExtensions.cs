using FCG.Catalog.Worker.Application.DTO;
using FCG.Catalog.Worker.Application.Interfaces;
using FCG.Catalog.Worker.Application.Services;
using FCG.Catalog.Worker.Domain.Interfaces;
using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using FCG.Catalog.Worker.Domain.Services;
using FCG.Catalog.Worker.Infrastructure.Data;
using FCG.Catalog.Worker.Infrastructure.Messaging.Consumer;
using FCG.Catalog.Worker.Infrastructure.Persistence;
using FCG.Catalog.Worker.Infrastructure.Queue;
using FCG.Catalog.Worker.Infrastructure.Repositories;
using FCG.Catalog.Worker.Infrastructure.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        return services;
    }
    public static IServiceCollection ConfigureDomain(this IServiceCollection services)
    {
        services.AddScoped<ILibraryDomainService, LibraryDomainService>();
        services.AddScoped<IGameOrderDomainService, GameOrderDomainService>();
        return services;
    }
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddSingleton<IProcessingQueue<OrderUpdate>, ProcessingQueue<OrderUpdate>>();

        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
        if (rabbitMqSettings == null ||
        string.IsNullOrWhiteSpace(rabbitMqSettings.Host) ||
        string.IsNullOrWhiteSpace(rabbitMqSettings.Username) ||
        string.IsNullOrWhiteSpace(rabbitMqSettings.Password) ||
        string.IsNullOrWhiteSpace(rabbitMqSettings.KeyQueuePaymentProcessed))
        {
            throw new InvalidOperationException("RabbitMQ não configurado, verifique as ENVs do projeto");
        }
        var databaseConnection = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
        if (databaseConnection == null ||
        string.IsNullOrWhiteSpace(databaseConnection.DefaultConnection))
        {
            throw new InvalidOperationException("String de conexão com o bancode não configurada, verifique as ENVs do projeto");
        }
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(databaseConnection.DefaultConnection));

        services.AddMassTransit(
            x =>
            {
                x.AddConsumer<PaymentProcessedConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(
                       host: rabbitMqSettings.Host,
                       virtualHost: rabbitMqSettings.VirtualHost ?? "/",
                       h =>
                       {
                           h.Username(rabbitMqSettings.Username);
                           h.Password(rabbitMqSettings.Password);
                       }
                    );
                    cfg.ReceiveEndpoint(rabbitMqSettings.KeyQueuePaymentProcessed, e =>
                    {
                        e.ConfigureConsumer<PaymentProcessedConsumer>(context);
                    });

                });
            }

        );
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGameOrderRepository, GameOrderRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<ILibraryRepository, LibraryRepository>();
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