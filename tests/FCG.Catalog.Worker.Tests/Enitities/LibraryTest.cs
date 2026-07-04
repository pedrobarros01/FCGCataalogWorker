using CommonTestUtilities.Entities;
using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Tests.Enitities;

[Collection(nameof(LibraryFixtureCollection))]
public class LibraryTest
{
    public LibraryBuilder _libraryBuilder {  get; set; }
    public GameBuilder _gameBuilder { get; set; }
    public LibraryTest(LibraryBuilder libraryBuilder)
    {
        _libraryBuilder = libraryBuilder;
        _gameBuilder = new GameBuilder();
    }


    [Fact]
    public void LibraryEntity_Shrould_AddingGameInList()
    {
        var game = _gameBuilder.GenerateGame();
        var library = _libraryBuilder.GenerateLibrary();

        library.AddGame(game);

        Assert.Single(library.Games);
        Assert.Equal(game.Name, library.Games.ToList()[0].Name);
    }

    [Fact]
    public void LibraryEntity_Shrould_CreateEntity()
    {
        var game = _gameBuilder.GenerateGame();
        var library = new Library(game, Guid.NewGuid());

        Assert.Single(library.Games);
        Assert.Equal(game.Name, library.Games.ToList()[0].Name);
    }
}
