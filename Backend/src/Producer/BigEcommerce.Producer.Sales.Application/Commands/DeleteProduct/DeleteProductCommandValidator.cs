using FluentValidation;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteProduct
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("O ID do produto é obrigatório.");
        }
    }
}