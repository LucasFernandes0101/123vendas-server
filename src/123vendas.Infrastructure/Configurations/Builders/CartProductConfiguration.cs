using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace _123vendas.Infrastructure.Configurations.Builders;

public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.ToTable("CartProduct");

        builder.HasKey(s => s.Id);

        builder.Property(si => si.Quantity)
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnType("timestamptz")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.UpdatedAt)
            .HasColumnType("timestamptz")
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("boolean")
        .HasDefaultValue(false);

        builder.HasOne(s => s.Cart)
               .WithMany(i => i.Products)
               .HasForeignKey(i => i.CartId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Product)
               .WithMany()
               .HasForeignKey(i => i.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}