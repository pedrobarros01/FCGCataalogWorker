using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Entities;

public class LogWorker : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public string Level { get; set; }
    public string Category { get; set; }
    public string Message { get; set; }
    public string? Exception { get; set; }

    public LogWorker(string level, string category, string message, string? exception)
    {
        CreatedAt = DateTime.UtcNow;
        Level = level;
        Category = category;
        Message = message;
        Exception = exception;
    }
}
