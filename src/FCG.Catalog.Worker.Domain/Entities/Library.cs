using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class Library : BaseEntity
{
    public Guid ExternalId { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    private readonly List<Game> _games = [];
    public IReadOnlyCollection<Game> Games => _games.AsReadOnly();

    public Library()
    {
        
    }

    public Library(Game game, Guid userId)
    {
        UserId = userId;
        this.AddGame(game);
    }

    public void AddGame(Game game)
    {
        _games.Add(game);
    }
}
