using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigEcommerce.Producer.Sales.Application.Commands.PixPayment
{
    using BigEcommerce.Producer.Sales.Application.Commands.UsePixPayment;
    using FluentValidation;

    public class PixPaymentCommandValidator : AbstractValidator<PixPaymentCommand>
    {
        public PixPaymentCommandValidator()
        {
            RuleFor(x => x.Payment)
                .NotNull().WithMessage("Dados de pagamento não informados.");

            RuleFor(x => x.Payment.OrderId)
                .NotEmpty().WithMessage("O ID do pedido é obrigatório.");

            RuleFor(x => x.Payment.Amount)
                .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");
        }
    }
}
