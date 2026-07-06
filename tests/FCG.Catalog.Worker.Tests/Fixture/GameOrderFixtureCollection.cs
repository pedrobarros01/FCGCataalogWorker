using CommonTestUtilities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Fixture;

[CollectionDefinition("GameOrderFixtureCollection")]
public class GameOrderFixtureCollection : ICollectionFixture<GameOrderBuilder>
{
}
