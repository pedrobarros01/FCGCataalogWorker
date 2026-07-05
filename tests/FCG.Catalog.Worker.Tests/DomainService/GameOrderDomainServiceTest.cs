using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Enums;
using FCG.Catalog.Worker.Domain.Exceptions;
using FCG.Catalog.Worker.Domain.Services;
using FCG.Catalog.Worker.Infrastructure.Repositories;
using FCG.Catalog.Worker.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.DomainService;

[Collection(nameof(GameOrderFixtureCollection))]
public class GameOrderDomainServiceTest
{
    public LibraryBuilder _libraryBuilder { get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public CategoryBuilder _categoryBuilder { get; set; }
    public GameOrderBuilder _gameOrderBuilder { get; set; }
    public GameOrderDomainServiceTest(GameOrderBuilder gameOrderBuilder)
    {
        _libraryBuilder = new LibraryBuilder();
        _categoryBuilder = new CategoryBuilder();
        _gameBuilder = new GameBuilder();
        _gameOrderBuilder = gameOrderBuilder;
    }

    [Fact]
    public async Task GameOrderDomainService_Should_UpdateOrderApproved()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var gameOrder = _gameOrderBuilder.GenerateGameOrderPending(game);
        var context = await ContextBuilder.GenerateContext(categories: [category], gamesOrders: [gameOrder]);

        var repositoryOrder = new GameOrderRepository(context);

        var domainService = new GameOrderDomainService(repositoryOrder);

        var orderUpdated = await domainService.UpdateOrder(gameOrder.ExternalId, 1);

        Assert.NotNull(orderUpdated);
        Assert.Equal(GameOrderStatus.Approved, orderUpdated.Status);
    }

    [Fact]
    public async Task GameOrderDomainService_Should_UpdateOrderRejected()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var gameOrder = _gameOrderBuilder.GenerateGameOrderPending(game);
        var context = await ContextBuilder.GenerateContext(categories: [category], gamesOrders: [gameOrder]);

        var repositoryOrder = new GameOrderRepository(context);

        var domainService = new GameOrderDomainService(repositoryOrder);

        var orderUpdated = await domainService.UpdateOrder(gameOrder.ExternalId, 2);

        Assert.NotNull(orderUpdated);
        Assert.Equal(GameOrderStatus.Rejected, orderUpdated.Status);
    }

    [Fact]
    public async Task GameOrderDomainService_Should_ThrowOrderNotFoundException()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var gameOrder = _gameOrderBuilder.GenerateGameOrderPending(game);
        var context = await ContextBuilder.GenerateContext(categories: [category], gamesOrders: [gameOrder]);

        var repositoryOrder = new GameOrderRepository(context);

        var domainService = new GameOrderDomainService(repositoryOrder);

        await Assert.ThrowsAsync<BusinessException>(() => domainService.UpdateOrder(Guid.NewGuid(), 2));
    
    }

    [Fact]
    public async Task GameOrderDomainService_Should_ThrowStatusOrderNotFoundException()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var gameOrder = _gameOrderBuilder.GenerateGameOrderPending(game);
        var context = await ContextBuilder.GenerateContext(categories: [category], gamesOrders: [gameOrder]);

        var repositoryOrder = new GameOrderRepository(context);

        var domainService = new GameOrderDomainService(repositoryOrder);

        await Assert.ThrowsAsync<BusinessException>(() => domainService.UpdateOrder(gameOrder.ExternalId, 3));

    }

}
