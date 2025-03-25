using _123vendas.Domain.Base;
using _123vendas.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace _123vendas.Domain.Entities;

public class Product : BaseEntity
{
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public string? Image { get; set; }
    public ProductRating? Rating { get; set; }
    public bool IsActive { get; set; }
}

[Owned]
public class ProductRating
{
    public double? Rate { get; set; }
    public int Count { get; set; }
}