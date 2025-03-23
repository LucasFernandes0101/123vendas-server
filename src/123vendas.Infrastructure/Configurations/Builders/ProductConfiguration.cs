using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Configurations.Builders;

[ExcludeFromCodeCoverage]
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(p => p.Description)
            .HasColumnType("varchar(500)");

        builder.Property(p => p.Category)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(p => p.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.Image)
            .HasColumnType("varchar(512)")
            .IsRequired();

        builder.Property(p => p.Rating)
            .HasColumnType("decimal(3, 1)") 
            .IsRequired(false);

        builder.Property(p => p.RateCount)
            .HasColumnType("int")
            .HasDefaultValue(0);

        builder.Property(p => p.IsActive)
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("timestamp")
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("boolean")
            .HasDefaultValue(false);
    }
}