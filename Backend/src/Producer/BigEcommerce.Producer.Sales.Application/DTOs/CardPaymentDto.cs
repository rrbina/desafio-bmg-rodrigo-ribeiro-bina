using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Producer.Sales.Application.DTOs
{
    public class CardPaymentDto
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }

        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
    }
}
