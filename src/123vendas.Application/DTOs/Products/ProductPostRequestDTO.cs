using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Products;

public class ProductPostRequestDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; }
}