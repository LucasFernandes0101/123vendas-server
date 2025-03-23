using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Sale?> GetWithItemsByIdAsync(int id)
    {
        return await _dbContext.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}