using _123vendas.Domain.Base;
using _123vendas.Domain.Enums;

namespace _123vendas.Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ProductCategory Category { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; }
}