using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public class SalePutRequestDTO
{
    public int Id { get; set; }
    public SaleStatus Status { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? CancelledAt { get; set; }
}