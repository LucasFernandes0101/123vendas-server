namespace _123vendas.Application.DTOs.Branches;

public record BranchGetDetailResponseDTO
{
    public int Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
    public string? Name { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public bool IsActive { get; init; }
}