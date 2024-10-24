using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public class SaleItemRepository : BaseRepository<SaleItem>, ISaleItemRepository
{
    public SaleItemRepository(SqlDbContext dbContext) : base(dbContext)
    {
    }
}