using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class Library : BaseEntity
{
    public Guid ExternalId { get; private set; }
    public Guid UserId { get; private set; }
    private readonly List<Game> _games = [];
    public IReadOnlyCollection<Game> Games => _games.AsReadOnly();

    public void AddGame(Game game)
    {
        throw new NotImplementedException();
    }
}
