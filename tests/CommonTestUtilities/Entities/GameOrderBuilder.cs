using Bogus;
using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Entities;

public class GameOrderBuilder
{
    public GameOrder GenerateGameOrder(Game game)
    {
        var gameOrder = Build();
        gameOrder.GameId = game.Id;
        return gameOrder;
    }

    public GameOrder GenerateGameOrderPending(Game game)
    {
        var gameOrder = Build();
        gameOrder.Status = GameOrderStatus.Pending;
        gameOrder.GameId = game.Id;
        return gameOrder;
    }

    private GameOrder Build()
    {
        var status = new[]
        {
            GameOrderStatus.Pending,
            GameOrderStatus.Approved,
            GameOrderStatus.Rejected,
        };
        var gameOrder = new Faker<GameOrder>("pt_BR")
            .RuleFor(p => p.ExternalId, faker => faker.Random.Guid())
            .RuleFor(p => p.UserId, faker => faker.Random.Guid())
            .RuleFor(p => p.Price, faker => faker.Random.Decimal(1, 500))
            .RuleFor(p => p.Status, faker => faker.PickRandom(status))
            .RuleFor(p => p.CreatedOn, faker => faker.Date.Recent());
        return gameOrder;
    }
}
