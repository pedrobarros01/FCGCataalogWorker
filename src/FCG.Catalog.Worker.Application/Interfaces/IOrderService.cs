using FCG.Catalog.Worker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> ProcessOrder(OrderUpdate update);
}
