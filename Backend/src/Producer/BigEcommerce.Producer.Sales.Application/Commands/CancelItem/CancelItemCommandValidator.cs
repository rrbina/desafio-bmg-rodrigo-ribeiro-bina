using FluentValidation;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.CancelItem
{
    public class CancelItemCommandValidator : AbstractValidator<CancelItemCommand>
    {
        public CancelItemCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Id da venda é obrigatório.");

            RuleFor(x => x.ItemId)
                .NotEmpty().WithMessage("Id do item é obrigatório.");
        }
    }
}