using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Domain.Enums;
using FCG.Catalog.Worker.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Enitities;

[Collection(nameof(GameOrderFixtureCollection))]
public class GameOrderTest
{
    public GameOrderBuilder _gameOrderBuilder { get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public GameOrderTest(GameOrderBuilder gameOrderBuilder)
    {
        _gameOrderBuilder = gameOrderBuilder;
        _gameBuilder = new GameBuilder();
    }
    [Fact]
    public void GameOrderEntity_Should_UpdateEntity()
    {
        var game = _gameBuilder.GenerateGame();
        var gameOrder = _gameOrderBuilder.GenerateGameOrderPending(game);

        gameOrder.Update(1);

        Assert.Equal(GameOrderStatus.Approved, gameOrder.Status);
        Assert.NotNull(gameOrder.ProcessedOn);
    }


}
