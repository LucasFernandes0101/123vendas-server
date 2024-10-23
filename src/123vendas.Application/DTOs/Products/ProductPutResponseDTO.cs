using _123vendas.Domain.Enums;

namespace _123vendas.Application.DTOs.Products;

public class ProductPutResponseDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; }
}