using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Application.Services;

public class BaseApplicationService(IUnitOfWork uow)
{
    protected IUnitOfWork UnitOfWork { get; } = uow;
}
