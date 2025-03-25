using _123vendas.Domain.Base;
using _123vendas.Domain.Entities;

namespace _123vendas.Application.Events.Sales
{
    public class SaleCreatedEvent : BaseEvent
    {
        public SaleCreatedEvent(Sale sale) : base("Sale")
        {
            Id = sale.Id;
            Date = sale.Date;
        }

        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
