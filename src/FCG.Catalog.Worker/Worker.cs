using FCG.Catalog.Worker.Application.DTO;
using FCG.Catalog.Worker.Application.Interfaces;
using FCG.Catalog.Worker.Domain.Enums;

namespace FCG.Catalog.Worker
{
    public class Worker(ILogger<Worker> logger, IProcessingQueue<OrderUpdate> channel, IServiceScopeFactory scopeFactory) : BackgroundService
    {
        private readonly IProcessingQueue<OrderUpdate> _channel = channel;
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_channel.CountItems() > 0)
                    {
                        var item = await _channel.DequeueAsync(stoppingToken);

                        using var scope = _scopeFactory.CreateScope();

                        var orderService = scope.ServiceProvider
                            .GetRequiredService<IOrderService>();

                        await orderService.ProcessOrder(item);

                        logger.LogInformation(
                            "UserId: {UserId} - GameId: {GameId} - OrderId: {OrderId} - Status: {Status}",
                            item.UserId,
                            item.GameId,
                            item.OrderId,
                            (GameOrderStatus)item.GameOrderStatus);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            }
        }
    }
}
