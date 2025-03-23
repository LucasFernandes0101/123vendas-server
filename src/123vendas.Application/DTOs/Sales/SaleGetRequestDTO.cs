using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public record SaleGetRequestDTO
{
    public int? Id { get; init; }
    public int? CustomerId { get; init; }
    public int? BranchId { get; init; }
    public SaleStatus? Status { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int Page { get; init; } = 1;
    public int MaxResults { get; init; } = 10;
}