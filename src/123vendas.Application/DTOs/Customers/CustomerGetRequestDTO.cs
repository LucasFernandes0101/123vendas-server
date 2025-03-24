using _123vendas.Application.DTOs.Common;

namespace _123vendas.Application.DTOs.Customers;

public record CustomerGetRequestDTO : PagedRequestDTO
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? Document { get; init; }
    public string? Phone { get; init; }
    public string? Email { get; init; }
    public bool? IsActive { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
}