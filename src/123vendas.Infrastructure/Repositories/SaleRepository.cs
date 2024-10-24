using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace _123vendas.Infrastructure.Repositories;

public class SaleRepository : BaseRepository<Sale>, ISaleRepository
{
    public SaleRepository(SqlDbContext dbContext) : base(dbContext)
    {
    }

    public new async Task<Sale?> GetWithItemsByIdAsync(int id)
    {
        return await _dbContext.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}