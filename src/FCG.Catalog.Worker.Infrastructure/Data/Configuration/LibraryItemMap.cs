using FCG.Catalog.Worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Infrastructure.Data.Configuration;

public class LibraryItemMap : IEntityTypeConfiguration<LibraryItem>
{
    public void Configure(EntityTypeBuilder<LibraryItem> builder)
    {
        builder.ToTable("LibraryItem");
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(item => item.Library)
            .WithMany(library => library.Items)
            .HasForeignKey(item => item.LibraryId);

        builder.Property(item => item.DateCreated)
            .IsRequired();
    }
}
