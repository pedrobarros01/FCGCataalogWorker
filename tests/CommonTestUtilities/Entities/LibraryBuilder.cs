using Bogus;
using FCG.Catalog.Worker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Entities;

public class LibraryBuilder
{
    public Library GenerateLibrary()
    {
        return Build();
    }

    private Library Build()
    {
        var library = new Faker<Library>("pt_BR")
            .RuleFor(p => p.ExternalId, faker => faker.Random.Guid())
            .RuleFor(p => p.UserId, faker => faker.Random.Guid());
        return library;
    }
}
