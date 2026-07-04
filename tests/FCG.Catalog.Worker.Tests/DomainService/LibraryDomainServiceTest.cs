using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Domain.Exceptions;
using FCG.Catalog.Worker.Domain.Services;
using FCG.Catalog.Worker.Infrastructure.Repositories;
using FCG.Catalog.Worker.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.DomainService;

[Collection(nameof(LibraryFixtureCollection))]
public class LibraryDomainServiceTest
{
    public LibraryBuilder _libraryBuilder { get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public CategoryBuilder _categoryBuilder { get; set; }
    public LibraryDomainServiceTest(LibraryBuilder libraryBuilder)
    {
        _libraryBuilder = libraryBuilder;
        _categoryBuilder = new CategoryBuilder();
        _gameBuilder = new GameBuilder();
    }

    [Fact]
    public async Task LibraryDomainService_Should_AddGame()
    {
        var game = _gameBuilder.GenerateGame();
        var categorie = _categoryBuilder.GenerateCategory();
        categorie.Games.Add(game);
        var library = _libraryBuilder.GenerateLibrary();

        var context = await ContextBuilder.GenerateContext(categories: [categorie], libraries: [library]);

        var repositoryLibrary = new LibraryRepository(context);
        var repositoryGame = new GameRepository(context);

        var domainService = new LibraryDomainService(repositoryLibrary, repositoryGame);

        var libraryInserted = await domainService.AddGame(library, game.ExternalId);

        Assert.NotNull(libraryInserted);
        Assert.Equal(game.Name, libraryInserted.Games.First().Name);
    }

    [Fact]
    public async Task LibraryDomainService_Should_ThrowGameNotFoundException()
    {
        var game = _gameBuilder.GenerateGame();
        var game2 = _gameBuilder.GenerateGame();
        var categorie = _categoryBuilder.GenerateCategory();
        categorie.Games.Add(game);
        var library = _libraryBuilder.GenerateLibrary();

        var context = await ContextBuilder.GenerateContext(categories: [categorie], libraries: [library]);

        var repositoryLibrary = new LibraryRepository(context);
        var repositoryGame = new GameRepository(context);

        var domainService = new LibraryDomainService(repositoryLibrary, repositoryGame);

        await Assert.ThrowsAsync<BusinessException>(() => domainService.AddGame(library, game2.ExternalId));
    
    }

    [Fact]
    public async Task LibraryDomainService_Should_GetLibrary()
    {
        var game = _gameBuilder.GenerateGame();
        var categorie = _categoryBuilder.GenerateCategory();
        categorie.Games.Add(game);
        var library = _libraryBuilder.GenerateLibrary();
        var userId = library.UserId;
        var context = await ContextBuilder.GenerateContext(categories: [categorie], libraries: [library]);

        var repositoryLibrary = new LibraryRepository(context);
        var repositoryGame = new GameRepository(context);

        var domainService = new LibraryDomainService(repositoryLibrary, repositoryGame);

        var foundLibrary = await domainService.GetLibraryByUserId(userId);

        Assert.NotNull(foundLibrary);
        Assert.Equal(library.ExternalId, foundLibrary.ExternalId);
    }

    [Fact]
    public async Task LibraryDomainService_Should_GetLibraryNull()
    {
        var game = _gameBuilder.GenerateGame();
        var categorie = _categoryBuilder.GenerateCategory();
        categorie.Games.Add(game);
        var library = _libraryBuilder.GenerateLibrary();
        var userId = Guid.NewGuid();
        var context = await ContextBuilder.GenerateContext(categories: [categorie], libraries: [library]);

        var repositoryLibrary = new LibraryRepository(context);
        var repositoryGame = new GameRepository(context);

        var domainService = new LibraryDomainService(repositoryLibrary, repositoryGame);

        var foundLibrary = await domainService.GetLibraryByUserId(userId);

        Assert.Null(foundLibrary);
    }
}
