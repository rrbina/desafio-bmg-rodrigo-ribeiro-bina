using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Producer.Sales.Application.DTOs
{
    public class PixPaymentDto
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
