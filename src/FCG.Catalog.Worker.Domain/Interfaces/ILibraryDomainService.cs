using FCG.Catalog.Worker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Interfaces;

public interface ILibraryDomainService
{
    Task<Library> AddGame(Library library, Guid gameId);
    Task<Library?> GetLibraryByUserId(Guid userId);
}
