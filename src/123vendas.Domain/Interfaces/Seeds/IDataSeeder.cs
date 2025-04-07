namespace _123vendas.Domain.Interfaces.Seeds;

public interface IDataSeeder
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}