using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using FCG.Catalog.Worker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Infrastructure.Repositories;

public class LibraryRepository(ApplicationDbContext context) : BaseRepository<Library>(context), ILibraryRepository
{
    public async Task<Library?> GetLibraryByUserId(Guid userId)
    {
        return await BaseQuery<Library>()
            .FirstOrDefaultAsync(l => l.UserId == userId);
    }
}
