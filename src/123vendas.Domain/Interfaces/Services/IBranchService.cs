using _123vendas.Domain.Entities;

namespace _123vendas.Domain.Interfaces.Services;

public interface IBranchService
{
    Task<List<Branch>> GetAllAsync(Branch request);
    Task<Branch> GetByIdAsync(int id);
    Task<Branch> CreateAsync(Branch request);
    Task<Branch> UpdateAsync(int id, Branch request);
    Task DeleteAsync(int id);
}