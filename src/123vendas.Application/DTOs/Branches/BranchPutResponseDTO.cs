namespace _123vendas.Application.DTOs.Branches;

public record BranchPutResponseDTO
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public string? Name { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public bool IsActive { get; init; }
}