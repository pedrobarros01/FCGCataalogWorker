using FCG.Catalog.Worker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class GameOrder : BaseEntity
{
    public Guid ExternalId { get; private set; }
    public long GameId { get; set; }
    public Game Game { get; set; } = default!;
    public Guid UserId { get; private set; }
    public decimal Price { get; private set; }
    public GameOrderStatus Status { get; set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? ProcessedOn { get; private set; }

    public GameOrder()
    {
        
    }

    public void Update(int orderStatus)
    {
        ProcessedOn = DateTime.UtcNow;
        Status = (GameOrderStatus)orderStatus;
    }
}
