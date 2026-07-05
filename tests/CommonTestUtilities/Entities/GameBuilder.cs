using Bogus;
using FCG.Catalog.Worker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Entities;

public class GameBuilder
{
    public Game GenerateGame()
    {
        return Build();
    }

    private Game Build()
    {
        var library = new Faker<Game>("pt_BR")
            .RuleFor(p => p.ExternalId, faker => faker.Random.Guid())
            .RuleFor(p => p.CategoryId, faker => faker.Random.Long())
            .RuleFor(p => p.Price, faker => faker.Random.Decimal(1, 500))
            .RuleFor(p => p.Description, faker => faker.Lorem.Sentence())
            .RuleFor(p => p.Name, faker => faker.Commerce.ProductName());
        return library;
    }
}
