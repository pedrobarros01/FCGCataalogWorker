using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Infrastructure.Repositories;
using FCG.Catalog.Worker.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Repositories;

[Collection(nameof(GameFixtureCollection))]
public class GameRepositoryTest
{
    public LibraryBuilder _libraryBuilder { get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public CategoryBuilder _categoryBuilder { get; set; }
    public GameRepositoryTest(GameBuilder gameBuilder)
    {
        _libraryBuilder = new LibraryBuilder();
        _categoryBuilder = new CategoryBuilder();
        _gameBuilder = gameBuilder;
    }

    [Fact]
    public async Task GameRepository_Shrould_GetGameById()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var context = await ContextBuilder.GenerateContext(categories: [category]);

        var repository = new GameRepository(context);

        var foundGame = await repository.GetById(game.ExternalId);
        Assert.NotNull(foundGame);
    }
    [Fact]
    public async Task GameRepository_Shrould_GetGameNull()
    {
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var context = await ContextBuilder.GenerateContext(categories: [category]);

        var repository = new GameRepository(context);

        var foundGame = await repository.GetById(Guid.NewGuid());
        Assert.Null(foundGame);
    }
}
