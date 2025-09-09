using BigEcommerce.Producer.Sales.Domain.Entities;
using BigEcommerce.Producer.Sales.Domain.Exceptions;
using FluentValidation;

namespace BigEcommerce.Producer.Sales.Application.Helpers
{
    public static class SaleItemValidator
    {
        public static void Validate(SaleItem item, decimal discount)
        {
            if (item.Quantity < 4 && discount > 0)
                throw new BigEcommerceException("Não é permitido aplicar desconto para quantidades inferiores a 4.");            

            var discountValidatorResult = new DiscountValidator().Validate(item);

            if (!discountValidatorResult.IsValid)
                throw new BigEcommerceException(ValidationMessageHelper.CreateMessageFromFailures(discountValidatorResult.Errors));
        }        
    }
}
