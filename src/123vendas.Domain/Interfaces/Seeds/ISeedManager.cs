namespace _123vendas.Domain.Interfaces.Seeds;

public interface ISeedManager
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}