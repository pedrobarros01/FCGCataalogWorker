using FCG.Catalog.Worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Infrastructure.Data.Configuration;

public class LibraryMap : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Library");
        builder.HasKey(library => library.Id);
        builder.Property(library => library.Id)
            .ValueGeneratedOnAdd();

        builder.Property(library => library.UserId)
            .IsRequired();

        builder.HasMany(library => library.Items)
            .WithOne(item => item.Library)
            .HasForeignKey(item => item.LibraryId);
    }
}
