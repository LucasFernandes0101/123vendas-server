using _123vendas.Domain.Base;

namespace _123vendas.Domain.Entities;

public class Cart : BaseEntity
{
    public int UserId { get; set; }
    public DateTimeOffset Date { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual List<CartProduct>? Products { get; set; }
}