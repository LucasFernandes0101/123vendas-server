using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _123vendas.Infrastructure.Configurations.Builders;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branch");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Address)
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(x => x.Phone)
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        builder.Property(x => x.IsActive)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime2")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnType("datetime2")
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false);
    }
}