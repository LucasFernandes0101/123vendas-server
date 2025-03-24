using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface IBranchService
{
    Task<PagedResult<Branch>> GetAllAsync(int? id, bool? isActive, string? name, DateTime? startDate, DateTime? endDate, int page = 1, int maxResults = 10, string? orderByClause = default);
    Task<Branch?> GetByIdAsync(int id);
    Task<Branch> CreateAsync(Branch request);
    Task<Branch> UpdateAsync(int id, Branch request);
    Task DeleteAsync(int id);
}