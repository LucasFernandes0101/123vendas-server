namespace _123vendas.Application.DTOs.Customers;

public record CustomerGetResponseDTO
{
    public int Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public string? Name { get; init; }
    public string? Document { get; init; }
    public bool IsActive { get; init; }
}