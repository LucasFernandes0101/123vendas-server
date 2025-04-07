using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Seeds;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Seeds;

[ExcludeFromCodeCoverage]
public class CartProductSeeder : IDataSeeder
{
    private readonly PostgreDbContext _dbContext;
    public CartProductSeeder(PostgreDbContext dbContext) => _dbContext = dbContext;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.CartProducts.AnyAsync(cancellationToken))
            return;

        var cart = await _dbContext.Carts.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        var product = await _dbContext.Products.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (cart is null || product is null) 
            return;

        var cartProduct = new CartProduct
        {
            CartId = cart.Id,
            ProductId = product.Id,
            Quantity = 2
        };

        _dbContext.CartProducts.Add(cartProduct);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}