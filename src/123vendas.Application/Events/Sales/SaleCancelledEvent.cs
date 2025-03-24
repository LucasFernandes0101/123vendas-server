using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Application.Events.Sales;

public class SaleCancelledEvent : BaseEvent
{
    public SaleCancelledEvent(Sale sale) : base("Sale")
    {
        Id = sale.Id;
        CancelledAt = sale.CancelledAt ?? DateTime.UtcNow;
    }

    public int Id { get; set; }
    public DateTimeOffset CancelledAt { get; set; }
}