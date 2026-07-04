using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class, IBaseEntity
{
    Task<T> Insert(T entity);
    T Update(T entity);
    T Delete(T entity);
}
