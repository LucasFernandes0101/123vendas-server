using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Configurations.Builders;

[ExcludeFromCodeCoverage]
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasColumnType("varchar(255)")
            .IsRequired(false);

        builder.Property(u => u.Username)
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder.Property(u => u.Password)
            .HasColumnType("varchar(200)")
            .IsRequired(false);

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.Firstname)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            name.Property(n => n.Lastname)
                .HasColumnType("varchar(100)")
                .IsRequired(false);
        });

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.HasAddress)
                .HasColumnType("boolean")
                .IsRequired();

            address.Property(a => a.City)
                .HasColumnType("varchar(100)")
                .IsRequired(false);

            address.Property(a => a.Street)
                .HasColumnType("varchar(150)")
                .IsRequired(false);

            address.Property(a => a.Number)
                .HasColumnType("varchar(10)")
                .IsRequired(false);

            address.Property(a => a.Zipcode)
                .HasColumnType("varchar(20)")
                .IsRequired(false);

            address.OwnsOne(a => a.Geolocation, geo =>
            {
                geo.Property(g => g.HasGeolocation)
                    .HasColumnType("boolean")
                    .IsRequired();

                geo.Property(g => g.Lat)
                    .HasColumnType("varchar(20)")
                    .IsRequired(false);

                geo.Property(g => g.Long)
                    .HasColumnType("varchar(20)")
                    .IsRequired(false);
            });
        });

        builder.Property(u => u.Phone)
            .HasColumnType("varchar(20)")
            .IsRequired(false);

        builder.Property(u => u.Status)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(u => u.Role)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasColumnType("timestamptz")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.UpdatedAt)
            .HasColumnType("timestamptz")
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.IsDeleted)
            .HasColumnType("boolean")
            .HasDefaultValue(false);
    }
}