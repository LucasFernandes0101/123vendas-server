using _123vendas.Domain.Base;

namespace _123vendas.Domain.Entities;

public class SaleItem : BaseEntity
{
    public short Sequence { get; set; } 
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public string? ProductTitle { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public bool IsCancelled { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }

    public virtual Sale? Sale { get; set; }
    public virtual Product? Product { get; set; }
}