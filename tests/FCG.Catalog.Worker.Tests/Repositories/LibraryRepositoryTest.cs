using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Repositories;

[Collection(nameof(LibraryFixtureCollection))]
public class LibraryRepositoryTest
{
    public LibraryBuilder _libraryBuilder { get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public CategoryBuilder _categoryBuilder { get; set; }
    public LibraryRepositoryTest(LibraryBuilder libraryBuilder)
    {
        _libraryBuilder = libraryBuilder;
        _categoryBuilder = new CategoryBuilder();
        _gameBuilder = new GameBuilder();
    }

    [Fact]
    public void LibraryRepository_Shrould_GetLibraryByUserId()
    {
        Guid userId = Guid.NewGuid();
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var library = _libraryBuilder.GenerateLibrary();
        library.UserId = userId;
        var context = ContextBuilder.GenerateContext(categories: [category], games: [game], libraries: [library]);

        var repository = new LibraryRepository(context);

        var foundLibrary = repository.GetLibraryByUserId(library.UserId);
        Assert.Single(library.Games);
        Assert.Equal(game.Name, library.Games.ToList()[0].Name);
    }
}
