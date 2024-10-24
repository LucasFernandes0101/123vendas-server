using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Application.Events.Sales;

public class SaleUpdatedEvent : BaseEvent
{
    public SaleUpdatedEvent(Sale sale) : base("Sale")
    {
        Id = sale.Id;
        UpdatedAt = sale.UpdatedAt;
    }

    public int Id { get; set; }
    public DateTime UpdatedAt { get; set; }
}