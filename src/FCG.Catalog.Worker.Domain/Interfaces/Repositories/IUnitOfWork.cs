using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}
