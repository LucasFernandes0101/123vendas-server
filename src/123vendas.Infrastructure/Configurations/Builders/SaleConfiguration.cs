using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace _123vendas.Infrastructure.Configurations.Builders;

[ExcludeFromCodeCoverage]
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(s => s.Date)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(s => s.TotalAmount)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(s => s.CancelledAt)
            .IsRequired(false);

        builder.HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Branch)
            .WithMany()
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(s => s.CreatedAt)
            .HasColumnType("datetime2")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.UpdatedAt)
            .HasColumnType("datetime2")
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("bit")
        .HasDefaultValue(false);

        builder.HasMany(s => s.Items)
               .WithOne(i => i.Sale)
               .HasForeignKey(i => i.SaleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}