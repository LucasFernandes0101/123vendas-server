using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Seeds;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Seeds;

[ExcludeFromCodeCoverage]
public class CartSeeder : IDataSeeder
{
    private readonly PostgreDbContext _dbContext;
    public CartSeeder(PostgreDbContext dbContext) => _dbContext = dbContext;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Carts.AnyAsync(cancellationToken))
            return;

        var user = await _dbContext.Users.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (user == null) return;

        var cart = new Cart
        {
            UserId = user.Id,
            Date = DateTime.UtcNow,
        };

        _dbContext.Carts.Add(cart);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}