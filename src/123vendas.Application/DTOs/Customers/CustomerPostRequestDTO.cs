namespace _123vendas.Application.DTOs.Customers;

public record CustomerPostRequestDTO
{
    public string? Name { get; init; }
    public string? Document { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public string? Address { get; init; }
    public bool IsActive { get; init; }
}