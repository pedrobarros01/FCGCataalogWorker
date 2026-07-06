using Bogus;
using FCG.Catalog.Worker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Entities;

public class CategoryBuilder
{
    public Category GenerateCategory()
    {
        return Build();
    }

    private Category Build()
    {
        var category = new Faker<Category>("pt_BR")
            .RuleFor(p => p.Name, faker => faker.Commerce.ProductName());
        return category;
    }
}
