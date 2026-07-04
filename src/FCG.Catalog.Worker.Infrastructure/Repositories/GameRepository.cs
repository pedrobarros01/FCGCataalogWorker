using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using FCG.Catalog.Worker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Infrastructure.Repositories;

public class GameRepository(ApplicationDbContext context) : BaseRepository<Game>(context), IGameRepository
{
    public async Task<Game?> GetById(Guid gameId)
    {
        return await BaseQuery<Game>()
            .FirstOrDefaultAsync(g => g.ExternalId == gameId);
    }
}
