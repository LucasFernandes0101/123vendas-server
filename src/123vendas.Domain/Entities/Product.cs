using _123vendas.Domain.Base;
using _123vendas.Domain.Enums;

namespace _123vendas.Domain.Entities;

public class Product : BaseEntity
{
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public string? Image { get; set; }
    public double? Rating { get; set; }
    public int RateCount { get; set; }
    public bool IsActive { get; set; }
}