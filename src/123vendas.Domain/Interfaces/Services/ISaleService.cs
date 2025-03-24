using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;

namespace _123vendas.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<PagedResult<Sale>> GetAllAsync(int? id = default, int? branchId = default, int? customerId = default, SaleStatus? status = default, DateTimeOffset? startDate = default, DateTimeOffset? endDate = default, int page = 1, int maxResults = 10, string? orderByClause = default);
    Task<Sale?> GetByIdAsync(int id);
    Task<Sale> CreateAsync(Sale request);
    Task<Sale> UpdateAsync(int saleId, Sale request);
    Task DeleteAsync(int saleId);
    Task<Sale> CancelAsync(int saleId);
    Task<Sale> CancelItemAsync(int saleId, int sequence);
    Task<SaleItem> GetItemAsync(int saleId, int sequence);
}