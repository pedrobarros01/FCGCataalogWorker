using FCG.Catalog.Worker.Application.DTO;
using FCG.Catalog.Worker.Application.Interfaces;
using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Enums;
using FCG.Catalog.Worker.Domain.Interfaces;
using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Application.Services;

public class OrderService : BaseApplicationService, IOrderService
{
    private readonly ILibraryDomainService _libraryDomainService;
    private readonly ILibraryRepository _libraryRepository;
    private readonly IGameOrderDomainService _gameOrderDomainService;
    private readonly IGameOrderRepository _gameOrderRepository;

    public OrderService(
        IUnitOfWork uow,
        ILibraryDomainService libraryDomainService, 
        ILibraryRepository libraryRepository, 
        IGameOrderDomainService gameOrderDomainService, 
        IGameOrderRepository gameOrderRepository
    ) : base(uow)
    {
        _libraryDomainService = libraryDomainService;
        _libraryRepository = libraryRepository;
        _gameOrderDomainService = gameOrderDomainService;
        _gameOrderRepository = gameOrderRepository;
    }

    public async Task<OrderResponse> ProcessOrder(OrderUpdate update)
    {
        GameOrder order = await _gameOrderDomainService.UpdateOrder(update.OrderId, update.GameOrderStatus);
        _gameOrderRepository.Update(order);
        Library? library = null;
        bool insert = false;
        if (!await _libraryDomainService.LibraryExist(update.UserId))
        {
            library = await _libraryRepository.Insert(new Library(update.UserId));
            insert = true;
        }
        else
        {
            library = await _libraryDomainService.GetByUserId(update.UserId);
            insert = false;
        }

        if((GameOrderStatus)update.GameOrderStatus == GameOrderStatus.Approved)
        {
            
            await _libraryDomainService.AddGame(library, update.GameId, order.GameId);
            if (!insert)
            {
                _libraryRepository.Update(library);
            }

        }
        await UnitOfWork.CommitAsync();
        return new OrderResponse(order.ExternalId, update.GameId, update.GameOrderStatus);

    }
}
