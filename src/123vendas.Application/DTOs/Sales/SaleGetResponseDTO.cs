using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public record SaleGetResponseDTO
{
    public int Id { get; init; }
    public SaleStatus Status { get; init; }
    public DateTime Date { get; init; }
    public int CustomerId { get; init; }
    public int BranchId { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime? CancelledAt { get; init; }
}