using FCG.Catalog.Worker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class GameOrder : BaseEntity
{
    public Guid OrderId { get; set; }
    public long GameId { get; set; }
    public Game Game { get; set; } = default!;
    public Guid UserId { get; set; }
    public decimal Price { get; set; }
    public GameOrderStatus Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ProcessedOn { get; set; }

    public GameOrder()
    {
        
    }
}
