using FCG.Catalog.Worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Logger.Configuration;

internal class LogMap : IEntityTypeConfiguration<LogWorker>
{
    public void Configure(EntityTypeBuilder<LogWorker> builder)
    {
        builder.ToTable("LogWorker");
        builder.HasKey(log => log.Id);
        builder.Property(log => log.Id)
            .ValueGeneratedOnAdd();
    }
}
