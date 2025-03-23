using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public record SaleGetDetailResponseDTO
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public SaleStatus Status { get; init; }
    public DateTime Date { get; init; }
    public int CustomerId { get; init; }
    public int BranchId { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime? CancelledAt { get; init; }

    public List<SaleItemGetDTO>? Items { get; init; }
}