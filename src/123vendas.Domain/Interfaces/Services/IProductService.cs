using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface IProductService
{
    Task<Product> CreateAsync(Product request);
    Task<Product> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync(string? name, bool? isActive, int page = 1, int maxResults = 10);
    Task<Product> UpdateAsync(int id, Product request);
    Task DeleteAsync(int id);
}