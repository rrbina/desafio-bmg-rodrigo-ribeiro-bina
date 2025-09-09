using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Producer.Sales.Application.Commands.CardPayment
{
    using FluentValidation;

    namespace BigEcommerce.Producer.Sales.Application.Commands.CardPayment
    {
        public class CardPaymentCommandValidator : AbstractValidator<CardPaymentCommand>
        {
            public CardPaymentCommandValidator()
            {
                RuleFor(x => x.Payment)
                    .NotNull().WithMessage("Dados de pagamento não informados.");

                RuleFor(x => x.Payment.OrderId)
                    .NotEmpty().WithMessage("O ID do pedido é obrigatório.");

                RuleFor(x => x.Payment.Amount)
                    .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");

                RuleFor(x => x.Payment.CardNumber)
                    .NotEmpty().WithMessage("O número do cartão é obrigatório.");

                RuleFor(x => x.Payment.CardHolder)
                    .NotEmpty().WithMessage("O nome do titular é obrigatório.");

                RuleFor(x => x.Payment.Expiration)
                    .NotEmpty().WithMessage("A data de expiração é obrigatória.");

                RuleFor(x => x.Payment.CVV)
                    .NotEmpty().WithMessage("O CVV é obrigatório.");
            }
        }
    }
}
