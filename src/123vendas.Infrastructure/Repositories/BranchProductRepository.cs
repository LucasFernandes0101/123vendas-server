using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;
using _123vendas.Domain.Interfaces.Repositories;
using _123vendas.Infrastructure.Contexts;
using _123vendas.Infrastructure.Scripts;
using Microsoft.EntityFrameworkCore;

namespace _123vendas.Infrastructure.Repositories;

public class BranchProductRepository : BaseRepository<BranchProduct>, IBranchProductRepository
{
    public BranchProductRepository(SqlDbContext dbContext) : base(dbContext)
    {
    }

    public async Task UpdateByProductIdAsync(int productId, string productName, ProductCategory productCategory)
    {
        await _dbContext.Database.ExecuteSqlRawAsync(SqlScripts.UpdateBranchProductsByProductId, 
                                                     productName, 
                                                     (int)productCategory, 
                                                     productId);
    }
}