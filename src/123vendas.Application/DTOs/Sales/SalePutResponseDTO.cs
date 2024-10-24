using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public class SalePutResponseDTO
{
    public int Id { get; set; }
    public short Sequence { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public SaleStatus Status { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? CancelledAt { get; set; }
}