using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface ICustomerService
{
    Task<PagedResult<Customer>> GetAllAsync(int? id, string? name, string? document, string? phone, string? email, bool? isActive, DateTime? startDate, DateTime? endDate, int page = 1, int maxResults = 10, string? orderByClause = default);
    Task<Customer?> GetByIdAsync(int id);
    Task<Customer> CreateAsync(Customer request);
    Task<Customer> UpdateAsync(int id, Customer request);
    Task DeleteAsync(int id);
}