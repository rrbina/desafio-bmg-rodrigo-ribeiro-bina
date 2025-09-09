using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigEcommerce.Producer.Sales.Application.DTOs;

namespace BigEcommerce.Producer.Sales.Application.Commands.UsePixPayment
{
    public class PixPaymentCommand : IRequest<PixPaymentResultDto>
    {
        public PixPaymentDto Payment { get; }

        public PixPaymentCommand(PixPaymentDto payment)
        {
            Payment = payment;
        }
    }
}
