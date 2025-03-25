using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Cart?> GetWithProductsByIdAsync(int id)
        => await _dbContext.Carts.Include(s => s.Products)
                                 .FirstOrDefaultAsync(s => s.Id == id);
}