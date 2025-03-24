using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public record SalePutResponseDTO
{
    public int Id { get; init; }
    public short Sequence { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
    public SaleStatus Status { get; init; }
    public DateTimeOffset Date { get; init; }
    public int CustomerId { get; init; }
    public int BranchId { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTimeOffset? CancelledAt { get; init; }
}