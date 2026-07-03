using FCG.Catalog.Worker.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class BaseEntity : IBaseEntity
{
    public long Id { get; set; }
}
