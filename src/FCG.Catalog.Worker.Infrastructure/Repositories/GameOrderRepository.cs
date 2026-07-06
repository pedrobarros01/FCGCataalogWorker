using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using FCG.Catalog.Worker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Infrastructure.Repositories
{
    public class GameOrderRepository(ApplicationDbContext context) : BaseRepository<GameOrder>(context), IGameOrderRepository
    {
        public async Task<GameOrder?> GetById(Guid orderId)
        {
            return await BaseQuery<GameOrder>()
                .FirstOrDefaultAsync(order => order.ExternalId == orderId);
        }
    }
}
