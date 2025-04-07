using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;
using _123vendas.Domain.Interfaces.Seeds;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Seeds;

[ExcludeFromCodeCoverage]
public class ProductSeeder : IDataSeeder
{
    private readonly PostgreDbContext _dbContext;
    public ProductSeeder(PostgreDbContext dbContext) => _dbContext = dbContext;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Products.AnyAsync(cancellationToken))
            return;

        var products = new List<Product>
        {
            new Product
            {
                Title = "Produto A",
                Price = 10.5m,
                Description = "Descrição do Produto A",
                Category = ProductCategory.Beer,
                Image = "https://s3-sa-east-1.amazonaws.com/123vendas-files-bucket/uploads/products/imagem-123abc987def456.png",
                Rating = new ProductRating { Rate = 4.5, Count = 10 },
                IsActive = true
            },
            new Product
            {
                Title = "Produto B",
                Price = 20m,
                Description = "Descrição do Produto B",
                Category = ProductCategory.Juice,
                Image = "https://s3-sa-east-1.amazonaws.com/123vendas-files-bucket/uploads/products/imagem-123cba987def476.png",
                Rating = new ProductRating { Rate = 4.0, Count = 5 },
                IsActive = true
            }
        };

        _dbContext.Products.AddRange(products);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}