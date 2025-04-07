using _123vendas.Domain.Interfaces.Seeds;
using System.Diagnostics.CodeAnalysis;

namespace _123vendas.Infrastructure.Seeds;

[ExcludeFromCodeCoverage]
public class SeedManager : ISeedManager
{
    private readonly IEnumerable<IDataSeeder> _seeders;
    public SeedManager(IEnumerable<IDataSeeder> seeders) => _seeders = seeders;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (!IsSeedEnabled())
            return;

        foreach (var seeder in _seeders)
            await seeder.SeedAsync(cancellationToken);
    }

    private bool IsSeedEnabled()
    {
        var value = Environment.GetEnvironmentVariable("SEED_DATABASE_FLAG");

        return bool.TryParse(value, out bool result) && result;
    }
}