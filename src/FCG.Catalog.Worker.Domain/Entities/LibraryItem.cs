using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class LibraryItem : BaseEntity
{
    public long LibraryId { get; set; }
    public Library Library { get; set; } = default!;

    public long GameId { get; set; }
    public DateTime DateCreated { get; set; }
}
