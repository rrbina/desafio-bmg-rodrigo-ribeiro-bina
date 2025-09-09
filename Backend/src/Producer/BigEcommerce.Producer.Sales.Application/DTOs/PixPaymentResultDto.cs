using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Producer.Sales.Application.DTOs
{
    public class PixPaymentResultDto
    {
        public Guid PixCode { get; set; }

        public PixPaymentResultDto(Guid pixCode)
        {
            PixCode = pixCode;
        }
    }
}
