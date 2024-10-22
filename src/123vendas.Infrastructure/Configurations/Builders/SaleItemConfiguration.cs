﻿using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace _123vendas.Infrastructure.Configurations.Builders;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItem");

        builder.HasKey(si => si.Id);

        builder.Property(si => si.SaleId)
            .IsRequired();

        builder.Property(si => si.ProductId)
            .IsRequired();

        builder.Property(si => si.ProductName)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(si => si.Quantity)
            .IsRequired();

        builder.Property(si => si.UnitPrice)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(si => si.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(si => si.Discount)
            .HasColumnType("decimal(10,2)")
            .IsRequired(false);

        builder.Property(si => si.IsCancelled)
            .IsRequired();

        builder.Property(si => si.CancelledAt)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(si => si.CreatedAt)
            .HasColumnType("datetime")
            .ValueGeneratedOnAdd();

        builder.Property(si => si.UpdatedAt)
            .HasColumnType("datetime")
            .ValueGeneratedOnUpdate();

        builder.Property(si => si.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false);

        builder.HasOne(si => si.Product)
            .WithMany()
            .HasForeignKey(si => si.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(si => si.Sale)
            .WithMany()
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}