using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface ICartService
{
    Task<PagedResult<Cart>> GetAllAsync(int? id, int? userId, DateTimeOffset? minDate, DateTimeOffset? maxDate, int page = 1, int maxResults = 10, string? orderByClause = default);
    Task<Cart?> GetByIdAsync(int id);
    Task<Cart> CreateAsync(Cart request);
    Task<Cart> UpdateAsync(int id, Cart request);
    Task DeleteAsync(int id);
}