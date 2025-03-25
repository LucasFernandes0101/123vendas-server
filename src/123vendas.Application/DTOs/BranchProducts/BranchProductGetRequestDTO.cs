using _123vendas.Application.DTOs.Common;

namespace _123vendas.Application.DTOs.BranchProducts;

public record BranchProductGetRequestDTO : PagedRequestDTO
{
    public int? Id { get; init; }
    public int? ProductId { get; init; }
    public int? BranchId { get; init; }
    public string? ProductTitle { get; init; }
    public bool? IsActive { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
}