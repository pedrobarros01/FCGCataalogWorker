using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Infrastructure.Repositories;
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
    public async Task LibraryRepository_Shrould_GetLibraryByUserId()
    {
        Guid userId = Guid.NewGuid();
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var library = _libraryBuilder.GenerateLibrary();
        library.UserId = userId;
        var context = await ContextBuilder.GenerateContext(categories: [category], libraries: [library]);

        var repository = new LibraryRepository(context);

        var foundLibrary = await repository.GetLibraryByUserId(userId);
        Assert.NotNull(foundLibrary);
    }
    [Fact]
    public async Task LibraryRepository_Shrould_GetLibraryNull()
    {
        Guid userId = Guid.NewGuid();
        var game = _gameBuilder.GenerateGame();
        var category = _categoryBuilder.GenerateCategory();
        category.Games.Add(game);
        var library = _libraryBuilder.GenerateLibrary();
        library.UserId = Guid.NewGuid();
        var context = await ContextBuilder.GenerateContext(categories: [category], libraries: [library]);

        var repository = new LibraryRepository(context);

        var foundLibrary = await repository.GetLibraryByUserId(userId);
        Assert.Null(foundLibrary);
    }
}
