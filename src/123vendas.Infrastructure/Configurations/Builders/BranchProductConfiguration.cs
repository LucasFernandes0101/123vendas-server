using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Configurations.Builders;

[ExcludeFromCodeCoverage]
public class BranchProductConfiguration : IEntityTypeConfiguration<BranchProduct>
{
    public void Configure(EntityTypeBuilder<BranchProduct> builder)
    {
        builder.ToTable("BranchProduct");

        builder.HasKey(bp => bp.Id);

        builder.Property(bp => bp.ProductName)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(bp => bp.ProductCategory)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(bp => bp.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(bp => bp.StockQuantity)
            .IsRequired();

        builder.Property(bp => bp.IsActive)
            .HasColumnType("bit")
            .IsRequired();

        builder.HasOne(bp => bp.Product)
            .WithMany()
            .HasForeignKey(bp => bp.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(bp => bp.Branch)
            .WithMany()
            .HasForeignKey(bp => bp.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(bp => bp.CreatedAt)
            .HasColumnType("datetime2")
            .ValueGeneratedOnAdd();
        builder.Property(bp => bp.UpdatedAt)
            .HasColumnType("datetime2")
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false);
    }
}