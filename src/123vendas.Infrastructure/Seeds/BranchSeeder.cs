using _123vendas.Domain.Entities;
using _123vendas.Domain.Interfaces.Seeds;
using _123vendas.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Seeds;

[ExcludeFromCodeCoverage]
public class BranchSeeder : IDataSeeder
{
    private readonly PostgreDbContext _dbContext;
    public BranchSeeder(PostgreDbContext dbContext) => _dbContext = dbContext;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Branches.AnyAsync(cancellationToken))
            return;

        var branches = new List<Branch>
        {
            new Branch { Name = "Matriz", Address = "Endereço Matriz", Phone = "333333333", IsActive = true },
            new Branch { Name = "Filial", Address = "Endereço Filial", Phone = "444444444", IsActive = true }
        };

        _dbContext.Branches.AddRange(branches);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}