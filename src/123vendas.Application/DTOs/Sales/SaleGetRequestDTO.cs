using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public class SaleGetRequestDTO
{
    public int? Id { get; set; }
    public int? CustomerId { get; set; }
    public int? BranchId { get; set; }
    public SaleStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Page { get; set; } = 1;
    public int MaxResults { get; set; } = 10;
}