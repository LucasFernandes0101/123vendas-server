using _123vendas.Domain.Entities;
using _123vendas.Domain.Enums;

namespace _123vendas.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<List<Sale>> GetAllAsync(int? id, int? branchId, int? customerId, SaleStatus? status, DateTime? startDate, DateTime? endDate, int page = 1, int maxResults = 10);
    Task<Sale?> GetByIdAsync(int id);
    Task<Sale> CreateSaleAsync(Sale request);
    Task<Sale> UpdateSaleAsync(int saleId, Sale request);
    Task DeleteAsync(int saleId);
    Task<Sale> CancelSaleAsync(int saleId);
    Task<Sale> CancelSaleItemAsync(int saleId, int saleItemId);
}