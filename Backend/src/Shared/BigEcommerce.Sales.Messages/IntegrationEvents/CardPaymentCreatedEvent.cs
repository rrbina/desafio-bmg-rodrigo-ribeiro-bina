using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Sales.Messages.IntegrationEvents
{
    public class CardPaymentCreatedEvent
    {
        public Guid OrderId { get; }
        public DateTime EventDateTime { get; set; }

        public CardPaymentCreatedEvent(Guid orderId)
        {
            OrderId = orderId;
            EventDateTime = DateTime.Now;
        }
    }
}
