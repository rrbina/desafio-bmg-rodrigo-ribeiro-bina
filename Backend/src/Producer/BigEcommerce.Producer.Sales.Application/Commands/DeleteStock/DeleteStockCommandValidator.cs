using FluentValidation;

namespace BigEcommerce.Producer.Sales.Application.Commands.DeleteStock
{
    public class DeleteStockCommandValidator : AbstractValidator<DeleteStockCommand>
    {
        public DeleteStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
        }
    }
}