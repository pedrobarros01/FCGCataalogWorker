using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Interfaces;

public interface IBaseEntity
{
    long Id { get; set; }
}
