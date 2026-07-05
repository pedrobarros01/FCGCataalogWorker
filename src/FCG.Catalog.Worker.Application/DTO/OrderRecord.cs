using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Application.DTO;

public record OrderUpdate(Guid OrderId, Guid UserId, Guid GameId, int GameOrderStatus);

public record OrderResponse(Guid OrderId, Guid GameId, int GameOrderStatus);
