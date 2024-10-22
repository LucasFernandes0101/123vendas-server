using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;

namespace _123vendas.Infrastructure.Repositories;

public class SaleItemRepository : BaseRepository<SaleItem>, ISaleItemRepository
{
    public SaleItemRepository(SqlDbContext dbContext) : base(dbContext)
    {
    }
}