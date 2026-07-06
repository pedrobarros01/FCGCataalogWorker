using FCG.Catalog.Worker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Interfaces.Repositories;

public interface IGameRepository : IBaseRepository<Game>
{
    Task<Game?> GetById(Guid gameId);
}
