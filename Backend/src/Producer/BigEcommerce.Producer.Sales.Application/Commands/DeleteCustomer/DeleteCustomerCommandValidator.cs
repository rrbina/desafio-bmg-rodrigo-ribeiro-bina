using FluentValidation;

namespace BigEcommerce.Producer.Sales.Application.Sales.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório.");
        }
    }
}