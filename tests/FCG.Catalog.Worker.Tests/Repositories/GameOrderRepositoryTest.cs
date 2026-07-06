using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Infrastructure.Repositories;
using FCG.Catalog.Worker.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Repositories;

[Collection(nameof(GameOrderFixtureCollection))]
public class GameOrderRepositoryTest
{
    public LibraryBuilder _libraryBuilder { get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public CategoryBuilder _categoryBuilder { get; set; }
    public GameOrderBuilder _gameOrderBuilder { get; set; }
    public GameOrderRepositoryTest(GameOrderBuilder gameOrderBuilder)
    {
        _libraryBuilder = new LibraryBuilder();
        _categoryBuilder = new CategoryBuilder();
        _gameBuilder = new GameBuilder();
        _gameOrderBuilder = gameOrderBuilder;
    }

    [Fact]
    public async Task GameOrderRepository_Shrould_GetOrderById()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var gameOrder = _gameOrderBuilder.GenerateGameOrder(game);
        var context = await ContextBuilder.GenerateContext(categories: [category], gamesOrders: [gameOrder]);

        var repository = new GameOrderRepository(context);

        var foundOrder = await repository.GetById(gameOrder.ExternalId);
        Assert.NotNull(foundOrder);
    }
    [Fact]
    public async Task GameRepository_Shrould_GetGameNull()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var gameOrder = _gameOrderBuilder.GenerateGameOrder(game);
        var context = await ContextBuilder.GenerateContext(categories: [category], gamesOrders: [gameOrder]);

        var repository = new GameOrderRepository(context);

        var foundOrder = await repository.GetById(Guid.NewGuid());
        Assert.Null(foundOrder);
    }
}
