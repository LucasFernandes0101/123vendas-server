namespace _123vendas.Application.DTOs.Customers;

public record CustomerGetRequestDTO
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? Document { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public bool? IsActive { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int Page { get; init; } = 1;
    public int MaxResults { get; init; } = 10;
}