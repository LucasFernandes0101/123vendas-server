using _123vendas.Application.DTOs.Common;
using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Sales;

public record SaleGetRequestDTO : PagedRequestDTO
{
    public int? Id { get; init; }
    public int? UserId { get; init; }
    public int? BranchId { get; init; }
    public SaleStatus? Status { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
}