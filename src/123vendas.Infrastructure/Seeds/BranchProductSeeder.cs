using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Seeds;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Seeds;

[ExcludeFromCodeCoverage]
public class BranchProductSeeder : IDataSeeder
{
    private readonly PostgreDbContext _dbContext;
    public BranchProductSeeder(PostgreDbContext dbContext) => _dbContext = dbContext;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.BranchProducts.AnyAsync(cancellationToken))
            return;

        var branch = await _dbContext.Branches.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        var product = await _dbContext.Products.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        if (branch is null || product is null) 
            return;

        var branchProducts = new List<BranchProduct>
        {
            new BranchProduct
            {
                BranchId = branch.Id,
                ProductId = product.Id,
                ProductTitle = product.Title,
                ProductCategory = product.Category,
                Price = product.Price,
                StockQuantity = 100,
                IsActive = true
            }
        };

        _dbContext.BranchProducts.AddRange(branchProducts);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}