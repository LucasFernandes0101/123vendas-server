namespace _123vendas.Application.DTOs.Branches;

public record BranchGetRequestDTO
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int Page { get; init; } = 1;
    public int MaxResults { get; init; } = 10;
}