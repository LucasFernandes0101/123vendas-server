namespace _123vendas.Application.DTOs.Branches;

public record BranchPostRequestDTO
{
    public string? Name { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public bool IsActive { get; init; }
}