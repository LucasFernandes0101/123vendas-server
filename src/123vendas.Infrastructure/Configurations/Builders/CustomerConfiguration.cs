using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _123vendas.Infrastructure.Configurations.Builders;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Document)
            .IsRequired()
            .HasColumnType("varchar(14)");

        builder.Property(c => c.Phone)
            .HasColumnType("varchar(20)");

        builder.Property(c => c.Email)
            .HasColumnType("varchar(100)");

        builder.Property(c => c.Address)
            .HasColumnType("varchar(250)");

        builder.Property(c => c.IsActive)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnType("datetime")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UpdatedAt)
            .HasColumnType("datetime")
            .ValueGeneratedOnUpdate();

        builder.HasIndex(c => c.Document).IsUnique();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false);
    }
}