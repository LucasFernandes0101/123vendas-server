using _123vendas.Application.DTOs.Common;

namespace _123vendas.Application.DTOs.Branches;

public record BranchGetRequestDTO : PagedRequestDTO
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}