using FCG.Catalog.Worker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Interfaces.Repositories;

public interface ILibraryRepository : IBaseRepository<Library>
{
    Task<Library?> GetLibraryByUserId(Guid userId);
}
