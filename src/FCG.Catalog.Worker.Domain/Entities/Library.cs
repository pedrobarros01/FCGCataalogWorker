using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class Library : BaseEntity
{
    public Guid UserId { get; set; }
    public ICollection<LibraryItem> Items { get; set; } = [];

}
