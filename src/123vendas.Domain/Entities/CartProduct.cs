using _123vendas.Domain.Base;

namespace _123vendas.Domain.Entities;

public class CartProduct : BaseEntity
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public virtual Cart? Cart { get; set; }
    public virtual Product? Product { get; set; }
}