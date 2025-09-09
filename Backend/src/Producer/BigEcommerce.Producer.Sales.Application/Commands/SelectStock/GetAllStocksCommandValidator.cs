using FluentValidation;

namespace BigEcommerce.Producer.Sales.Application.Commands.SelectStock
{
    public class GetAllStocksCommandValidator : AbstractValidator<GetAllStocksCommand>
    {
        public GetAllStocksCommandValidator()
        {
            
        }
    }
}