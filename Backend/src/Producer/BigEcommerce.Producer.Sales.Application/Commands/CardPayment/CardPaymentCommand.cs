using BigEcommerce.Producer.Sales.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Producer.Sales.Application.Commands.CardPayment
{
    public class CardPaymentCommand : IRequest<bool>
    {
        public CardPaymentDto Payment { get; }

        public CardPaymentCommand(CardPaymentDto payment)
        {
            Payment = payment;
        }
    }
}
