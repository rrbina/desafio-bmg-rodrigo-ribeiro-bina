using FluentValidation;

namespace BigEcommerce.Producer.Sales.Application.Commands.UpdateStock
{
    public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.QuantityChange)
                .NotEqual(0).WithMessage("QuantityChange must be non-zero.");
        }
    }
}