using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Sales.Messages.IntegrationEvents
{
    public class PixPaymentCreatedEvent
    {
        public Guid OrderId { get; }
        public Guid PixCode { get; }
        public DateTime EventDateTime { get; set; }

        public PixPaymentCreatedEvent(Guid orderId, Guid pixCode)
        {
            OrderId = orderId;
            PixCode = pixCode;
            EventDateTime = DateTime.Now;
        }
    }
}
