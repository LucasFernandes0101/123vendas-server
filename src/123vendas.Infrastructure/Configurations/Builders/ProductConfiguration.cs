﻿using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _123vendas.Infrastructure.Configurations.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(p => p.Description)
            .HasColumnType("varchar(500)");

        builder.Property(p => p.Category)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(p => p.BasePrice)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("datetime")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("datetime")
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false);
    }
}