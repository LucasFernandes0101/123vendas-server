namespace _123vendas.Application.DTOs.Branches;

public record BranchGetResponseDTO
{
    public int Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string? Name { get; init; }
    public string? Address { get; init; }
    public string? Phone { get; init; }
    public bool IsActive { get; init; }
}