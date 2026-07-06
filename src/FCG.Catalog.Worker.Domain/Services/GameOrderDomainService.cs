using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Enums;
using FCG.Catalog.Worker.Domain.Exceptions;
using FCG.Catalog.Worker.Domain.Interfaces;
using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Services;

public class GameOrderDomainService : IGameOrderDomainService
{
    private readonly IGameOrderRepository _gameOrderRepository;

    public GameOrderDomainService(IGameOrderRepository gameOrderRepository)
    {
        _gameOrderRepository = gameOrderRepository;
    }

    public async Task<GameOrder> UpdateOrder(Guid orderId, int orderStatus)
    {
        GameOrder order = await _gameOrderRepository.GetById(orderId) ?? throw new BusinessException("Pedido do jogo não existe");
        if (!Enum.IsDefined(typeof(GameOrderStatus), orderStatus)) throw new BusinessException("Status de pedido irreconhecível");
        order.Update(orderStatus);
        return order;
    }
}
