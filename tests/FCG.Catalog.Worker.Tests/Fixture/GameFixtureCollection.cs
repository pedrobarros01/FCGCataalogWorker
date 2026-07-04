using CommonTestUtilities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Fixture;

[CollectionDefinition("GameFixtureCollection")]
public class GameFixtureCollection : ICollectionFixture<GameBuilder>
{
}
