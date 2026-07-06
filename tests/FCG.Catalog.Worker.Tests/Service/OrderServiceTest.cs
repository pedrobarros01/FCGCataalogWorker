using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Application.DTO;
using FCG.Catalog.Worker.Application.Services;
using FCG.Catalog.Worker.Domain.Services;
using FCG.Catalog.Worker.Infrastructure.Persistence;
using FCG.Catalog.Worker.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Service;

public class OrderServiceTest
{
    public LibraryBuilder _libraryBuilder { get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public CategoryBuilder _categoryBuilder { get; set; }
    public GameOrderBuilder _gameOrderBuilder { get; set; }
    public OrderServiceTest()
    {
        _libraryBuilder = new LibraryBuilder();
        _gameBuilder = new GameBuilder();
        _categoryBuilder = new CategoryBuilder();
        _gameOrderBuilder = new GameOrderBuilder();
    }
    [Fact]
    public async Task OrderSerice_Should_ProcessOrderApproved()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var gameOrder = _gameOrderBuilder.GenerateGameOrderPending(game);
        gameOrder.Game = game;
        var context = await ContextBuilder.GenerateContext(categories: [category], gamesOrders: [gameOrder]);
        var updateDto = new OrderUpdate(gameOrder.ExternalId, gameOrder.UserId, game.ExternalId, 1);

        var repositoryLibrary = new LibraryRepository(context);
        var repositoryGame = new GameRepository(context);
        var repositoryGameOrder = new GameOrderRepository(context);
        var repositoryOrder = new GameOrderRepository(context);

        var domainServiceLibrary = new LibraryDomainService(repositoryLibrary, repositoryGame);
        var domainServiceGameOrder = new GameOrderDomainService(repositoryOrder);
        var uow = new UnitOfWork(context);

        var service = new OrderService(uow, domainServiceLibrary, repositoryLibrary, domainServiceGameOrder, repositoryGameOrder);
        var response = await service.ProcessOrder(updateDto);

        Assert.NotNull(response);
        Assert.Equal(gameOrder.ExternalId, response.OrderId);
        Assert.Equal(1, response.GameOrderStatus);


    }
}
