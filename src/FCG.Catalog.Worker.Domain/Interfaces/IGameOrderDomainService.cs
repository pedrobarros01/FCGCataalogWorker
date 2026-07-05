using FCG.Catalog.Worker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Interfaces;

public interface IGameOrderDomainService
{
    Task<GameOrder> UpdateOrder(Guid orderId, int orderStatus);
}
