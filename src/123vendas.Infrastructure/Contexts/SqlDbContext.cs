using _123vendas.Domain.Base.Interfaces;
using _123vendas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace _123vendas.Infrastructure.Contexts;

[ExcludeFromCodeCoverage]
public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {
        // base.Database.EnsureCreated();
    }

    public DbSet<Branch> Branches { get; set; }
    public DbSet<BranchProduct> BranchProducts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        EnsureIsNotDeletedFilter(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private void EnsureIsNotDeletedFilter(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(IBaseEntity.IsDeleted));
                var body = Expression.Not(property);
                var lambda = Expression.Lambda(body, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConfigureTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        ConfigureTimestamps();
        return base.SaveChanges();
    }

    private void ConfigureTimestamps()
    {
        var entries = ChangeTracker.Entries<IBaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.UpdatedAt = DateTime.Now;
            }
            else if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAt = DateTime.Now;
        }
    }
}