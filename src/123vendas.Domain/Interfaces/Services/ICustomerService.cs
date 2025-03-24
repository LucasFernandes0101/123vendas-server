using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<PagedResult<Customer>> GetAllAsync(int? id = default, string? name = default, string? document = default, string? phone = default, string? email = default, bool? isActive = default, DateTimeOffset? startDate = default, DateTimeOffset? endDate = default, int page = 1, int maxResults = 10, string? orderByClause = default);
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> CreateAsync(Customer request);
    Task<Customer> UpdateAsync(int id, Customer request);
    Task DeleteAsync(int id);
}